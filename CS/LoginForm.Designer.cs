namespace MultiUserOutlookSync
{
    partial class LoginForm
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
			this.components = new System.ComponentModel.Container();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dataSet1 = new MultiUserOutlookSync.DataSet1();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.usersTableAdapter = new MultiUserOutlookSync.DataSet1TableAdapters.UsersTableAdapter();
			((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.DataSource = this.usersBindingSource;
			this.comboBox1.DisplayMember = "FirstName";
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(12, 32);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(296, 21);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.ValueMember = "ID";
			// 
			// usersBindingSource
			// 
			this.usersBindingSource.DataMember = "Users";
			this.usersBindingSource.DataSource = this.dataSet1;
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "DataSet1";
			this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// simpleButton1
			// 
			this.simpleButton1.Location = new System.Drawing.Point(233, 83);
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size(75, 23);
			this.simpleButton1.TabIndex = 1;
			this.simpleButton1.Text = "Login";
			this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// usersTableAdapter
			// 
			this.usersTableAdapter.ClearBeforeFill = true;
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(320, 122);
			this.Controls.Add(this.simpleButton1);
			this.Controls.Add(this.comboBox1);
			this.Name = "LoginForm";
			this.Text = "Please select user name to log in";
			this.Load += new System.EventHandler(this.LoginForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource usersBindingSource;
        private MultiUserOutlookSync.DataSet1TableAdapters.UsersTableAdapter usersTableAdapter;
    }
}