using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDD
{
    public partial class Form0 : Form
    {
        string connectionString;
        public Form0(String connectionString)
        {
            InitializeComponent();
            button1.Text = "USER";
            button2.Text = "ADMIN";

            this.connectionString = connectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enteruser new_form = new enteruser(connectionString);
            new_form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 admin = new Form2(connectionString);
            admin.Show();
            this.Hide();
        }
    }
}
