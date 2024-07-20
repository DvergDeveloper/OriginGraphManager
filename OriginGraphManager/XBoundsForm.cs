using System;
using System.Windows.Forms;

namespace OriginGraphManager
{
    public enum BoundsType
    {
        Fixed,
        Conditional
    }

    public partial class XBoundsForm : Form, IFillable
    {
        private const string _prodolItem = "Продол";
        private const string _bokItem = "Бок";
        private const string _shassiItem = "Шасси";
        private const string _otherItem = "Другой";

        private string[] _prodNames;
        private string[] _bokNames;
        private string[] _shassiNames;
        private string[] _otherNames;

        private string[] _startChoosedNames;
        private string[] _endChoosedNames;
        private int _startParamIndex;
        private int _endParamIndex;
        private string _startCondition;
        private string _endCondition;
        private double _startTargetValue;
        private double _endTargetValue;
        private bool _needCorrection;

        private double _fixedStartBorder;
        private double _fixedEndBorder;

        private bool _isFilled;
        private BoundsType _xBoundsType;

        public event Action OnFilling;

        enum Channel
        {
            prodol,
            bok,
            shassi,
            other
        }

        public bool IsFilled { get { return _isFilled; } }

        public XBoundsForm() // - вроде инициализация еще и по правилам базового класса
        {
            InitializeComponent();
            SetStartValue();
            InitializeInterface();
            SetStartStates();
        }

        private void SetStartValue()
        {
            XBoundTypeBox.SelectedIndex = 1;
            StartConditionTypeComboBox.SelectedIndex = 0;
            EndConditionTypeComboBox.SelectedIndex = 0;
            _xBoundsType = BoundsType.Conditional;
            OriginController.XBoundsType = _xBoundsType;
            StartConditionTypeComboBox.SelectedIndex = 0;
            EndConditionTypeComboBox.SelectedIndex = 0;
            XStartTextBox.Text = "0";
            _isFilled = false;
            _needCorrection = true;
        }

        private void SetStartStates()
        {
            FixXField.Enabled = false;
            StartParamNameComboBox.Enabled = false;
            EndParamNameComboBox.Enabled = false;
        }

        private void InitializeInterface()
        {
            if (OriginController.HaveProdol)
            {
                StartTypeComboBox.Items.Add(_prodolItem);
                EndTypeComboBox.Items.Add(_prodolItem);
                OriginController.GetProdolNames(out _prodNames);
            }
            if (OriginController.HaveBok)
            {
                StartTypeComboBox.Items.Add(_bokItem);
                EndTypeComboBox.Items.Add(_bokItem);
                OriginController.GetBokNames(out _bokNames);
            }
            if (OriginController.HaveShassi)
            {
                StartTypeComboBox.Items.Add(_shassiItem);
                EndTypeComboBox.Items.Add(_shassiItem);
                OriginController.GetShassiNames(out _shassiNames);
            }
            if (OriginController.HaveOthers)
            {
                StartTypeComboBox.Items.Add(_otherItem);
                EndTypeComboBox.Items.Add(_otherItem);
                OriginController.GetOtherNames(out _otherNames);
            }
        }

        private void StartTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (StartTypeComboBox.Text)
            {
                case _prodolItem:
                    SubstitudeComboBox(ref StartParamNameComboBox,
                     OriginController.GetParamNamesFromTXT(_prodNames[0]));
                    _startChoosedNames = _prodNames;
                    break;
                case _bokItem:
                    SubstitudeComboBox(ref StartParamNameComboBox,
                        OriginController.GetParamNamesFromTXT(_bokNames[0]));
                    _startChoosedNames = _bokNames;
                    break;
                case _shassiItem:
                    SubstitudeComboBox(ref StartParamNameComboBox,
                        OriginController.GetParamNamesFromTXT(_shassiNames[0]));
                    _startChoosedNames = _shassiNames;
                    break;
                case _otherItem:
                    SubstitudeComboBox(ref StartParamNameComboBox,
                        OriginController.GetParamNamesFromTXT(_otherNames[0]));
                    _startChoosedNames = _otherNames;
                    break;
            }

            CheckIsFormFilled();
        }

        private void SubstitudeComboBox(ref ComboBox comboBox, string[] cells)
        {
            comboBox.Enabled = true;
            comboBox.Items.Clear();
            comboBox.Items.AddRange(cells);
        }

        private void StartParamNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startParamIndex = StartParamNameComboBox.SelectedIndex;

            CheckIsFormFilled();
        }

        private void StartConditionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _startCondition = StartConditionTypeComboBox.Text;

            CheckIsFormFilled();
        }

        private void EndTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (EndTypeComboBox.Text)
            {
                case _prodolItem:
                    SubstitudeComboBox(ref EndParamNameComboBox,
                        OriginController.GetParamNamesFromTXT(_prodNames[0]));
                    _endChoosedNames = _prodNames;
                    break;
                case _bokItem:
                    SubstitudeComboBox(ref EndParamNameComboBox,
                        OriginController.GetParamNamesFromTXT(_bokNames[0]));
                    _endChoosedNames = _bokNames;
                    break;
                case _shassiItem:
                    SubstitudeComboBox(ref EndParamNameComboBox,
                        OriginController.GetParamNamesFromTXT(_shassiNames[0]));
                    _endChoosedNames = _shassiNames;
                    break;
                case _otherItem:
                    SubstitudeComboBox(ref EndParamNameComboBox,
                        OriginController.GetParamNamesFromTXT(_otherNames[0]));
                    _endChoosedNames = _otherNames;
                    break;
            }

            CheckIsFormFilled();
        }

        private void EndParamNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endParamIndex = EndParamNameComboBox.SelectedIndex;
        }

        private void EndConditionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _endCondition = EndConditionTypeComboBox.Text;
        }

        private void ProtectFromIncorrectInput(object sender, KeyPressEventArgs e)
        {
            if (!ConfigContainer.IsCorrectNumericInput(e, (TextBox)sender))
                e.Handled = true;

        }

        private void BanInput(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void XBoundTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _xBoundsType = XBoundTypeBox.SelectedIndex == 0 ? BoundsType.Fixed : BoundsType.Conditional;
            OriginController.XBoundsType = _xBoundsType;

            if (_xBoundsType == BoundsType.Conditional)
            {
                FixXField.Enabled = false;
                ConditionsField.Enabled = true;
            }
            else
            {
                FixXField.Enabled = true;
                ConditionsField.Enabled = false;
            }

            CheckIsFormFilled();
        }

        private void CheckIsFormFilled(object sender, EventArgs e)
        {
            CheckIsFormFilled();
        }

        private void CheckIsFormFilled()
        {
            if (_xBoundsType == BoundsType.Conditional)
            {
                if (StartTypeComboBox.SelectedItem != null &&
                   EndTypeComboBox.SelectedItem != null &&
                    StartParamNameComboBox.SelectedItem != null &&
                    EndParamNameComboBox.SelectedItem != null &&
                    !string.IsNullOrEmpty(StartTargetTextBox.Text) &&
                    !string.IsNullOrEmpty(EndTargetTextBox.Text))
                {
                    _isFilled = true;
                    WriteCondition();
                    SetFormVariables();
                    SetFormVariablesToConfig();
                }
                else
                {
                    _isFilled = false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(XStartTextBox.Text) &&
                    !string.IsNullOrEmpty(XEndTextBox.Text))
                {
                    _isFilled = true;
                    SetFormVariables();
                    SetFormVariablesToConfig();
                }
                else
                {
                    _isFilled = false;
                }
            }

            if (OnFilling != null)
            {
                OnFilling.Invoke();
            }
        }

        private void WriteCondition()
        {
            StartConditionTextBox.Text = StartParamNameComboBox.Text +
                " " + StartConditionTypeComboBox.Text + " " + StartTargetTextBox.Text;

            EndConditionTextBox.Text = EndParamNameComboBox.Text +
            " " + EndConditionTypeComboBox.Text + " " + EndTargetTextBox.Text;
        }


        private void SetFormVariables()
        {
            if (_xBoundsType == BoundsType.Conditional)
            {
                _startTargetValue = Double.Parse(StartTargetTextBox.Text);
                _endTargetValue = Double.Parse(EndTargetTextBox.Text);
            }
            else
            {
                _fixedStartBorder = Double.Parse(XStartTextBox.Text);
                _fixedEndBorder = Double.Parse(XEndTextBox.Text);
            }
        }

        private void SetFormVariablesToConfig()
        {
            ConfigContainer.SetXBoundsParams(_startParamIndex, _endParamIndex, _startCondition,
         _endCondition, _startTargetValue, _endTargetValue, _startChoosedNames, _endChoosedNames,
         _needCorrection, _xBoundsType, _fixedStartBorder, _fixedEndBorder);
        }

        private void CorrectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _needCorrection = CorrectionCheckBox.Checked;
        }
    }
}
