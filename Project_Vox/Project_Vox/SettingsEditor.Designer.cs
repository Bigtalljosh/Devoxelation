namespace Devoxelation
{
    partial class SettingsEditor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu_one = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.windowTitle = new System.Windows.Forms.Label();
            this.windowMode = new System.Windows.Forms.Label();
            this.windowRes = new System.Windows.Forms.Label();
            this.resolutionSelect = new System.Windows.Forms.ComboBox();
            this.windowTitleBox = new System.Windows.Forms.TextBox();
            this.isFullScreen = new System.Windows.Forms.ComboBox();
            this.reload = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_one,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(357, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu_one
            // 
            this.menu_one.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadConfigToolStripMenuItem,
            this.saveConfigToolStripMenuItem,
            this.exitSettingsToolStripMenuItem});
            this.menu_one.Name = "menu_one";
            this.menu_one.Size = new System.Drawing.Size(37, 20);
            this.menu_one.Text = "File";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToUseToolStripMenuItem,
            this.creditsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // loadConfigToolStripMenuItem
            // 
            this.loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
            this.loadConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadConfigToolStripMenuItem.Text = "Load Config";
            // 
            // saveConfigToolStripMenuItem
            // 
            this.saveConfigToolStripMenuItem.Name = "saveConfigToolStripMenuItem";
            this.saveConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveConfigToolStripMenuItem.Text = "Save Config";
            // 
            // exitSettingsToolStripMenuItem
            // 
            this.exitSettingsToolStripMenuItem.Name = "exitSettingsToolStripMenuItem";
            this.exitSettingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitSettingsToolStripMenuItem.Text = "Exit Settings";
            this.exitSettingsToolStripMenuItem.Click += new System.EventHandler(this.exitSettingsToolStripMenuItem_Click);
            // 
            // howToUseToolStripMenuItem
            // 
            this.howToUseToolStripMenuItem.Name = "howToUseToolStripMenuItem";
            this.howToUseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.howToUseToolStripMenuItem.Text = "How To Use";
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.creditsToolStripMenuItem.Text = "Credits";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(336, 143);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.windowTitleBox);
            this.tabPage1.Controls.Add(this.isFullScreen);
            this.tabPage1.Controls.Add(this.resolutionSelect);
            this.tabPage1.Controls.Add(this.windowRes);
            this.tabPage1.Controls.Add(this.windowMode);
            this.tabPage1.Controls.Add(this.windowTitle);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(328, 117);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Screen Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(378, 139);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Reset to Defaults";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // windowTitle
            // 
            this.windowTitle.AutoSize = true;
            this.windowTitle.Location = new System.Drawing.Point(6, 18);
            this.windowTitle.Name = "windowTitle";
            this.windowTitle.Size = new System.Drawing.Size(75, 13);
            this.windowTitle.TabIndex = 0;
            this.windowTitle.Text = "Window Title :";
            // 
            // windowMode
            // 
            this.windowMode.AutoSize = true;
            this.windowMode.Location = new System.Drawing.Point(6, 44);
            this.windowMode.Name = "windowMode";
            this.windowMode.Size = new System.Drawing.Size(82, 13);
            this.windowMode.TabIndex = 1;
            this.windowMode.Text = "Window Mode :";
            // 
            // windowRes
            // 
            this.windowRes.AutoSize = true;
            this.windowRes.Location = new System.Drawing.Point(6, 71);
            this.windowRes.Name = "windowRes";
            this.windowRes.Size = new System.Drawing.Size(105, 13);
            this.windowRes.TabIndex = 2;
            this.windowRes.Text = "Window Resolution :";
            // 
            // resolutionSelect
            // 
            this.resolutionSelect.FormattingEnabled = true;
            this.resolutionSelect.Items.AddRange(new object[] {
            "640  x  360 ",
            "854  x  480",
            "960  x  540",
            "1024  x  576 ",
            "1280  x  720",
            "1366  x  768",
            "1600  x  900",
            "1920  x  1080",
            "2048  x  1152",
            "2560  x  1440"});
            this.resolutionSelect.Location = new System.Drawing.Point(140, 71);
            this.resolutionSelect.Name = "resolutionSelect";
            this.resolutionSelect.Size = new System.Drawing.Size(169, 21);
            this.resolutionSelect.TabIndex = 7;
            // 
            // windowTitleBox
            // 
            this.windowTitleBox.Location = new System.Drawing.Point(140, 18);
            this.windowTitleBox.Name = "windowTitleBox";
            this.windowTitleBox.Size = new System.Drawing.Size(169, 20);
            this.windowTitleBox.TabIndex = 9;
            this.windowTitleBox.Text = "\r\n";
            // 
            // isFullScreen
            // 
            this.isFullScreen.FormattingEnabled = true;
            this.isFullScreen.Items.AddRange(new object[] {
            "true",
            "false"});
            this.isFullScreen.Location = new System.Drawing.Point(140, 44);
            this.isFullScreen.Name = "isFullScreen";
            this.isFullScreen.Size = new System.Drawing.Size(169, 21);
            this.isFullScreen.TabIndex = 8;
            // 
            // reload
            // 
            this.reload.Location = new System.Drawing.Point(16, 186);
            this.reload.Name = "reload";
            this.reload.Size = new System.Drawing.Size(328, 23);
            this.reload.TabIndex = 2;
            this.reload.Text = "Save and Reload";
            this.reload.UseVisualStyleBackColor = true;
            this.reload.Click += new System.EventHandler(this.reload_Click);
            // 
            // SettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 220);
            this.Controls.Add(this.reload);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SettingsEditor";
            this.Text = "SettingsEditor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_one;
        private System.Windows.Forms.ToolStripMenuItem loadConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToUseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox windowTitleBox;
        private System.Windows.Forms.ComboBox isFullScreen;
        private System.Windows.Forms.ComboBox resolutionSelect;
        private System.Windows.Forms.Label windowRes;
        private System.Windows.Forms.Label windowMode;
        private System.Windows.Forms.Label windowTitle;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button reload;
    }
}