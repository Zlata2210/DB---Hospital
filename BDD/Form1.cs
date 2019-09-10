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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "Host: ";
            textBox1.Text = "localhost";
            label2.Text = "Port";
            textBox2.Text = "5432";
            label3.Text = "User id";
            textBox3.Text = "postgres";
            label4.Text = "Password";
            textBox4.Text = "123";
            label5.Text = "DataBase";
            textBox5.Text = "Hospital";
            button1.Text = "Войти";
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String connectionString = "Host="+textBox1.Text + ";" +"Port="+ textBox2.Text + ";" + "User id=" + textBox3.Text + ";" + "Password=" + textBox4.Text + ";" + "DataBase=" + textBox5.Text + ";";
                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();
                Form0 new_form = new Form0(connectionString);
                new_form.Show();
                this.Hide();

            }
            catch
            {
                MessageBox.Show("ERROORR");
            }
        }
    }
}
