namespace A_Automation.HealthCheck
{
    partial class DynamicPopup
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
            this.rbtn_DVT = new System.Windows.Forms.RadioButton();
            this.rbtn_QAC = new System.Windows.Forms.RadioButton();
            this.rbtn_STG = new System.Windows.Forms.RadioButton();
            this.rbtn_PRD = new System.Windows.Forms.RadioButton();
            this.btn_runScript = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtn_DVT
            // 
            this.rbtn_DVT.AutoSize = true;
            this.rbtn_DVT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtn_DVT.ForeColor = System.Drawing.SystemColors.Desktop;
            this.rbtn_DVT.Location = new System.Drawing.Point(48, 36);
            this.rbtn_DVT.Name = "rbtn_DVT";
            this.rbtn_DVT.Size = new System.Drawing.Size(46, 17);
            this.rbtn_DVT.TabIndex = 0;
            this.rbtn_DVT.Text = "DVT";
            this.rbtn_DVT.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtn_DVT.UseVisualStyleBackColor = true;
            this.rbtn_DVT.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbtn_QAC
            // 
            this.rbtn_QAC.AutoSize = true;
            this.rbtn_QAC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtn_QAC.ForeColor = System.Drawing.SystemColors.Desktop;
            this.rbtn_QAC.Location = new System.Drawing.Point(116, 36);
            this.rbtn_QAC.Name = "rbtn_QAC";
            this.rbtn_QAC.Size = new System.Drawing.Size(46, 17);
            this.rbtn_QAC.TabIndex = 1;
            this.rbtn_QAC.Text = "QAC";
            this.rbtn_QAC.UseVisualStyleBackColor = true;
            this.rbtn_QAC.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbtn_STG
            // 
            this.rbtn_STG.AutoSize = true;
            this.rbtn_STG.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtn_STG.ForeColor = System.Drawing.SystemColors.Desktop;
            this.rbtn_STG.Location = new System.Drawing.Point(178, 36);
            this.rbtn_STG.Name = "rbtn_STG";
            this.rbtn_STG.Size = new System.Drawing.Size(46, 17);
            this.rbtn_STG.TabIndex = 2;
            this.rbtn_STG.Text = "STG";
            this.rbtn_STG.UseVisualStyleBackColor = true;
            this.rbtn_STG.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rbtn_PRD
            // 
            this.rbtn_PRD.AutoSize = true;
            this.rbtn_PRD.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtn_PRD.ForeColor = System.Drawing.SystemColors.Desktop;
            this.rbtn_PRD.Location = new System.Drawing.Point(231, 36);
            this.rbtn_PRD.Name = "rbtn_PRD";
            this.rbtn_PRD.Size = new System.Drawing.Size(47, 17);
            this.rbtn_PRD.TabIndex = 3;
            this.rbtn_PRD.Text = "PRD";
            this.rbtn_PRD.UseVisualStyleBackColor = true;
            this.rbtn_PRD.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // btn_runScript
            // 
            this.btn_runScript.Location = new System.Drawing.Point(139, 220);
            this.btn_runScript.Name = "btn_runScript";
            this.btn_runScript.Size = new System.Drawing.Size(75, 23);
            this.btn_runScript.TabIndex = 9;
            this.btn_runScript.Text = "RunScript";
            this.btn_runScript.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(48, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 120);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(68, 83);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(98, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "View Password";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(68, 57);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(193, 20);
            this.textBox2.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(68, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(193, 20);
            this.textBox1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Username";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(324, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(33, 13);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Close";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // DynamicPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(369, 255);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_runScript);
            this.Controls.Add(this.rbtn_PRD);
            this.Controls.Add(this.rbtn_STG);
            this.Controls.Add(this.rbtn_QAC);
            this.Controls.Add(this.rbtn_DVT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DynamicPopup";
            this.Text = "DynamicPopup";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtn_DVT;
        private System.Windows.Forms.RadioButton rbtn_QAC;
        private System.Windows.Forms.RadioButton rbtn_STG;
        private System.Windows.Forms.RadioButton rbtn_PRD;
        private System.Windows.Forms.Button btn_runScript;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}