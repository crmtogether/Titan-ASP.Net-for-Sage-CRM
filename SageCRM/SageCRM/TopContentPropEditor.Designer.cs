namespace SageCRM
{
    partial class TopContentPropEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopContentPropEditor));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbEntityName = new System.Windows.Forms.ComboBox();
            this.lb_screens = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_allScreens = new System.Windows.Forms.Label();
            this.lbl_entryblockname = new System.Windows.Forms.Label();
            this.tb_entryblockname = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb_entitywhere = new System.Windows.Forms.RichTextBox();
            this.lb_EntityWhere = new System.Windows.Forms.Label();
            this.tb_icon = new System.Windows.Forms.TextBox();
            this.lbl_icon = new System.Windows.Forms.Label();
            this.gb_entitytinfo = new System.Windows.Forms.GroupBox();
            this.lbl_entitycolumns = new System.Windows.Forms.Label();
            this.rtb_columnlist = new System.Windows.Forms.RichTextBox();
            this.tb_idcolumn = new System.Windows.Forms.TextBox();
            this.lbl_Entityidcolumn = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.gb_entitytinfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(812, 426);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(731, 426);
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
            this.cbEntityName.Location = new System.Drawing.Point(139, 30);
            this.cbEntityName.Name = "cbEntityName";
            this.cbEntityName.Size = new System.Drawing.Size(366, 21);
            this.cbEntityName.TabIndex = 4;
            this.cbEntityName.SelectedValueChanged += new System.EventHandler(this.cbEntityName_SelectedValueChanged);
            // 
            // lb_screens
            // 
            this.lb_screens.FormattingEnabled = true;
            this.lb_screens.Location = new System.Drawing.Point(139, 85);
            this.lb_screens.Name = "lb_screens";
            this.lb_screens.Size = new System.Drawing.Size(366, 225);
            this.lb_screens.TabIndex = 7;
            this.lb_screens.SelectedValueChanged += new System.EventHandler(this.lb_screens_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Entity Name:";
            // 
            // lbl_allScreens
            // 
            this.lbl_allScreens.AutoSize = true;
            this.lbl_allScreens.Location = new System.Drawing.Point(11, 85);
            this.lbl_allScreens.Name = "lbl_allScreens";
            this.lbl_allScreens.Size = new System.Drawing.Size(75, 13);
            this.lbl_allScreens.TabIndex = 9;
            this.lbl_allScreens.Text = "Entity Screens";
            // 
            // lbl_entryblockname
            // 
            this.lbl_entryblockname.AutoSize = true;
            this.lbl_entryblockname.Location = new System.Drawing.Point(11, 59);
            this.lbl_entryblockname.Name = "lbl_entryblockname";
            this.lbl_entryblockname.Size = new System.Drawing.Size(92, 13);
            this.lbl_entryblockname.TabIndex = 15;
            this.lbl_entryblockname.Text = "Entry Block Name";
            // 
            // tb_entryblockname
            // 
            this.tb_entryblockname.Location = new System.Drawing.Point(139, 59);
            this.tb_entryblockname.Name = "tb_entryblockname";
            this.tb_entryblockname.Size = new System.Drawing.Size(366, 20);
            this.tb_entryblockname.TabIndex = 16;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tb_entitywhere);
            this.groupBox4.Controls.Add(this.lb_EntityWhere);
            this.groupBox4.Controls.Add(this.tb_icon);
            this.groupBox4.Controls.Add(this.lbl_icon);
            this.groupBox4.Controls.Add(this.tb_entryblockname);
            this.groupBox4.Controls.Add(this.lbl_entryblockname);
            this.groupBox4.Controls.Add(this.lbl_allScreens);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.lb_screens);
            this.groupBox4.Controls.Add(this.cbEntityName);
            this.groupBox4.Location = new System.Drawing.Point(6, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(512, 420);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Properties";
            // 
            // tb_entitywhere
            // 
            this.tb_entitywhere.Location = new System.Drawing.Point(139, 351);
            this.tb_entitywhere.Name = "tb_entitywhere";
            this.tb_entitywhere.Size = new System.Drawing.Size(366, 63);
            this.tb_entitywhere.TabIndex = 20;
            this.tb_entitywhere.Text = "";
            // 
            // lb_EntityWhere
            // 
            this.lb_EntityWhere.AutoSize = true;
            this.lb_EntityWhere.Location = new System.Drawing.Point(10, 351);
            this.lb_EntityWhere.Name = "lb_EntityWhere";
            this.lb_EntityWhere.Size = new System.Drawing.Size(65, 13);
            this.lb_EntityWhere.TabIndex = 19;
            this.lb_EntityWhere.Text = "EntityWhere";
            // 
            // tb_icon
            // 
            this.tb_icon.Location = new System.Drawing.Point(139, 318);
            this.tb_icon.Name = "tb_icon";
            this.tb_icon.Size = new System.Drawing.Size(366, 20);
            this.tb_icon.TabIndex = 18;
            // 
            // lbl_icon
            // 
            this.lbl_icon.AutoSize = true;
            this.lbl_icon.Location = new System.Drawing.Point(11, 318);
            this.lbl_icon.Name = "lbl_icon";
            this.lbl_icon.Size = new System.Drawing.Size(31, 13);
            this.lbl_icon.TabIndex = 17;
            this.lbl_icon.Text = "Icon:";
            // 
            // gb_entitytinfo
            // 
            this.gb_entitytinfo.Controls.Add(this.lbl_entitycolumns);
            this.gb_entitytinfo.Controls.Add(this.rtb_columnlist);
            this.gb_entitytinfo.Controls.Add(this.tb_idcolumn);
            this.gb_entitytinfo.Controls.Add(this.lbl_Entityidcolumn);
            this.gb_entitytinfo.Location = new System.Drawing.Point(524, 0);
            this.gb_entitytinfo.Name = "gb_entitytinfo";
            this.gb_entitytinfo.Size = new System.Drawing.Size(369, 420);
            this.gb_entitytinfo.TabIndex = 22;
            this.gb_entitytinfo.TabStop = false;
            this.gb_entitytinfo.Text = "Entity Infomation";
            // 
            // lbl_entitycolumns
            // 
            this.lbl_entitycolumns.AutoSize = true;
            this.lbl_entitycolumns.Location = new System.Drawing.Point(6, 63);
            this.lbl_entitycolumns.Name = "lbl_entitycolumns";
            this.lbl_entitycolumns.Size = new System.Drawing.Size(61, 13);
            this.lbl_entitycolumns.TabIndex = 18;
            this.lbl_entitycolumns.Text = "Column List";
            // 
            // rtb_columnlist
            // 
            this.rtb_columnlist.Location = new System.Drawing.Point(99, 63);
            this.rtb_columnlist.Name = "rtb_columnlist";
            this.rtb_columnlist.Size = new System.Drawing.Size(264, 351);
            this.rtb_columnlist.TabIndex = 17;
            this.rtb_columnlist.Text = "";
            // 
            // tb_idcolumn
            // 
            this.tb_idcolumn.Location = new System.Drawing.Point(99, 34);
            this.tb_idcolumn.Name = "tb_idcolumn";
            this.tb_idcolumn.Size = new System.Drawing.Size(264, 20);
            this.tb_idcolumn.TabIndex = 14;
            // 
            // lbl_Entityidcolumn
            // 
            this.lbl_Entityidcolumn.AutoSize = true;
            this.lbl_Entityidcolumn.Location = new System.Drawing.Point(6, 34);
            this.lbl_Entityidcolumn.Name = "lbl_Entityidcolumn";
            this.lbl_Entityidcolumn.Size = new System.Drawing.Size(58, 13);
            this.lbl_Entityidcolumn.TabIndex = 16;
            this.lbl_Entityidcolumn.Text = "ID column:";
            // 
            // TopContentPropEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 457);
            this.Controls.Add(this.gb_entitytinfo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TopContentPropEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Top Content Screen Property Editor";
            this.Load += new System.EventHandler(this.FilterPropEditor_Load);
            this.Shown += new System.EventHandler(this.FilterPropEditor_Shown);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gb_entitytinfo.ResumeLayout(false);
            this.gb_entitytinfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbEntityName;
        private System.Windows.Forms.ListBox lb_screens;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_allScreens;
        private System.Windows.Forms.Label lbl_entryblockname;
        private System.Windows.Forms.TextBox tb_entryblockname;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tb_icon;
        private System.Windows.Forms.Label lbl_icon;
        private System.Windows.Forms.RichTextBox tb_entitywhere;
        private System.Windows.Forms.Label lb_EntityWhere;
        private System.Windows.Forms.GroupBox gb_entitytinfo;
        private System.Windows.Forms.Label lbl_entitycolumns;
        private System.Windows.Forms.RichTextBox rtb_columnlist;
        private System.Windows.Forms.TextBox tb_idcolumn;
        private System.Windows.Forms.Label lbl_Entityidcolumn;
    }
}