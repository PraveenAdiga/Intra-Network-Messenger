namespace Lan_messenger
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /*protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }*/

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.busyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.online_radiobutton = new System.Windows.Forms.RadioButton();
            this.offline_radioButton = new System.Windows.Forms.RadioButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(261, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.busyToolStripMenuItem,
            this.developersToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.fileToolStripMenuItem.Text = "Messenger";
            // 
            // busyToolStripMenuItem
            // 
            this.busyToolStripMenuItem.Name = "busyToolStripMenuItem";
            this.busyToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.busyToolStripMenuItem.Text = "Exit";
            this.busyToolStripMenuItem.Click += new System.EventHandler(this.busyToolStripMenuItem_Click);
            // 
            // developersToolStripMenuItem
            // 
            this.developersToolStripMenuItem.Name = "developersToolStripMenuItem";
            this.developersToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.developersToolStripMenuItem.Text = "Developers";
            this.developersToolStripMenuItem.Click += new System.EventHandler(this.developersToolStripMenuItem_Click);
            // 
            // online_radiobutton
            // 
            this.online_radiobutton.AutoSize = true;
            this.online_radiobutton.Checked = true;
            this.online_radiobutton.Location = new System.Drawing.Point(13, 36);
            this.online_radiobutton.Name = "online_radiobutton";
            this.online_radiobutton.Size = new System.Drawing.Size(55, 17);
            this.online_radiobutton.TabIndex = 1;
            this.online_radiobutton.TabStop = true;
            this.online_radiobutton.Text = "Online";
            this.online_radiobutton.UseVisualStyleBackColor = true;
            this.online_radiobutton.CheckedChanged += new System.EventHandler(this.online_radiobutton_CheckedChanged);
            // 
            // offline_radioButton
            // 
            this.offline_radioButton.AutoSize = true;
            this.offline_radioButton.Location = new System.Drawing.Point(83, 36);
            this.offline_radioButton.Name = "offline_radioButton";
            this.offline_radioButton.Size = new System.Drawing.Size(55, 17);
            this.offline_radioButton.TabIndex = 2;
            this.offline_radioButton.TabStop = true;
            this.offline_radioButton.Text = "Offline";
            this.offline_radioButton.UseVisualStyleBackColor = true;
            this.offline_radioButton.CheckedChanged += new System.EventHandler(this.offline_radioButton_CheckedChanged);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(13, 72);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(236, 325);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "NAME";
            this.columnHeader1.Width = 86;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP ADDRESS";
            this.columnHeader2.Width = 175;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(261, 418);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.offline_radioButton);
            this.Controls.Add(this.online_radiobutton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(500, 100);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Lan Messenger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem busyToolStripMenuItem;
        private System.Windows.Forms.RadioButton online_radiobutton;
        private System.Windows.Forms.RadioButton offline_radioButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem developersToolStripMenuItem;

    }
}

