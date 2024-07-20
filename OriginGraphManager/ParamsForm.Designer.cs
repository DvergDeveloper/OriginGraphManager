namespace OriginGraphManager
{
    partial class ParamsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserDataTable = new System.Windows.Forms.DataGridView();
            this.PlaneName = new System.Windows.Forms.Label();
            this.PlaneNameTextBox = new System.Windows.Forms.TextBox();
            this.FlightsNameTextBox = new System.Windows.Forms.TextBox();
            this.VmarkTextBox = new System.Windows.Forms.TextBox();
            this.SameNameCheckBox = new System.Windows.Forms.CheckBox();
            this.VmarkCheckBox = new System.Windows.Forms.CheckBox();
            this.RecordName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vpr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DopText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.UserDataTable)).BeginInit();
            this.SuspendLayout();
            // 
            // UserDataTable
            // 
            this.UserDataTable.AllowUserToAddRows = false;
            this.UserDataTable.AllowUserToDeleteRows = false;
            this.UserDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserDataTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordName,
            this.G,
            this.XT,
            this.DZ,
            this.DP,
            this.Vpr,
            this.Wz,
            this.DopText});
            this.UserDataTable.Location = new System.Drawing.Point(12, 173);
            this.UserDataTable.Name = "UserDataTable";
            this.UserDataTable.Size = new System.Drawing.Size(443, 256);
            this.UserDataTable.TabIndex = 0;
            this.UserDataTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataTable_CellValueChanged);
            this.UserDataTable.Leave += new System.EventHandler(this.DataTable_Leave);
            // 
            // PlaneName
            // 
            this.PlaneName.AutoSize = true;
            this.PlaneName.Location = new System.Drawing.Point(8, 9);
            this.PlaneName.Name = "PlaneName";
            this.PlaneName.Size = new System.Drawing.Size(112, 13);
            this.PlaneName.TabIndex = 1;
            this.PlaneName.Text = "Название самолета:";
            // 
            // PlaneNameTextBox
            // 
            this.PlaneNameTextBox.Location = new System.Drawing.Point(11, 26);
            this.PlaneNameTextBox.Name = "PlaneNameTextBox";
            this.PlaneNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.PlaneNameTextBox.TabIndex = 2;
            this.PlaneNameTextBox.TextChanged += new System.EventHandler(this.CheckIsFormFilled);
            // 
            // FlightsNameTextBox
            // 
            this.FlightsNameTextBox.Location = new System.Drawing.Point(11, 85);
            this.FlightsNameTextBox.Name = "FlightsNameTextBox";
            this.FlightsNameTextBox.Size = new System.Drawing.Size(240, 20);
            this.FlightsNameTextBox.TabIndex = 4;
            this.FlightsNameTextBox.TextChanged += new System.EventHandler(this.CheckIsFormFilled);
            // 
            // VmarkTextBox
            // 
            this.VmarkTextBox.Location = new System.Drawing.Point(14, 138);
            this.VmarkTextBox.Name = "VmarkTextBox";
            this.VmarkTextBox.Size = new System.Drawing.Size(240, 20);
            this.VmarkTextBox.TabIndex = 7;
            this.VmarkTextBox.TextChanged += new System.EventHandler(this.CheckIsFormFilled);
            // 
            // SameNameCheckBox
            // 
            this.SameNameCheckBox.AutoSize = true;
            this.SameNameCheckBox.Location = new System.Drawing.Point(11, 62);
            this.SameNameCheckBox.Name = "SameNameCheckBox";
            this.SameNameCheckBox.Size = new System.Drawing.Size(261, 17);
            this.SameNameCheckBox.TabIndex = 9;
            this.SameNameCheckBox.Text = "Одинаковый текст в шапке для всех режимов";
            this.SameNameCheckBox.UseVisualStyleBackColor = true;
            this.SameNameCheckBox.CheckedChanged += new System.EventHandler(this.SameNameCheckBox_CheckedChanged);
            // 
            // VmarkCheckBox
            // 
            this.VmarkCheckBox.AutoSize = true;
            this.VmarkCheckBox.Location = new System.Drawing.Point(12, 115);
            this.VmarkCheckBox.Name = "VmarkCheckBox";
            this.VmarkCheckBox.Size = new System.Drawing.Size(180, 17);
            this.VmarkCheckBox.TabIndex = 10;
            this.VmarkCheckBox.Text = "Особая подпись для скорости";
            this.VmarkCheckBox.UseVisualStyleBackColor = true;
            this.VmarkCheckBox.CheckedChanged += new System.EventHandler(this.VmarkCheckBox_CheckedChanged);
            // 
            // RecordName
            // 
            this.RecordName.HeaderText = "Режим";
            this.RecordName.Name = "RecordName";
            this.RecordName.ReadOnly = true;
            this.RecordName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // G
            // 
            this.G.HeaderText = "G [тонн]";
            this.G.Name = "G";
            this.G.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G.Width = 40;
            // 
            // XT
            // 
            this.XT.HeaderText = "XT    [%САХ]";
            this.XT.Name = "XT";
            this.XT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.XT.Width = 50;
            // 
            // DZ
            // 
            this.DZ.HeaderText = "DZ [град]";
            this.DZ.Name = "DZ";
            this.DZ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DZ.Width = 40;
            // 
            // DP
            // 
            this.DP.HeaderText = "DP [град]";
            this.DP.Name = "DP";
            this.DP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DP.Width = 40;
            // 
            // Vpr
            // 
            this.Vpr.HeaderText = "Vpr [км/ч]";
            this.Vpr.Name = "Vpr";
            this.Vpr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Vpr.Width = 40;
            // 
            // Wz
            // 
            this.Wz.HeaderText = "Wz [м/с]";
            this.Wz.Name = "Wz";
            this.Wz.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Wz.Width = 40;
            // 
            // DopText
            // 
            this.DopText.HeaderText = "Доп. текст";
            this.DopText.Name = "DopText";
            this.DopText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DopText.Width = 50;
            // 
            // ParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 460);
            this.ControlBox = false;
            this.Controls.Add(this.VmarkCheckBox);
            this.Controls.Add(this.SameNameCheckBox);
            this.Controls.Add(this.VmarkTextBox);
            this.Controls.Add(this.FlightsNameTextBox);
            this.Controls.Add(this.PlaneNameTextBox);
            this.Controls.Add(this.PlaneName);
            this.Controls.Add(this.UserDataTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ParamsForm";
            this.Text = "ParamsForm";
            ((System.ComponentModel.ISupportInitialize)(this.UserDataTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView UserDataTable;
        private System.Windows.Forms.Label PlaneName;
        private System.Windows.Forms.TextBox PlaneNameTextBox;
        private System.Windows.Forms.TextBox FlightsNameTextBox;
        private System.Windows.Forms.TextBox VmarkTextBox;
        private System.Windows.Forms.CheckBox SameNameCheckBox;
        private System.Windows.Forms.CheckBox VmarkCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordName;
        private System.Windows.Forms.DataGridViewTextBoxColumn G;
        private System.Windows.Forms.DataGridViewTextBoxColumn XT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn DP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vpr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wz;
        private System.Windows.Forms.DataGridViewTextBoxColumn DopText;

    }
}