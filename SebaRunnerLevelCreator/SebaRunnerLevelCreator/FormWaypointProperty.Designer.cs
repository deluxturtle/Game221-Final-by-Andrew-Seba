namespace SebaRunnerLevelCreator
{
    partial class FormWaypointProperty
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.groupBoxWait = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxWaypointType = new System.Windows.Forms.ComboBox();
            this.textWaypointName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonCancel);
            this.groupBox1.Controls.Add(this.buttonApply);
            this.groupBox1.Controls.Add(this.groupBoxWait);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxWaypointType);
            this.groupBox1.Controls.Add(this.textWaypointName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 223);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Waypoint Properties";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(281, 187);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(16, 187);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 5;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            // 
            // groupBoxWait
            // 
            this.groupBoxWait.Location = new System.Drawing.Point(6, 74);
            this.groupBoxWait.Name = "groupBoxWait";
            this.groupBoxWait.Size = new System.Drawing.Size(350, 106);
            this.groupBoxWait.TabIndex = 4;
            this.groupBoxWait.TabStop = false;
            this.groupBoxWait.Text = "Wait Properties";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Waypoint Type:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxWaypointType
            // 
            this.comboBoxWaypointType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWaypointType.FormattingEnabled = true;
            this.comboBoxWaypointType.Items.AddRange(new object[] {
            "Wait",
            "Straight",
            "Bezier"});
            this.comboBoxWaypointType.Location = new System.Drawing.Point(98, 47);
            this.comboBoxWaypointType.Name = "comboBoxWaypointType";
            this.comboBoxWaypointType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxWaypointType.TabIndex = 2;
            this.comboBoxWaypointType.SelectedIndexChanged += new System.EventHandler(this.comboBoxWaypointType_SelectedIndexChanged);
            // 
            // textWaypointName
            // 
            this.textWaypointName.Location = new System.Drawing.Point(98, 20);
            this.textWaypointName.Name = "textWaypointName";
            this.textWaypointName.Size = new System.Drawing.Size(121, 20);
            this.textWaypointName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormWaypointProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 223);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormWaypointProperty";
            this.Text = "Waypoint Properties";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.GroupBox groupBoxWait;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox comboBoxWaypointType;
        public System.Windows.Forms.TextBox textWaypointName;
    }
}