namespace SageCRM
{
    partial class DataSourcePropEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSourcePropEditor));
            this.tb_idcolumn = new System.Windows.Forms.TextBox();
            this.lbl_Entityidcolumn = new System.Windows.Forms.Label();
            this.gb_mainpropeerties = new System.Windows.Forms.GroupBox();
            this.lbl_entitycolumns = new System.Windows.Forms.Label();
            this.rtb_columnlist = new System.Windows.Forms.RichTextBox();
            this.lbl_whereclause = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbEntityName = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtb_selectsql = new System.Windows.Forms.RichTextBox();
            this.rtb_whereclause = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gb_mainpropeerties.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_idcolumn
            // 
            this.tb_idcolumn.Location = new System.Drawing.Point(96, 35);
            this.tb_idcolumn.Name = "tb_idcolumn";
            this.tb_idcolumn.Size = new System.Drawing.Size(264, 20);
            this.tb_idcolumn.TabIndex = 14;
            // 
            // lbl_Entityidcolumn
            // 
            this.lbl_Entityidcolumn.AutoSize = true;
            this.lbl_Entityidcolumn.Location = new System.Drawing.Point(6, 35);
            this.lbl_Entityidcolumn.Name = "lbl_Entityidcolumn";
            this.lbl_Entityidcolumn.Size = new System.Drawing.Size(58, 13);
            this.lbl_Entityidcolumn.TabIndex = 16;
            this.lbl_Entityidcolumn.Text = "ID column:";
            // 
            // gb_mainpropeerties
            // 
            this.gb_mainpropeerties.Controls.Add(this.lbl_entitycolumns);
            this.gb_mainpropeerties.Controls.Add(this.rtb_columnlist);
            this.gb_mainpropeerties.Controls.Add(this.tb_idcolumn);
            this.gb_mainpropeerties.Controls.Add(this.lbl_Entityidcolumn);
            this.gb_mainpropeerties.Location = new System.Drawing.Point(379, 10);
            this.gb_mainpropeerties.Name = "gb_mainpropeerties";
            this.gb_mainpropeerties.Size = new System.Drawing.Size(369, 391);
            this.gb_mainpropeerties.TabIndex = 17;
            this.gb_mainpropeerties.TabStop = false;
            this.gb_mainpropeerties.Text = "Entity Infomation";
            // 
            // lbl_entitycolumns
            // 
            this.lbl_entitycolumns.AutoSize = true;
            this.lbl_entitycolumns.Location = new System.Drawing.Point(6, 69);
            this.lbl_entitycolumns.Name = "lbl_entitycolumns";
            this.lbl_entitycolumns.Size = new System.Drawing.Size(61, 13);
            this.lbl_entitycolumns.TabIndex = 18;
            this.lbl_entitycolumns.Text = "Column List";
            // 
            // rtb_columnlist
            // 
            this.rtb_columnlist.Location = new System.Drawing.Point(96, 69);
            this.rtb_columnlist.Name = "rtb_columnlist";
            this.rtb_columnlist.Size = new System.Drawing.Size(264, 312);
            this.rtb_columnlist.TabIndex = 17;
            this.rtb_columnlist.Text = "";
            // 
            // lbl_whereclause
            // 
            this.lbl_whereclause.AutoSize = true;
            this.lbl_whereclause.Location = new System.Drawing.Point(9, 69);
            this.lbl_whereclause.Name = "lbl_whereclause";
            this.lbl_whereclause.Size = new System.Drawing.Size(77, 13);
            this.lbl_whereclause.TabIndex = 21;
            this.lbl_whereclause.Text = "Where Clause:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Entity Name:";
            // 
            // cbEntityName
            // 
            this.cbEntityName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEntityName.FormattingEnabled = true;
            this.cbEntityName.Location = new System.Drawing.Point(104, 35);
            this.cbEntityName.Name = "cbEntityName";
            this.cbEntityName.Size = new System.Drawing.Size(260, 21);
            this.cbEntityName.TabIndex = 19;
            this.cbEntityName.SelectedValueChanged += new System.EventHandler(this.cbEntityName_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rtb_selectsql);
            this.groupBox1.Controls.Add(this.rtb_whereclause);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lbl_whereclause);
            this.groupBox1.Controls.Add(this.cbEntityName);
            this.groupBox1.Location = new System.Drawing.Point(3, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 391);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main Properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Select sql";
            // 
            // rtb_selectsql
            // 
            this.rtb_selectsql.Location = new System.Drawing.Point(104, 228);
            this.rtb_selectsql.Name = "rtb_selectsql";
            this.rtb_selectsql.Size = new System.Drawing.Size(260, 153);
            this.rtb_selectsql.TabIndex = 22;
            this.rtb_selectsql.Text = "";
            // 
            // rtb_whereclause
            // 
            this.rtb_whereclause.Location = new System.Drawing.Point(104, 69);
            this.rtb_whereclause.Name = "rtb_whereclause";
            this.rtb_whereclause.Size = new System.Drawing.Size(260, 153);
            this.rtb_whereclause.TabIndex = 21;
            this.rtb_whereclause.Text = "";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(673, 407);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(592, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataSourcePropEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 435);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_mainpropeerties);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataSourcePropEditor";
            this.ShowIcon = false;
            this.Text = "DataSourcePropEditor";
            this.Load += new System.EventHandler(this.DataSourcePropEditor_Load);
            this.Shown += new System.EventHandler(this.DataSourcePropEditor_Shown);
            this.gb_mainpropeerties.ResumeLayout(false);
            this.gb_mainpropeerties.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tb_idcolumn;
        private System.Windows.Forms.Label lbl_Entityidcolumn;
        private System.Windows.Forms.GroupBox gb_mainpropeerties;
        private System.Windows.Forms.Label lbl_entitycolumns;
        private System.Windows.Forms.RichTextBox rtb_columnlist;
        private System.Windows.Forms.Label lbl_whereclause;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbEntityName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtb_whereclause;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtb_selectsql;
    }
}