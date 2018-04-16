using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiUserOutlookSync
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.dataSet1.Users);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
			MainForm frm = new MainForm(Convert.ToInt32(comboBox1.SelectedValue), comboBox1.Text);
			frm.ShowDialog();
        }
    }
}