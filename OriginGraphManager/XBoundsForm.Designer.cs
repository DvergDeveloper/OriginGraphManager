namespace OriginGraphManager
{
    partial class XBoundsForm
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
            this.XBoundTypeBox = new System.Windows.Forms.ComboBox();
            this.StartParamFileLabel = new System.Windows.Forms.Label();
            this.FixXField = new System.Windows.Forms.GroupBox();
            this.XEndLabel = new System.Windows.Forms.Label();
            this.XEndTextBox = new System.Windows.Forms.TextBox();
            this.XStartLabel = new System.Windows.Forms.Label();
            this.XStartTextBox = new System.Windows.Forms.TextBox();
            this.ConditionsField = new System.Windows.Forms.GroupBox();
            this.EndField = new System.Windows.Forms.GroupBox();
            this.EndTargetTextBox = new System.Windows.Forms.TextBox();
            this.EndConditionTextBox = new System.Windows.Forms.TextBox();
            this.EndConditionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EndConditionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.EndConditionTypeLabel = new System.Windows.Forms.Label();
            this.EndParamNameComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.EndTypeComboBox = new System.Windows.Forms.ComboBox();
            this.EndParamFileLabel = new System.Windows.Forms.Label();
            this.StartField = new System.Windows.Forms.GroupBox();
            this.StartTargetTextBox = new System.Windows.Forms.TextBox();
            this.StartConditionTextBox = new System.Windows.Forms.TextBox();
            this.StartConditionLabel = new System.Windows.Forms.Label();
            this.StartTargetLabel = new System.Windows.Forms.Label();
            this.StartConditionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.StartConditionTypeLabel = new System.Windows.Forms.Label();
            this.StartParamNameComboBox = new System.Windows.Forms.ComboBox();
            this.StartParamLabel = new System.Windows.Forms.Label();
            this.StartTypeComboBox = new System.Windows.Forms.ComboBox();
            this.BoundsTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.CorrectionCheckBox = new System.Windows.Forms.CheckBox();
            this.FixXField.SuspendLayout();
            this.ConditionsField.SuspendLayout();
            this.EndField.SuspendLayout();
            this.StartField.SuspendLayout();
            this.BoundsTypeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // XBoundTypeBox
            // 
            this.XBoundTypeBox.FormattingEnabled = true;
            this.XBoundTypeBox.Items.AddRange(new object[] {
            "Заданные границы",
            "Границы по условию"});
            this.XBoundTypeBox.Location = new System.Drawing.Point(24, 30);
            this.XBoundTypeBox.Name = "XBoundTypeBox";
            this.XBoundTypeBox.Size = new System.Drawing.Size(159, 21);
            this.XBoundTypeBox.TabIndex = 0;
            this.XBoundTypeBox.SelectedIndexChanged += new System.EventHandler(this.XBoundTypeBox_SelectedIndexChanged);
            // 
            // StartParamFileLabel
            // 
            this.StartParamFileLabel.AutoSize = true;
            this.StartParamFileLabel.Location = new System.Drawing.Point(13, 35);
            this.StartParamFileLabel.Name = "StartParamFileLabel";
            this.StartParamFileLabel.Size = new System.Drawing.Size(41, 13);
            this.StartParamFileLabel.TabIndex = 8;
            this.StartParamFileLabel.Text = "Канал:";
            // 
            // FixXField
            // 
            this.FixXField.Controls.Add(this.XEndLabel);
            this.FixXField.Controls.Add(this.XEndTextBox);
            this.FixXField.Controls.Add(this.XStartLabel);
            this.FixXField.Controls.Add(this.XStartTextBox);
            this.FixXField.Enabled = false;
            this.FixXField.Location = new System.Drawing.Point(33, 104);
            this.FixXField.Name = "FixXField";
            this.FixXField.Size = new System.Drawing.Size(200, 64);
            this.FixXField.TabIndex = 10;
            this.FixXField.TabStop = false;
            this.FixXField.Text = "Задание фиксированных границ";
            // 
            // XEndLabel
            // 
            this.XEndLabel.AutoSize = true;
            this.XEndLabel.Location = new System.Drawing.Point(109, 18);
            this.XEndLabel.Name = "XEndLabel";
            this.XEndLabel.Size = new System.Drawing.Size(69, 13);
            this.XEndLabel.TabIndex = 9;
            this.XEndLabel.Text = "Конец оси Х";
            // 
            // XEndTextBox
            // 
            this.XEndTextBox.Location = new System.Drawing.Point(112, 34);
            this.XEndTextBox.Name = "XEndTextBox";
            this.XEndTextBox.Size = new System.Drawing.Size(66, 20);
            this.XEndTextBox.TabIndex = 8;
            this.XEndTextBox.TextChanged += new System.EventHandler(this.CheckIsFormFilled);
            this.XEndTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProtectFromIncorrectInput);
            // 
            // XStartLabel
            // 
            this.XStartLabel.AutoSize = true;
            this.XStartLabel.Location = new System.Drawing.Point(22, 18);
            this.XStartLabel.Name = "XStartLabel";
            this.XStartLabel.Size = new System.Drawing.Size(75, 13);
            this.XStartLabel.TabIndex = 7;
            this.XStartLabel.Text = "Начало оси Х";
            // 
            // XStartTextBox
            // 
            this.XStartTextBox.Location = new System.Drawing.Point(25, 34);
            this.XStartTextBox.Name = "XStartTextBox";
            this.XStartTextBox.Size = new System.Drawing.Size(72, 20);
            this.XStartTextBox.TabIndex = 6;
            this.XStartTextBox.TextChanged += new System.EventHandler(this.CheckIsFormFilled);
            this.XStartTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProtectFromIncorrectInput);
            // 
            // ConditionsField
            // 
            this.ConditionsField.Controls.Add(this.EndField);
            this.ConditionsField.Controls.Add(this.StartField);
            this.ConditionsField.Location = new System.Drawing.Point(12, 174);
            this.ConditionsField.Name = "ConditionsField";
            this.ConditionsField.Size = new System.Drawing.Size(246, 275);
            this.ConditionsField.TabIndex = 11;
            this.ConditionsField.TabStop = false;
            this.ConditionsField.Text = "Условия определнеия границ";
            // 
            // EndField
            // 
            this.EndField.Controls.Add(this.EndTargetTextBox);
            this.EndField.Controls.Add(this.EndConditionTextBox);
            this.EndField.Controls.Add(this.EndConditionLabel);
            this.EndField.Controls.Add(this.label2);
            this.EndField.Controls.Add(this.EndConditionTypeComboBox);
            this.EndField.Controls.Add(this.EndConditionTypeLabel);
            this.EndField.Controls.Add(this.EndParamNameComboBox);
            this.EndField.Controls.Add(this.label4);
            this.EndField.Controls.Add(this.EndTypeComboBox);
            this.EndField.Controls.Add(this.EndParamFileLabel);
            this.EndField.Location = new System.Drawing.Point(122, 19);
            this.EndField.Name = "EndField";
            this.EndField.Size = new System.Drawing.Size(110, 250);
            this.EndField.TabIndex = 21;
            this.EndField.TabStop = false;
            this.EndField.Text = "Условие конца отрезка";
            // 
            // EndTargetTextBox
            // 
            this.EndTargetTextBox.Location = new System.Drawing.Point(16, 172);
            this.EndTargetTextBox.Name = "EndTargetTextBox";
            this.EndTargetTextBox.Size = new System.Drawing.Size(72, 20);
            this.EndTargetTextBox.TabIndex = 21;
            this.EndTargetTextBox.TextChanged += new System.EventHandler(this.CheckIsFormFilled);
            this.EndTargetTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProtectFromIncorrectInput);
            // 
            // EndConditionTextBox
            // 
            this.EndConditionTextBox.Location = new System.Drawing.Point(19, 213);
            this.EndConditionTextBox.Name = "EndConditionTextBox";
            this.EndConditionTextBox.Size = new System.Drawing.Size(72, 20);
            this.EndConditionTextBox.TabIndex = 20;
            this.EndConditionTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BanInput);
            // 
            // EndConditionLabel
            // 
            this.EndConditionLabel.AutoSize = true;
            this.EndConditionLabel.Location = new System.Drawing.Point(16, 196);
            this.EndConditionLabel.Name = "EndConditionLabel";
            this.EndConditionLabel.Size = new System.Drawing.Size(54, 13);
            this.EndConditionLabel.TabIndex = 19;
            this.EndConditionLabel.Text = "Условие:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Цель:";
            // 
            // EndConditionTypeComboBox
            // 
            this.EndConditionTypeComboBox.FormattingEnabled = true;
            this.EndConditionTypeComboBox.Items.AddRange(new object[] {
            ">",
            "<"});
            this.EndConditionTypeComboBox.Location = new System.Drawing.Point(16, 132);
            this.EndConditionTypeComboBox.Name = "EndConditionTypeComboBox";
            this.EndConditionTypeComboBox.Size = new System.Drawing.Size(72, 21);
            this.EndConditionTypeComboBox.TabIndex = 16;
            this.EndConditionTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.EndConditionTypeComboBox_SelectedIndexChanged);
            // 
            // EndConditionTypeLabel
            // 
            this.EndConditionTypeLabel.AutoSize = true;
            this.EndConditionTypeLabel.Location = new System.Drawing.Point(13, 116);
            this.EndConditionTypeLabel.Name = "EndConditionTypeLabel";
            this.EndConditionTypeLabel.Size = new System.Drawing.Size(73, 13);
            this.EndConditionTypeLabel.TabIndex = 15;
            this.EndConditionTypeLabel.Text = "Тип условия:";
            // 
            // EndParamNameComboBox
            // 
            this.EndParamNameComboBox.FormattingEnabled = true;
            this.EndParamNameComboBox.Location = new System.Drawing.Point(16, 92);
            this.EndParamNameComboBox.Name = "EndParamNameComboBox";
            this.EndParamNameComboBox.Size = new System.Drawing.Size(72, 21);
            this.EndParamNameComboBox.TabIndex = 14;
            this.EndParamNameComboBox.SelectedIndexChanged += new System.EventHandler(this.EndParamNameComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Параметр:";
            // 
            // EndTypeComboBox
            // 
            this.EndTypeComboBox.FormattingEnabled = true;
            this.EndTypeComboBox.Location = new System.Drawing.Point(16, 52);
            this.EndTypeComboBox.Name = "EndTypeComboBox";
            this.EndTypeComboBox.Size = new System.Drawing.Size(72, 21);
            this.EndTypeComboBox.TabIndex = 12;
            this.EndTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.EndTypeComboBox_SelectedIndexChanged);
            // 
            // EndParamFileLabel
            // 
            this.EndParamFileLabel.AutoSize = true;
            this.EndParamFileLabel.Location = new System.Drawing.Point(13, 35);
            this.EndParamFileLabel.Name = "EndParamFileLabel";
            this.EndParamFileLabel.Size = new System.Drawing.Size(41, 13);
            this.EndParamFileLabel.TabIndex = 8;
            this.EndParamFileLabel.Text = "Канал:";
            // 
            // StartField
            // 
            this.StartField.Controls.Add(this.StartTargetTextBox);
            this.StartField.Controls.Add(this.StartConditionTextBox);
            this.StartField.Controls.Add(this.StartConditionLabel);
            this.StartField.Controls.Add(this.StartTargetLabel);
            this.StartField.Controls.Add(this.StartConditionTypeComboBox);
            this.StartField.Controls.Add(this.StartConditionTypeLabel);
            this.StartField.Controls.Add(this.StartParamNameComboBox);
            this.StartField.Controls.Add(this.StartParamLabel);
            this.StartField.Controls.Add(this.StartTypeComboBox);
            this.StartField.Controls.Add(this.StartParamFileLabel);
            this.StartField.Location = new System.Drawing.Point(6, 19);
            this.StartField.Name = "StartField";
            this.StartField.Size = new System.Drawing.Size(110, 250);
            this.StartField.TabIndex = 0;
            this.StartField.TabStop = false;
            this.StartField.Text = "Условие начала отрезка";
            // 
            // StartTargetTextBox
            // 
            this.StartTargetTextBox.Location = new System.Drawing.Point(19, 172);
            this.StartTargetTextBox.Name = "StartTargetTextBox";
            this.StartTargetTextBox.Size = new System.Drawing.Size(72, 20);
            this.StartTargetTextBox.TabIndex = 21;
            this.StartTargetTextBox.TextChanged += new System.EventHandler(this.CheckIsFormFilled);
            this.StartTargetTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProtectFromIncorrectInput);
            // 
            // StartConditionTextBox
            // 
            this.StartConditionTextBox.Location = new System.Drawing.Point(19, 213);
            this.StartConditionTextBox.Name = "StartConditionTextBox";
            this.StartConditionTextBox.Size = new System.Drawing.Size(72, 20);
            this.StartConditionTextBox.TabIndex = 20;
            this.StartConditionTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BanInput);
            // 
            // StartConditionLabel
            // 
            this.StartConditionLabel.AutoSize = true;
            this.StartConditionLabel.Location = new System.Drawing.Point(16, 196);
            this.StartConditionLabel.Name = "StartConditionLabel";
            this.StartConditionLabel.Size = new System.Drawing.Size(54, 13);
            this.StartConditionLabel.TabIndex = 19;
            this.StartConditionLabel.Text = "Условие:";
            // 
            // StartTargetLabel
            // 
            this.StartTargetLabel.AutoSize = true;
            this.StartTargetLabel.Location = new System.Drawing.Point(13, 156);
            this.StartTargetLabel.Name = "StartTargetLabel";
            this.StartTargetLabel.Size = new System.Drawing.Size(36, 13);
            this.StartTargetLabel.TabIndex = 17;
            this.StartTargetLabel.Text = "Цель:";
            // 
            // StartConditionTypeComboBox
            // 
            this.StartConditionTypeComboBox.FormattingEnabled = true;
            this.StartConditionTypeComboBox.Items.AddRange(new object[] {
            ">",
            "<"});
            this.StartConditionTypeComboBox.Location = new System.Drawing.Point(16, 132);
            this.StartConditionTypeComboBox.Name = "StartConditionTypeComboBox";
            this.StartConditionTypeComboBox.Size = new System.Drawing.Size(72, 21);
            this.StartConditionTypeComboBox.TabIndex = 16;
            this.StartConditionTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.StartConditionTypeComboBox_SelectedIndexChanged);
            // 
            // StartConditionTypeLabel
            // 
            this.StartConditionTypeLabel.AutoSize = true;
            this.StartConditionTypeLabel.Location = new System.Drawing.Point(13, 116);
            this.StartConditionTypeLabel.Name = "StartConditionTypeLabel";
            this.StartConditionTypeLabel.Size = new System.Drawing.Size(73, 13);
            this.StartConditionTypeLabel.TabIndex = 15;
            this.StartConditionTypeLabel.Text = "Тип условия:";
            // 
            // StartParamNameComboBox
            // 
            this.StartParamNameComboBox.FormattingEnabled = true;
            this.StartParamNameComboBox.Location = new System.Drawing.Point(16, 92);
            this.StartParamNameComboBox.Name = "StartParamNameComboBox";
            this.StartParamNameComboBox.Size = new System.Drawing.Size(72, 21);
            this.StartParamNameComboBox.TabIndex = 14;
            this.StartParamNameComboBox.SelectedIndexChanged += new System.EventHandler(this.StartParamNameComboBox_SelectedIndexChanged);
            // 
            // StartParamLabel
            // 
            this.StartParamLabel.AutoSize = true;
            this.StartParamLabel.Location = new System.Drawing.Point(13, 76);
            this.StartParamLabel.Name = "StartParamLabel";
            this.StartParamLabel.Size = new System.Drawing.Size(61, 13);
            this.StartParamLabel.TabIndex = 13;
            this.StartParamLabel.Text = "Параметр:";
            // 
            // StartTypeComboBox
            // 
            this.StartTypeComboBox.FormattingEnabled = true;
            this.StartTypeComboBox.Location = new System.Drawing.Point(16, 52);
            this.StartTypeComboBox.Name = "StartTypeComboBox";
            this.StartTypeComboBox.Size = new System.Drawing.Size(72, 21);
            this.StartTypeComboBox.TabIndex = 12;
            this.StartTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.StartTypeComboBox_SelectedIndexChanged);
            // 
            // BoundsTypeGroupBox
            // 
            this.BoundsTypeGroupBox.Controls.Add(this.XBoundTypeBox);
            this.BoundsTypeGroupBox.Location = new System.Drawing.Point(33, 12);
            this.BoundsTypeGroupBox.Name = "BoundsTypeGroupBox";
            this.BoundsTypeGroupBox.Size = new System.Drawing.Size(200, 55);
            this.BoundsTypeGroupBox.TabIndex = 12;
            this.BoundsTypeGroupBox.TabStop = false;
            this.BoundsTypeGroupBox.Text = "Постоянные/переменные границы оси Х";
            // 
            // CorrectionCheckBox
            // 
            this.CorrectionCheckBox.AutoSize = true;
            this.CorrectionCheckBox.Location = new System.Drawing.Point(34, 73);
            this.CorrectionCheckBox.Name = "CorrectionCheckBox";
            this.CorrectionCheckBox.Size = new System.Drawing.Size(208, 17);
            this.CorrectionCheckBox.TabIndex = 15;
            this.CorrectionCheckBox.Text = "Нужно подкорректировать границы";
            this.CorrectionCheckBox.UseVisualStyleBackColor = true;
            this.CorrectionCheckBox.CheckedChanged += new System.EventHandler(this.CorrectionCheckBox_CheckedChanged);
            // 
            // XBoundsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 460);
            this.ControlBox = false;
            this.Controls.Add(this.CorrectionCheckBox);
            this.Controls.Add(this.BoundsTypeGroupBox);
            this.Controls.Add(this.ConditionsField);
            this.Controls.Add(this.FixXField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "XBoundsForm";
            this.Text = "XBoundsForm";
            this.FixXField.ResumeLayout(false);
            this.FixXField.PerformLayout();
            this.ConditionsField.ResumeLayout(false);
            this.EndField.ResumeLayout(false);
            this.EndField.PerformLayout();
            this.StartField.ResumeLayout(false);
            this.StartField.PerformLayout();
            this.BoundsTypeGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox XBoundTypeBox;
        private System.Windows.Forms.Label StartParamFileLabel;
        private System.Windows.Forms.GroupBox FixXField;
        private System.Windows.Forms.Label XEndLabel;
        private System.Windows.Forms.TextBox XEndTextBox;
        private System.Windows.Forms.Label XStartLabel;
        private System.Windows.Forms.TextBox XStartTextBox;
        private System.Windows.Forms.GroupBox ConditionsField;
        private System.Windows.Forms.GroupBox StartField;
        private System.Windows.Forms.Label StartConditionLabel;
        private System.Windows.Forms.Label StartTargetLabel;
        private System.Windows.Forms.ComboBox StartConditionTypeComboBox;
        private System.Windows.Forms.Label StartConditionTypeLabel;
        private System.Windows.Forms.ComboBox StartParamNameComboBox;
        private System.Windows.Forms.Label StartParamLabel;
        private System.Windows.Forms.ComboBox StartTypeComboBox;
        private System.Windows.Forms.GroupBox EndField;
        private System.Windows.Forms.Label EndConditionLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox EndConditionTypeComboBox;
        private System.Windows.Forms.Label EndConditionTypeLabel;
        private System.Windows.Forms.ComboBox EndParamNameComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox EndTypeComboBox;
        private System.Windows.Forms.Label EndParamFileLabel;
        private System.Windows.Forms.TextBox EndTargetTextBox;
        private System.Windows.Forms.TextBox StartTargetTextBox;
        private System.Windows.Forms.GroupBox BoundsTypeGroupBox;
        private System.Windows.Forms.TextBox EndConditionTextBox;
        private System.Windows.Forms.TextBox StartConditionTextBox;
        private System.Windows.Forms.CheckBox CorrectionCheckBox;
    }
}