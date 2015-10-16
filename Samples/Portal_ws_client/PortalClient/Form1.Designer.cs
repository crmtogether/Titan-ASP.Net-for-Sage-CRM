namespace PortalClient
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_companyname = new System.Windows.Forms.Label();
            this.lbl_yourcompany = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_companyinfo = new System.Windows.Forms.TabPage();
            this.tp_cases = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbl_companyname_1 = new System.Windows.Forms.Label();
            this.lbl_website = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.lbl_caseinfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tp_companyinfo.SuspendLayout();
            this.tp_cases.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_companyname);
            this.panel1.Controls.Add(this.lbl_yourcompany);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 636);
            this.panel1.TabIndex = 0;
            // 
            // lbl_companyname
            // 
            this.lbl_companyname.AutoSize = true;
            this.lbl_companyname.Location = new System.Drawing.Point(15, 26);
            this.lbl_companyname.Name = "lbl_companyname";
            this.lbl_companyname.Size = new System.Drawing.Size(92, 13);
            this.lbl_companyname.TabIndex = 1;
            this.lbl_companyname.Text = "lbl_companyname";
            // 
            // lbl_yourcompany
            // 
            this.lbl_yourcompany.AutoSize = true;
            this.lbl_yourcompany.Location = new System.Drawing.Point(12, 9);
            this.lbl_yourcompany.Name = "lbl_yourcompany";
            this.lbl_yourcompany.Size = new System.Drawing.Size(51, 13);
            this.lbl_yourcompany.TabIndex = 0;
            this.lbl_yourcompany.Text = "Company";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.webBrowser1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(156, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(898, 124);
            this.panel2.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(898, 124);
            this.webBrowser1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tp_companyinfo);
            this.tabControl1.Controls.Add(this.tp_cases);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(156, 124);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(898, 512);
            this.tabControl1.TabIndex = 2;
            // 
            // tp_companyinfo
            // 
            this.tp_companyinfo.Controls.Add(this.webBrowser2);
            this.tp_companyinfo.Controls.Add(this.textBox2);
            this.tp_companyinfo.Controls.Add(this.lbl_website);
            this.tp_companyinfo.Controls.Add(this.lbl_companyname_1);
            this.tp_companyinfo.Controls.Add(this.textBox1);
            this.tp_companyinfo.Location = new System.Drawing.Point(4, 22);
            this.tp_companyinfo.Name = "tp_companyinfo";
            this.tp_companyinfo.Padding = new System.Windows.Forms.Padding(3);
            this.tp_companyinfo.Size = new System.Drawing.Size(890, 486);
            this.tp_companyinfo.TabIndex = 0;
            this.tp_companyinfo.Text = "Company Summary";
            this.tp_companyinfo.UseVisualStyleBackColor = true;
            // 
            // tp_cases
            // 
            this.tp_cases.Controls.Add(this.lbl_caseinfo);
            this.tp_cases.Controls.Add(this.dataGridView1);
            this.tp_cases.Location = new System.Drawing.Point(4, 22);
            this.tp_cases.Name = "tp_cases";
            this.tp_cases.Padding = new System.Windows.Forms.Padding(3);
            this.tp_cases.Size = new System.Drawing.Size(890, 486);
            this.tp_cases.TabIndex = 1;
            this.tp_cases.Text = "Cases";
            this.tp_cases.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(879, 455);
            this.dataGridView1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(327, 20);
            this.textBox1.TabIndex = 0;
            // 
            // lbl_companyname_1
            // 
            this.lbl_companyname_1.AutoSize = true;
            this.lbl_companyname_1.Location = new System.Drawing.Point(10, 24);
            this.lbl_companyname_1.Name = "lbl_companyname_1";
            this.lbl_companyname_1.Size = new System.Drawing.Size(85, 13);
            this.lbl_companyname_1.TabIndex = 1;
            this.lbl_companyname_1.Text = "Company Name:";
            // 
            // lbl_website
            // 
            this.lbl_website.AutoSize = true;
            this.lbl_website.Location = new System.Drawing.Point(10, 74);
            this.lbl_website.Name = "lbl_website";
            this.lbl_website.Size = new System.Drawing.Size(46, 13);
            this.lbl_website.TabIndex = 2;
            this.lbl_website.Text = "Website";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 99);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(327, 20);
            this.textBox2.TabIndex = 3;
            // 
            // webBrowser2
            // 
            this.webBrowser2.Location = new System.Drawing.Point(13, 139);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.Size = new System.Drawing.Size(869, 339);
            this.webBrowser2.TabIndex = 4;
            // 
            // lbl_caseinfo
            // 
            this.lbl_caseinfo.AutoSize = true;
            this.lbl_caseinfo.Location = new System.Drawing.Point(6, 12);
            this.lbl_caseinfo.Name = "lbl_caseinfo";
            this.lbl_caseinfo.Size = new System.Drawing.Size(35, 13);
            this.lbl_caseinfo.TabIndex = 2;
            this.lbl_caseinfo.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 636);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Sage CRM Portal Client Demo from www.crmtogether.com";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tp_companyinfo.ResumeLayout(false);
            this.tp_companyinfo.PerformLayout();
            this.tp_cases.ResumeLayout(false);
            this.tp_cases.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_companyinfo;
        private System.Windows.Forms.TabPage tp_cases;
        private System.Windows.Forms.Label lbl_companyname;
        private System.Windows.Forms.Label lbl_yourcompany;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_companyname_1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbl_website;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lbl_caseinfo;
    }
}

