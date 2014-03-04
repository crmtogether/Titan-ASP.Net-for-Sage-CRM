namespace SageCRM
{
    partial class FilterPropEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterPropEditor));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbEntityName = new System.Windows.Forms.ComboBox();
            this.lb_screens = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_entryblockname = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbl_entryblockname = new System.Windows.Forms.Label();
            this.lbl_allScreens = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbBlockTitle = new System.Windows.Forms.TextBox();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(443, 434);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(362, 434);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbEntityName
            // 
            this.cbEntityName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEntityName.FormattingEnabled = true;
            this.cbEntityName.Location = new System.Drawing.Point(135, 63);
            this.cbEntityName.Name = "cbEntityName";
            this.cbEntityName.Size = new System.Drawing.Size(366, 21);
            this.cbEntityName.TabIndex = 4;
            this.cbEntityName.SelectedValueChanged += new System.EventHandler(this.cbEntityName_SelectedValueChanged);
            // 
            // lb_screens
            // 
            this.lb_screens.FormattingEnabled = true;
            this.lb_screens.Location = new System.Drawing.Point(135, 118);
            this.lb_screens.Name = "lb_screens";
            this.lb_screens.Size = new System.Drawing.Size(366, 290);
            this.lb_screens.TabIndex = 7;
            this.lb_screens.SelectedValueChanged += new System.EventHandler(this.lb_screens_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Entity Name:";
            // 
            // tb_entryblockname
            // 
            this.tb_entryblockname.Location = new System.Drawing.Point(135, 92);
            this.tb_entryblockname.Name = "tb_entryblockname";
            this.tb_entryblockname.Size = new System.Drawing.Size(366, 20);
            this.tb_entryblockname.TabIndex = 16;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tb_entryblockname);
            this.groupBox4.Controls.Add(this.lbl_entryblockname);
            this.groupBox4.Controls.Add(this.lbl_allScreens);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.lb_screens);
            this.groupBox4.Controls.Add(this.tbBlockTitle);
            this.groupBox4.Controls.Add(this.cbEntityName);
            this.groupBox4.Location = new System.Drawing.Point(6, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(512, 420);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Properties";
            // 
            // lbl_entryblockname
            // 
            this.lbl_entryblockname.AutoSize = true;
            this.lbl_entryblockname.Location = new System.Drawing.Point(7, 92);
            this.lbl_entryblockname.Name = "lbl_entryblockname";
            this.lbl_entryblockname.Size = new System.Drawing.Size(92, 13);
            this.lbl_entryblockname.TabIndex = 15;
            this.lbl_entryblockname.Text = "Entry Block Name";
            // 
            // lbl_allScreens
            // 
            this.lbl_allScreens.AutoSize = true;
            this.lbl_allScreens.Location = new System.Drawing.Point(7, 118);
            this.lbl_allScreens.Name = "lbl_allScreens";
            this.lbl_allScreens.Size = new System.Drawing.Size(75, 13);
            this.lbl_allScreens.TabIndex = 9;
            this.lbl_allScreens.Text = "Entity Screens";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Block Title";
            // 
            // tbBlockTitle
            // 
            this.tbBlockTitle.Location = new System.Drawing.Point(135, 34);
            this.tbBlockTitle.Name = "tbBlockTitle";
            this.tbBlockTitle.Size = new System.Drawing.Size(366, 20);
            this.tbBlockTitle.TabIndex = 3;
            // 
            // FilterPropEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(523, 463);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterPropEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter Property Editor";
            this.Load += new System.EventHandler(this.FilterPropEditor_Load);
            this.Shown += new System.EventHandler(this.FilterPropEditor_Shown);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbEntityName;
        private System.Windows.Forms.ListBox lb_screens;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_entryblockname;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbl_entryblockname;
        private System.Windows.Forms.Label lbl_allScreens;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbBlockTitle;
    }
}