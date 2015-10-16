namespace SageCRM
{
    partial class ButtonPropEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ButtonPropEditor));
            this.lbl_caption = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tb_caption = new System.Windows.Forms.TextBox();
            this.cb_EntityName = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_urlscript = new System.Windows.Forms.TextBox();
            this.cb_target = new System.Windows.Forms.ComboBox();
            this.lbl_target = new System.Windows.Forms.Label();
            this.cb_permtype = new System.Windows.Forms.ComboBox();
            this.lbl_permissionstype = new System.Windows.Forms.Label();
            this.lbl_imagepreview = new System.Windows.Forms.Label();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.lbl_btnimages = new System.Windows.Forms.Label();
            this.lb_buttonimages = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_caption
            // 
            this.lbl_caption.AutoSize = true;
            this.lbl_caption.Location = new System.Drawing.Point(7, 16);
            this.lbl_caption.Name = "lbl_caption";
            this.lbl_caption.Size = new System.Drawing.Size(46, 13);
            this.lbl_caption.TabIndex = 28;
            this.lbl_caption.Text = "Caption:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Permissions Entity :";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(418, 366);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 29;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tb_caption
            // 
            this.tb_caption.Location = new System.Drawing.Point(116, 16);
            this.tb_caption.Name = "tb_caption";
            this.tb_caption.Size = new System.Drawing.Size(366, 20);
            this.tb_caption.TabIndex = 0;
            // 
            // cb_EntityName
            // 
            this.cb_EntityName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_EntityName.FormattingEnabled = true;
            this.cb_EntityName.Location = new System.Drawing.Point(117, 177);
            this.cb_EntityName.Name = "cb_EntityName";
            this.cb_EntityName.Size = new System.Drawing.Size(362, 21);
            this.cb_EntityName.TabIndex = 2;
            this.cb_EntityName.SelectedValueChanged += new System.EventHandler(this.cb_EntityName_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_urlscript);
            this.groupBox1.Controls.Add(this.cb_target);
            this.groupBox1.Controls.Add(this.lbl_target);
            this.groupBox1.Controls.Add(this.cb_permtype);
            this.groupBox1.Controls.Add(this.lbl_permissionstype);
            this.groupBox1.Controls.Add(this.lbl_imagepreview);
            this.groupBox1.Controls.Add(this.webBrowser2);
            this.groupBox1.Controls.Add(this.lbl_btnimages);
            this.groupBox1.Controls.Add(this.lb_buttonimages);
            this.groupBox1.Controls.Add(this.lbl_caption);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_caption);
            this.groupBox1.Controls.Add(this.cb_EntityName);
            this.groupBox1.Location = new System.Drawing.Point(5, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 346);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "URL/Script:";
            // 
            // tb_urlscript
            // 
            this.tb_urlscript.Location = new System.Drawing.Point(116, 265);
            this.tb_urlscript.Name = "tb_urlscript";
            this.tb_urlscript.Size = new System.Drawing.Size(363, 20);
            this.tb_urlscript.TabIndex = 5;
            // 
            // cb_target
            // 
            this.cb_target.FormattingEnabled = true;
            this.cb_target.Items.AddRange(new object[] {
            "",
            "_blank",
            "_self",
            "_parent",
            "_top"});
            this.cb_target.Location = new System.Drawing.Point(116, 237);
            this.cb_target.Name = "cb_target";
            this.cb_target.Size = new System.Drawing.Size(363, 21);
            this.cb_target.TabIndex = 4;
            this.cb_target.SelectedValueChanged += new System.EventHandler(this.cb_target_SelectedValueChanged);
            // 
            // lbl_target
            // 
            this.lbl_target.AutoSize = true;
            this.lbl_target.Location = new System.Drawing.Point(7, 236);
            this.lbl_target.Name = "lbl_target";
            this.lbl_target.Size = new System.Drawing.Size(41, 13);
            this.lbl_target.TabIndex = 35;
            this.lbl_target.Text = "Target:";
            // 
            // cb_permtype
            // 
            this.cb_permtype.FormattingEnabled = true;
            this.cb_permtype.ItemHeight = 13;
            this.cb_permtype.Items.AddRange(new object[] {
            "",
            "VIEW",
            "EDIT",
            "DELETE",
            "INSERT"});
            this.cb_permtype.Location = new System.Drawing.Point(116, 208);
            this.cb_permtype.Name = "cb_permtype";
            this.cb_permtype.Size = new System.Drawing.Size(363, 21);
            this.cb_permtype.TabIndex = 3;
            this.cb_permtype.SelectedValueChanged += new System.EventHandler(this.cb_permtype_SelectedValueChanged);
            // 
            // lbl_permissionstype
            // 
            this.lbl_permissionstype.AutoSize = true;
            this.lbl_permissionstype.Location = new System.Drawing.Point(7, 208);
            this.lbl_permissionstype.Name = "lbl_permissionstype";
            this.lbl_permissionstype.Size = new System.Drawing.Size(95, 13);
            this.lbl_permissionstype.TabIndex = 33;
            this.lbl_permissionstype.Text = "Permissions Type :";
            // 
            // lbl_imagepreview
            // 
            this.lbl_imagepreview.AutoSize = true;
            this.lbl_imagepreview.Location = new System.Drawing.Point(332, 48);
            this.lbl_imagepreview.Name = "lbl_imagepreview";
            this.lbl_imagepreview.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_imagepreview.Size = new System.Drawing.Size(77, 13);
            this.lbl_imagepreview.TabIndex = 32;
            this.lbl_imagepreview.Text = "Image Preview";
            // 
            // webBrowser2
            // 
            this.webBrowser2.Location = new System.Drawing.Point(332, 71);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.Size = new System.Drawing.Size(147, 98);
            this.webBrowser2.TabIndex = 31;
            // 
            // lbl_btnimages
            // 
            this.lbl_btnimages.AutoSize = true;
            this.lbl_btnimages.Location = new System.Drawing.Point(7, 48);
            this.lbl_btnimages.Name = "lbl_btnimages";
            this.lbl_btnimages.Size = new System.Drawing.Size(36, 13);
            this.lbl_btnimages.TabIndex = 30;
            this.lbl_btnimages.Text = "Image";
            // 
            // lb_buttonimages
            // 
            this.lb_buttonimages.FormattingEnabled = true;
            this.lb_buttonimages.Location = new System.Drawing.Point(117, 48);
            this.lb_buttonimages.Name = "lb_buttonimages";
            this.lb_buttonimages.Size = new System.Drawing.Size(202, 121);
            this.lb_buttonimages.TabIndex = 1;
            this.lb_buttonimages.SelectedIndexChanged += new System.EventHandler(this.lb_buttonimages_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(337, 366);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ButtonPropEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 395);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ButtonPropEditor";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Button Property Editor";
            this.Load += new System.EventHandler(this.ButtonPropEditor_Load);
            this.Shown += new System.EventHandler(this.ButtonPropEditor_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_caption;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tb_caption;
        private System.Windows.Forms.ComboBox cb_EntityName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_btnimages;
        private System.Windows.Forms.ListBox lb_buttonimages;
        private System.Windows.Forms.Label lbl_imagepreview;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.ComboBox cb_permtype;
        private System.Windows.Forms.Label lbl_permissionstype;
        private System.Windows.Forms.ComboBox cb_target;
        private System.Windows.Forms.Label lbl_target;
        private System.Windows.Forms.TextBox tb_urlscript;
        private System.Windows.Forms.Label label1;
    }
}