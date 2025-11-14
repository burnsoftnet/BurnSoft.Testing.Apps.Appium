namespace SampleUITestApp
{
    partial class frmMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.lblClickStatus = new System.Windows.Forms.Label();
            this.btnClickTest = new System.Windows.Forms.Button();
            this.tabOther = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.txtClickStatus = new System.Windows.Forms.TextBox();
            this.lblDatabaseServer = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtDatabaseServer = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabOther.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabOther);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(788, 423);
            this.tabControl1.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.AccessibleName = "tabMain";
            this.tabMain.Controls.Add(this.txtClickStatus);
            this.tabMain.Controls.Add(this.lblClickStatus);
            this.tabMain.Controls.Add(this.btnClickTest);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(780, 397);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // lblClickStatus
            // 
            this.lblClickStatus.AccessibleName = "lblClickStatus";
            this.lblClickStatus.AutoSize = true;
            this.lblClickStatus.Location = new System.Drawing.Point(22, 50);
            this.lblClickStatus.Name = "lblClickStatus";
            this.lblClickStatus.Size = new System.Drawing.Size(62, 13);
            this.lblClickStatus.TabIndex = 1;
            this.lblClickStatus.Text = "Not Clicked";
            // 
            // btnClickTest
            // 
            this.btnClickTest.AccessibleName = "btnClickTest";
            this.btnClickTest.Location = new System.Drawing.Point(22, 20);
            this.btnClickTest.Name = "btnClickTest";
            this.btnClickTest.Size = new System.Drawing.Size(75, 23);
            this.btnClickTest.TabIndex = 0;
            this.btnClickTest.Text = "Click Test";
            this.btnClickTest.UseVisualStyleBackColor = true;
            this.btnClickTest.Click += new System.EventHandler(this.btnClickTest_Click);
            // 
            // tabOther
            // 
            this.tabOther.AccessibleName = "tabOther";
            this.tabOther.Controls.Add(this.btnSave);
            this.tabOther.Controls.Add(this.txtPassword);
            this.tabOther.Controls.Add(this.txtUserName);
            this.tabOther.Controls.Add(this.txtDatabaseServer);
            this.tabOther.Controls.Add(this.lblPassword);
            this.tabOther.Controls.Add(this.lblUserName);
            this.tabOther.Controls.Add(this.lblDatabaseServer);
            this.tabOther.Location = new System.Drawing.Point(4, 22);
            this.tabOther.Name = "tabOther";
            this.tabOther.Padding = new System.Windows.Forms.Padding(3);
            this.tabOther.Size = new System.Drawing.Size(780, 397);
            this.tabOther.TabIndex = 1;
            this.tabOther.Text = "Other";
            this.tabOther.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.AccessibleName = "mnuFile";
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuSave
            // 
            this.mnuSave.AccessibleName = "mnuSave";
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(98, 22);
            this.mnuSave.Text = "&Save";
            // 
            // mnuExit
            // 
            this.mnuExit.AccessibleName = "mnuExit";
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(98, 22);
            this.mnuExit.Text = "&Exit";
            // 
            // txtClickStatus
            // 
            this.txtClickStatus.AccessibleName = "txtClickStatus";
            this.txtClickStatus.Location = new System.Drawing.Point(25, 75);
            this.txtClickStatus.Name = "txtClickStatus";
            this.txtClickStatus.Size = new System.Drawing.Size(100, 20);
            this.txtClickStatus.TabIndex = 2;
            this.txtClickStatus.Text = "Not Clicked";
            // 
            // lblDatabaseServer
            // 
            this.lblDatabaseServer.AccessibleName = "lblDatabaseServer";
            this.lblDatabaseServer.AutoSize = true;
            this.lblDatabaseServer.Location = new System.Drawing.Point(8, 23);
            this.lblDatabaseServer.Name = "lblDatabaseServer";
            this.lblDatabaseServer.Size = new System.Drawing.Size(90, 13);
            this.lblDatabaseServer.TabIndex = 0;
            this.lblDatabaseServer.Text = "Database Server:";
            // 
            // lblUserName
            // 
            this.lblUserName.AccessibleName = "lblUserName";
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(8, 49);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(60, 13);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "User Name";
            // 
            // lblPassword
            // 
            this.lblPassword.AccessibleName = "lblPassword";
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 77);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password:";
            // 
            // txtDatabaseServer
            // 
            this.txtDatabaseServer.AccessibleName = "txtDatabaseServer";
            this.txtDatabaseServer.Location = new System.Drawing.Point(104, 20);
            this.txtDatabaseServer.Name = "txtDatabaseServer";
            this.txtDatabaseServer.Size = new System.Drawing.Size(140, 20);
            this.txtDatabaseServer.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.AccessibleName = "txtUserName";
            this.txtUserName.Location = new System.Drawing.Point(104, 49);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(140, 20);
            this.txtUserName.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.AccessibleName = "txtPassword";
            this.txtPassword.Location = new System.Drawing.Point(104, 77);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(140, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleName = "btnSave";
            this.btnSave.Location = new System.Drawing.Point(15, 118);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Sample UI App For Testing";
            this.tabControl1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.tabOther.ResumeLayout(false);
            this.tabOther.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabOther;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.Label lblClickStatus;
        private System.Windows.Forms.Button btnClickTest;
        private System.Windows.Forms.TextBox txtClickStatus;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblDatabaseServer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtDatabaseServer;
        private System.Windows.Forms.Label lblPassword;
    }
}

