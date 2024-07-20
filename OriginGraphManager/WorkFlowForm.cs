using System;
using System.Drawing;
using System.Windows.Forms;

namespace OriginGraphManager
{
    public partial class WorkFlowForm : Form
    {
        private string _fullFileName;
        private MainAction _ChoosedAction;

        private XBoundsForm _xBoundsForm;
        private ParamsForm _paramsForm;

        private MainForm _mainForm;

        private readonly int _formInterval = 5;

        public WorkFlowForm(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            InitializeInterface();
        }

        private void InitializeInterface()
        {
            //actionComboBox.SelectedIndex = 0;
            actionButton.Enabled = false;

            ActionChooseLabel.Hide();
            actionComboBox.Hide();
        }

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            if (TryOpenOriginFile(out _fullFileName))
            {
                SetAllFileVariables();
                WriteWorkDerictory();
                InitializeSubForms();
                _mainForm.PutInFront();
            }
            else
            {
                MessageBox.Show("файл не выбран");
            }
        }

        private void SetAllFileVariables()
        {
            OriginController.LoadFile(_fullFileName);
            OriginController.SetDerictoryVariables(_fullFileName);
            OriginController.SetShablonVariables();
            OriginController.DefineAvailableChannels();
            OriginController.DefineTrueGraphPages();

            OriginController.FillAllNameArrays();

            if (OriginController.IsCorrectFilesCount())
            {
                OriginController.SortNameArrays();
                OriginController.CreateOriginProjectNames();
            }
            else
            {
                return;
            }
        }

        private void InitializeSubForms()
        {
            actionComboBox.SelectedIndex = 0;
            _ChoosedAction = MainAction.DoAll;

            ChangeWindows(_ChoosedAction);

            _paramsForm.Location = new Point(_formInterval, this.Size.Height + 2 * _formInterval);
            _xBoundsForm.Location =
                new Point(_paramsForm.Width + 2 * _formInterval, this.Size.Height + 2 * _formInterval);
        }

        private void actionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            actionComboBox.SelectedIndex = 0; // в текущей версии программы всегда первый вариант

            // Для дальнейших версий программы с выбором действий

            //switch (actionComboBox.SelectedIndex)
            //{
            //    case 0:
            //        _ChoosedAction = MainAction.DoAll;
            //        break;
            //    case 1:
            //        _ChoosedAction = MainAction.RescaleY;
            //        break;
            //    case 2:
            //        _ChoosedAction = MainAction.RescaleXY;
            //        break;
            //    default:
            //        _ChoosedAction = MainAction.DoAll;
            //        break;
            //}

            //ChangeWindows(_ChoosedAction);
        }

        enum MainAction
        {
            DoAll,
            RescaleY,
            RescaleXY
        }

        private void ChangeWindows(MainAction choosedAction)
        {
            switch (choosedAction)
            {
                case MainAction.DoAll:
                    ShowForm<ParamsForm>(ref _paramsForm);
                    ShowForm<XBoundsForm>(ref _xBoundsForm);
                    break;
                case MainAction.RescaleY:
                    HideForm<ParamsForm>(ref _paramsForm);
                    HideForm<XBoundsForm>(ref _xBoundsForm);
                    break;
                case MainAction.RescaleXY:
                    HideForm<ParamsForm>(ref _paramsForm);
                    ShowForm<XBoundsForm>(ref _xBoundsForm);
                    break;
            }

            RescaleProjectWindowXAxis(choosedAction);
        }

        private void RescaleProjectWindowXAxis(MainAction mainAction)
        {
            int formXSize = _mainForm.Size.Width;
            int formYSize = _mainForm.Size.Height + _paramsForm.Size.Height
                + 2 * _formInterval;

            _mainForm.Size = new Size(formXSize, formYSize);
        }

        //  ВЫПОЛНЕНИЕ ВСЕХ ДЕЙСТВИЙ ТУТ
        private void actionButton_Click(object sender, EventArgs e)
        {

            if (_paramsForm.IsTableFillCorrect())
                DoAll();
        }

        private void DoAll()
        {
            for (int i_record = 0; i_record < OriginController.AllRecordsCount; i_record++)
            {
                OriginController.FillWorksheet(i_record);
                ConfigContainer.CreateParamsDesk(i_record);

                OriginController.FillOriginTextFields(ConfigContainer.PlaneName,
                    (ConfigContainer.IsVariantPageName ? ConfigContainer.GetPageNameByIndex(i_record) : ConfigContainer.PageName)
                    , ConfigContainer.ParamsDesk);

                if (OriginController.XBoundsType == BoundsType.Conditional)
                {
                    if (OriginController.TryCalculateXBounds(i_record,
                        ConfigContainer.StartChoosedNames, ConfigContainer.EndChoosedNames,
                        ConfigContainer.StartParamIndex, ConfigContainer.EndParamIndex,
                        ConfigContainer.StartTargetValue, ConfigContainer.EndTargetValue,
                        ConfigContainer.StartCondition, ConfigContainer.EndCondition))
                    {
                        OriginController.RescaleXAxis(ConfigContainer.NeedCorrection);
                        OriginController.RescaleYAxis();
                    }
                    else
                    {
                        MessageBox.Show("Для записи "
                            + ConfigContainer.StartChoosedNames[i_record] +
                            " не удалось рассчитать границы");
                    }
                }
                else
                {
                    OriginController.SetXBorders(ConfigContainer.FixXStartBound,
                        ConfigContainer.FixXEndBound);
                    OriginController.RescaleXAxis(ConfigContainer.NeedCorrection);
                    OriginController.RescaleYAxis();
                }

                OriginController.SaveProject(i_record);
            }

            OriginController.ExitOrigin();
            OriginController.ReleaseCOMObjects();
        }

        private void ShowForm<FormClass>(ref FormClass formInstance) where FormClass : Form, IFillable, new()
        {
            if (formInstance == null)
            {
                CreateNewSubform<FormClass>(formInstance = new FormClass());
                formInstance.OnFilling += OnSubFormsFilling;
            }
            else
                formInstance.Show();
        }

        private void HideForm<FormClass>(ref FormClass formInstance) where FormClass : Form
        {
            if (formInstance != null)
                formInstance.Hide();
        }

        private void CreateNewSubform<FormClass>(FormClass formInstance) where FormClass : Form, new()
        {
            // formInstance = new FormClass();
            formInstance.MdiParent = this.MdiParent;
            formInstance.Show();
        }

        private void WriteWorkDerictory()
        {
            workPathTextBox.Text = System.IO.Path.GetDirectoryName(_fullFileName);
        }

        private bool TryOpenOriginFile(out string fileFullName)
        {
            fileFullName = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"c:\";
                openFileDialog.Filter = "OPJ files (*.opj)|*.opj";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileFullName = openFileDialog.FileName;
                    return true;
                }
            }

            return false;
        }

        private void OnSubFormsFilling()
        {
            if (_ChoosedAction == MainAction.DoAll)
                actionButton.Enabled = _xBoundsForm.IsFilled && _paramsForm.IsFilled;
            else if (_ChoosedAction == MainAction.RescaleXY)
                actionButton.Enabled = _xBoundsForm.IsFilled;
        }
    }
}
