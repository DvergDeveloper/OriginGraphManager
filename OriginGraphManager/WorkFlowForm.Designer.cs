namespace OriginGraphManager
{
    partial class WorkFlowForm
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
            this.actionButton = new System.Windows.Forms.Button();
            this.ActionChooseLabel = new System.Windows.Forms.Label();
            this.actionComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.workPathTextBox = new System.Windows.Forms.TextBox();
            this.ChooseFileButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // actionButton
            // 
            this.actionButton.Location = new System.Drawing.Point(658, 22);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(75, 23);
            this.actionButton.TabIndex = 11;
            this.actionButton.Text = "Выполнить";
            this.actionButton.UseVisualStyleBackColor = true;
            this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
            // 
            // ActionChooseLabel
            // 
            this.ActionChooseLabel.AutoSize = true;
            this.ActionChooseLabel.Location = new System.Drawing.Point(219, 85);
            this.ActionChooseLabel.Name = "ActionChooseLabel";
            this.ActionChooseLabel.Size = new System.Drawing.Size(93, 13);
            this.ActionChooseLabel.TabIndex = 10;
            this.ActionChooseLabel.Text = "Выбор действия:";
            // 
            // actionComboBox
            // 
            this.actionComboBox.FormattingEnabled = true;
            this.actionComboBox.Items.AddRange(new object[] {
            "Выполнить все",
            "Масштабировать Y",
            "Масштабировать Y и X"});
            this.actionComboBox.Location = new System.Drawing.Point(209, 101);
            this.actionComboBox.Name = "actionComboBox";
            this.actionComboBox.Size = new System.Drawing.Size(121, 21);
            this.actionComboBox.TabIndex = 9;
            this.actionComboBox.SelectedIndexChanged += new System.EventHandler(this.actionComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(347, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Рабочай папка:";
            // 
            // workPathTextBox
            // 
            this.workPathTextBox.Location = new System.Drawing.Point(129, 25);
            this.workPathTextBox.Multiline = true;
            this.workPathTextBox.Name = "workPathTextBox";
            this.workPathTextBox.Size = new System.Drawing.Size(523, 36);
            this.workPathTextBox.TabIndex = 7;
            // 
            // ChooseFileButton
            // 
            this.ChooseFileButton.Location = new System.Drawing.Point(12, 14);
            this.ChooseFileButton.Name = "ChooseFileButton";
            this.ChooseFileButton.Size = new System.Drawing.Size(111, 39);
            this.ChooseFileButton.TabIndex = 6;
            this.ChooseFileButton.Text = "Выбрать opj файл рабочей папки";
            this.ChooseFileButton.UseVisualStyleBackColor = true;
            this.ChooseFileButton.Click += new System.EventHandler(this.ChooseFileButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // WorkFlowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 70);
            this.ControlBox = false;
            this.Controls.Add(this.actionButton);
            this.Controls.Add(this.ActionChooseLabel);
            this.Controls.Add(this.actionComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.workPathTextBox);
            this.Controls.Add(this.ChooseFileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WorkFlowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WorkFlowForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.Label ActionChooseLabel;
        private System.Windows.Forms.ComboBox actionComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox workPathTextBox;
        private System.Windows.Forms.Button ChooseFileButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}