namespace SageCRM
{
    partial class TabPropEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabPropEditor));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_entryblockname = new System.Windows.Forms.Label();
            this.lbl_allScreens = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_tabgroupname = new System.Windows.Forms.TextBox();
            this.lb_screens = new System.Windows.Forms.ListBox();
            this.cbEntityName = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(419, 436);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(338, 436);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_entryblockname);
            this.groupBox1.Controls.Add(this.lbl_allScreens);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_tabgroupname);
            this.groupBox1.Controls.Add(this.lb_screens);
            this.groupBox1.Controls.Add(this.cbEntityName);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 415);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // lbl_entryblockname
            // 
            this.lbl_entryblockname.AutoSize = true;
            this.lbl_entryblockname.Location = new System.Drawing.Point(4, 63);
            this.lbl_entryblockname.Name = "lbl_entryblockname";
            this.lbl_entryblockname.Size = new System.Drawing.Size(89, 13);
            this.lbl_entryblockname.TabIndex = 28;
            this.lbl_entryblockname.Text = "Tab Group Name";
            // 
            // lbl_allScreens
            // 
            this.lbl_allScreens.AutoSize = true;
            this.lbl_allScreens.Location = new System.Drawing.Point(4, 89);
            this.lbl_allScreens.Name = "lbl_allScreens";
            this.lbl_allScreens.Size = new System.Drawing.Size(63, 13);
            this.lbl_allScreens.TabIndex = 27;
            this.lbl_allScreens.Text = "Tab Groups";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Entity Name:";
            // 
            // tb_tabgroupname
            // 
            this.tb_tabgroupname.Location = new System.Drawing.Point(115, 63);
            this.tb_tabgroupname.Name = "tb_tabgroupname";
            this.tb_tabgroupname.Size = new System.Drawing.Size(366, 20);
            this.tb_tabgroupname.TabIndex = 25;
            // 
            // lb_screens
            // 
            this.lb_screens.FormattingEnabled = true;
            this.lb_screens.Location = new System.Drawing.Point(115, 89);
            this.lb_screens.Name = "lb_screens";
            this.lb_screens.Size = new System.Drawing.Size(366, 251);
            this.lb_screens.TabIndex = 24;
            this.lb_screens.SelectedValueChanged += new System.EventHandler(this.lb_screens_SelectedValueChanged);
            // 
            // cbEntityName
            // 
            this.cbEntityName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEntityName.FormattingEnabled = true;
            this.cbEntityName.Location = new System.Drawing.Point(115, 31);
            this.cbEntityName.Name = "cbEntityName";
            this.cbEntityName.Size = new System.Drawing.Size(366, 21);
            this.cbEntityName.TabIndex = 23;
            this.cbEntityName.SelectedValueChanged += new System.EventHandler(this.cbEntityName_SelectedValueChanged_1);
            // 
            // TabPropEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 464);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TabPropEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tab Property Editor";
            this.Load += new System.EventHandler(this.TabPropEditor_Load);
            this.Shown += new System.EventHandler(this.TabPropEditor_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_entryblockname;
        private System.Windows.Forms.Label lbl_allScreens;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_tabgroupname;
        private System.Windows.Forms.ListBox lb_screens;
        private System.Windows.Forms.ComboBox cbEntityName;
    }
}