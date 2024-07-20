using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OriginGraphManager
{
    public partial class ParamsForm : Form, IFillable
    {
        private string _planeName;
        private string _pageName;
        private string _VMark;

        private bool _isVariantPageName;


        private bool _haveVMark;
        private string[] _projectNames;
        private bool _isFilled = false;

        private List<string> _columnG = new List<string>();
        private List<string> _columnXT = new List<string>();
        private List<string> _columnDZ = new List<string>();
        private List<string> _columnDP = new List<string>();
        private List<string> _columnVpr = new List<string>();
        private List<string> _columnWz = new List<string>();
        private List<string> _columnText = new List<string>();

        private List<string> _columnPageNames = new List<string>();

        public event Action OnFilling;

        public bool IsFilled { get { return _isFilled; } }

        public ParamsForm()
        {
            InitializeComponent(); // Исходная строка при создании формы
            SetStartValue();
            SetStartState();
            InitializeInterface();
        }

        private void SetStartValue()
        {
            _isVariantPageName = false;
            _haveVMark = false;
            OriginController.GetOriginProjectNames(out _projectNames);
        }

        private void SetStartState()
        {
            SameNameCheckBox.Checked = true;
            FlightsNameTextBox.Enabled = true;

            VmarkCheckBox.Checked = false;
            VmarkTextBox.Enabled = false;

            UserDataTable.AllowDrop = true;

        }

        private void InitializeInterface()
        {
            UserDataTable.Rows.Add(_projectNames.Length - UserDataTable.Rows.Count);

            for (int i_row = 0; i_row < _projectNames.Length; i_row++)
                UserDataTable.Rows[i_row].Cells[0].Value = _projectNames[i_row];
        }

        private void CheckIsFormFilled(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(FlightsNameTextBox.Text) || _isVariantPageName) &&
               (!string.IsNullOrEmpty(VmarkTextBox.Text) || !_haveVMark) &&
                !string.IsNullOrEmpty(PlaneNameTextBox.Text))
            {
                _isFilled = true;
                _planeName = PlaneNameTextBox.Text;
                _pageName = !_isVariantPageName ? FlightsNameTextBox.Text : "";
                _VMark = _haveVMark ? VmarkTextBox.Text : "";
                ConfigContainer.SetDataFromForm(_planeName, _isVariantPageName, _pageName, _haveVMark, _VMark);
            }
            else
            {
                _isFilled = false;
            }

            if (OnFilling != null)
            {
                OnFilling.Invoke();
            }
        }

        private void DataTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.RowIndex > -1)
                ReloadTable(e.RowIndex, e.ColumnIndex);
        }



        private void ReloadTable(int row_index, int column_index)
        {
            List<string> workList = GetColumnByIndex(column_index);

            if (row_index > workList.Count - 1)
                while (row_index > workList.Count - 1)
                    workList.Add("");

            workList[row_index] = UserDataTable.Rows[row_index].
                Cells[column_index].Value.ToString();
        }

        private List<string> GetColumnByIndex(int i_column)
        {
            switch (i_column)
            {
                case 1:
                    return _columnG;
                case 2:
                    return _columnXT;
                case 3:
                    return _columnDZ;
                case 4:
                    return _columnDP;
                case 5:
                    return _columnVpr;
                case 6:
                    return _columnWz;
                case 7:
                    return _columnText;
                case 8:
                    return _columnPageNames;
                default:
                    return new List<string>();
            }
        }

        private void DataTable_Leave(object sender, EventArgs e)
        {
            UserDataTable.EndEdit();

            ConfigContainer.SetRezhimParamsFromTable(
                _columnG, _columnXT, _columnDZ, _columnDP,
                _columnVpr, _columnWz, _columnText, _columnPageNames);
        }

        public bool IsTableFillCorrect()
        {
            return ConfigContainer.CheckTableInput();
        }

        private void SameNameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _isVariantPageName = !SameNameCheckBox.Checked;
            FlightsNameTextBox.Enabled = !_isVariantPageName;

            if (!SameNameCheckBox.Checked)
            {
                UserDataTable.Columns.Add("PageNames", "Имена режимов");
            }
            else
            {
                if (UserDataTable.Columns.Contains("PageNames"))
                    UserDataTable.Columns.Remove("PageNames");

                _columnPageNames = new List<string>();
            }
        }

        private void VmarkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _haveVMark = VmarkCheckBox.Checked;
            VmarkTextBox.Enabled = VmarkCheckBox.Checked;
        }
    }
}
