using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace BDD
{
    public partial class Form2 : Form
    {
        string connectionString;
        public Form2(string connectionString)
        {
            InitializeComponent();
            label1.Text = "Login";
            label2.Text = "Password";
            button1.Text = "Войти";
            textBox2.UseSystemPasswordChar = true;
            this.connectionString = connectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if ((textBox1.Text == "1") & (textBox2.Text == "1"))
            {

                Admin new_form = new Admin(connectionString);
                new_form.Show();
                this.Hide();
            }
            
            else
            {
                Error error = new Error();
                error.Show();
                this.Hide();
            }
        }
    }
}
