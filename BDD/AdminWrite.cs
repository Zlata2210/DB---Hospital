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
    public partial class AdminWrite : Form
    {
        String connectionString;
        public AdminWrite(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            label1.Text = "Должность";
            label2.Text = "Сотрудник";
            label3.Text = "День недели";
            label4.Text = "ВРемя";
            label5.Text = "Пациент";
            button1.Text = "Добавить";
            Position();
           
            
        }


        public void Position()
        {
            string sql = "SELECT Должность FROM Сотрудники ";
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            Conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand(sql, Conn);
            NpgsqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr.GetString(0));
            }

            Conn.Close();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        public void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Worker();
        }
        public void Worker()
        {
            string sqll = "SELECT Фамилия FROM Сотрудники WHERE((Сотрудники.Должность) = '"+comboBox1.Text+"')";
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            Conn.Open();
            NpgsqlCommand comm1 = new NpgsqlCommand(sqll, Conn);
            NpgsqlDataReader reader = comm1.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader.GetString(0));
            }

            Conn.Close();

            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
        }
        public void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ssql = "Select \"Код сотрудника\" FROM Сотрудники WHERE ((Сотрудники.Фамилия) = '" + comboBox2.Text + "')";
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            Conn.Open();
            NpgsqlCommand comm1 = new NpgsqlCommand(ssql, Conn);
            NpgsqlDataReader reader = comm1.ExecuteReader();
            int num_id = 0;
            while (reader.Read())
            {
                try
                {
                    num_id = reader.GetInt32(0);
                }
                catch { break; }
            }
            Date(num_id);

        }
       public void Date(int num_id)
        {
            string sqll = "SELECT \"День недели\" FROM Дежурства WHERE ((Дежурства.\"Код сотрудника\") = '"+num_id+"')";
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            Conn.Open();
            NpgsqlCommand comm1 = new NpgsqlCommand(sqll, Conn);
            NpgsqlDataReader reader = comm1.ExecuteReader();
            while (reader.Read())
            {
                comboBox3.Items.Add(reader.GetString(0));
            }

            Conn.Close();
            comboBox3.SelectedIndexChanged += ComboBox3_SelectedIndexChanged;
        }
        int num_id;
        public void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ssql = "Select \"Код сотрудника\" FROM Сотрудники WHERE ((Сотрудники.Фамилия) = '" + comboBox2.Text + "')";
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            Conn.Open();
            NpgsqlCommand comm1 = new NpgsqlCommand(ssql, Conn);
            NpgsqlDataReader reader = comm1.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    num_id = reader.GetInt32(0);
                }
                catch { break; }
            }
            time(num_id);
        }
        public void time(int num_id)
        {
            string ch = "SELECT Время FROM Дежурства WHERE ((Дежурства.\"День недели\") ='" + comboBox3.Text + "' AND (Дежурства.\"Код сотрудника\") = '" + num_id + "')";
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            Conn.Open();
            NpgsqlCommand comm1 = new NpgsqlCommand(ch, Conn);
            NpgsqlDataReader reader = comm1.ExecuteReader();
            while (reader.Read())
            {
                comboBox4.Items.Add(reader.GetString(0));
            }
            Conn.Close();
            comboBox4.SelectedIndexChanged += ComboBox4_SelectedIndexChanged;  
            

        }


        int num_d;
        public void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            patient();
            string ssql = "SELECT\"Код дежурства\" FROM Дежурства WHERE (Дежурства.Время)='" + comboBox4.Text + "'";
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            Conn.Open();
            NpgsqlCommand comm11 = new NpgsqlCommand(ssql, Conn);
            NpgsqlDataReader reader2 = comm11.ExecuteReader();
            while (reader2.Read())
            {
                try
                {
                    num_d = reader2.GetInt32(0);
                }
                catch { break; }
            }
        }

        

        public void patient()
        {
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            string pat = "SELECT Номер_паспорта FROM Пациент";
            Conn.Open();
            NpgsqlCommand comm1 = new NpgsqlCommand(pat, Conn);
            NpgsqlDataReader reader = comm1.ExecuteReader();
            while (reader.Read())
            {
                comboBox5.Items.Add(reader.GetString(0));
            }
            comboBox5.SelectedIndexChanged += ComboBox5_SelectedIndexChanged;
            Conn.Close();
        }
        int num_pat;

        int num;
        public void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            string pat = "SELECT \"Код пациента\" FROM Пациент WHERE ((Пациент.Номер_паспорта)= '" + comboBox5.Text + "')";
            Conn.Open();
            NpgsqlCommand comm1 = new NpgsqlCommand(pat, Conn);
            NpgsqlDataReader reader = comm1.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    num_pat = reader.GetInt32(0);
                }
                catch { break; }
            }
            Conn.Close();
            NpgsqlConnection Connt = new NpgsqlConnection(connectionString);

            string ss = "SELECT MAX(\"Код записи\")+1 FROM \"Запись на прием\"";
            Connt.Open();
            NpgsqlCommand comma1 = new NpgsqlCommand(ss, Connt);
            NpgsqlDataReader reade1r = comma1.ExecuteReader();
            //int num = 0;
            while (reade1r.Read())
            {
                try
                {
                    num = reade1r.GetInt32(0);
                }
                catch { break; }
            }
        }
       
       
        


        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            string ssql = "INSERT INTO \"Запись на прием\"(\"Код записи\",\"Код пациента\",\"Код дежурства\") VALUES(" + num + "," + num_pat + "," + num_d + ")";
           
            Conn.Open();
            using (NpgsqlCommand cmd = new NpgsqlCommand(ssql, Conn))
            {
                // Добавить параметры
                cmd.Parameters.AddWithValue("@Код записи", num);
                cmd.Parameters.AddWithValue("@Код пациента", num_pat);
                cmd.Parameters.AddWithValue("@Код дежурства", num_d);

                cmd.ExecuteNonQuery();
            }

             Conn.Close();
            this.Hide();
        }
    }
}
