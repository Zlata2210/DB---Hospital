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
    public partial class enteruser : Form
    {
        string connectionString;
        public enteruser(String connectionString)
        {
            InitializeComponent();
            label1.Text = "Login";
            label2.Text = "Password";
            button1.Text = "Войти";
            textBox2.UseSystemPasswordChar = true;
            this.connectionString = connectionString;
        }
        string num_id;
        string num_polis;
        public void enter()
        {
            NpgsqlConnection con = new NpgsqlConnection(connectionString);
            string check = "SELECT \"Номер_паспорта\" FROM Пациент WHERE ((Пациент.Номер_паспорта) = '" + textBox1.Text + "') ";
            NpgsqlCommand command = new NpgsqlCommand(check, con);
            con.Open();
            NpgsqlDataReader reader;
            reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                try
                {
                    num_id = reader.GetString(0);
                }
                catch { break; }
            }
            con.Close();
            con.Open();
            string check1 = "SELECT \"Номер_полиса\" FROM Пациент WHERE ((Пациент.Номер_полиса) = '" + textBox2.Text + "') ";
            NpgsqlCommand command1 = new NpgsqlCommand(check1, con);
            NpgsqlDataReader reader1;
            reader1 = command1.ExecuteReader();
            
            while (reader1.Read())
            {
                try
                {
                    num_polis = reader1.GetString(0);
                    User new_form = new User(connectionString, num_id);
                    new_form.Show();
                    this.Hide();

                }
                catch { break; }
            }
            con.Close();

        }


        int num;
        public void nnum()
        {

            NpgsqlConnection con = new NpgsqlConnection(connectionString);
            string check22 = "SELECT \"Код сотрудника\" FROM Сотрудники WHERE ((Сотрудники.Номер_телефона) = '" + textBox1.Text + "') ";
            NpgsqlCommand command22 = new NpgsqlCommand(check22, con);
            con.Open();
            NpgsqlDataReader readerr2;
            readerr2 = command22.ExecuteReader();
            while (readerr2.Read())
            {
                try
                {
                    num = readerr2.GetInt32(0);
                }
                catch { break; }
            }
            con.Close();

        }

        public void sotr()
        {
            NpgsqlConnection con = new NpgsqlConnection(connectionString);
            string check2 = "SELECT \"Номер_телефона\" FROM Сотрудники WHERE ((Сотрудники.Номер_телефона) = '" + textBox1.Text + "') ";
            NpgsqlCommand command2 = new NpgsqlCommand(check2, con);
            con.Open();
            NpgsqlDataReader reader2;
            reader2 = command2.ExecuteReader();
            string num_phone = "";
            while (reader2.Read())
            {
                try
                {
                    num_phone = reader2.GetString(0);
                }
                catch { break; }
            }
            con.Close();
            string check3 = "SELECT \"Должность\" FROM Сотрудники WHERE ((Сотрудники.Должность) = '" + textBox2.Text + "') ";
            NpgsqlCommand command3 = new NpgsqlCommand(check3, con);
            con.Open();
            NpgsqlDataReader reader3;
            reader3 = command3.ExecuteReader();
            string num_worker = "";
            while (reader3.Read())
            {
                try
                {
                    num_worker = reader3.GetString(0);
                    con.Close();
                    UserDoctor ud = new UserDoctor(connectionString,num);
                    ud.Show();
                    this.Hide();
                }
                catch
                {
                    MessageBox.Show("ERROR(");
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            enter();
            nnum();
            sotr();
           
        }
    }
}
