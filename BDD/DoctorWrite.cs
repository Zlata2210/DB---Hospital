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
    public partial class DoctorWrite : Form
    {
        String connectionString;
        Int32 num;
        public DoctorWrite(string connectionString,int num)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.num = num;
            label1.Text = "Время приема/День недели";
            label2.Text = "Пациент";
            button1.Text = "Записать";
            Writee();
        }
        public void Writee()
        {
            string sqll = "SELECT \"День недели\" FROM Дежурства WHERE ((Дежурства.\"Код сотрудника\") = '" + num+ "')";
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            Conn.Open();
            NpgsqlCommand comm1 = new NpgsqlCommand(sqll, Conn);
            NpgsqlDataReader reader = comm1.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }

            Conn.Close();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo1();
        }

        public void combo1()
        {
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            string sql1 = "SELECT Время FROM  Дежурства WHERE(((Дежурства.\"День недели\")='"+comboBox1.Text+"') AND ((Дежурства.\"Код сотрудника\") = "+num+"))";
            Conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand(sql1, Conn);
            NpgsqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {

                comboBox3.Items.Add(dr.GetString(0));
            }

            Conn.Close();
            patient();
        }
        public void patient()
        {
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            string sql1 = "SELECT \"Номер_паспорта\" FROM Пациент";
            Conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand(sql1, Conn);
            NpgsqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {

                comboBox2.Items.Add(dr.GetString(0));
            }

            Conn.Close();
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
        }
        int num_id;
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            NpgsqlConnection Connt = new NpgsqlConnection(connectionString);
            string ss = "SELECT \"Код пациента\" FROM Пациент WHERE ((Пациент.Номер_паспорта)= '"+comboBox2.Text+"')";
            Connt.Open();
            NpgsqlCommand comma1 = new NpgsqlCommand(ss, Connt);
            NpgsqlDataReader reade1r = comma1.ExecuteReader();
            //int num = 0;
            while (reade1r.Read())
            {
                try
                {
                    num_id = reade1r.GetInt32(0);
                }
                catch { break; }
            }
            Connt.Close();

            ID();
        }

        int num_zap;
        public void ID()
        {
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
                    num_zap = reade1r.GetInt32(0);
                }
                catch { break; }
            }
            Connt.Close();
        }
        int num_time;
        public void time()
        {
            NpgsqlConnection Connt = new NpgsqlConnection(connectionString);
            string ss = "SELECT \"Код дежурства\" FROM Дежурства WHERE(((Дежурства.Время) = '"+comboBox3.Text+"') AND((Дежурства.\"День недели\")= '"+comboBox1.Text+"'))";
            Connt.Open();
            NpgsqlCommand comma1 = new NpgsqlCommand(ss, Connt);
            NpgsqlDataReader reade1r = comma1.ExecuteReader();
            while (reade1r.Read())
            {
                num_time = reade1r.GetInt32(0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            time();
            NpgsqlConnection Connt = new NpgsqlConnection(connectionString);
            string sql = "INSERT INTO \"Запись на прием\" VALUES("+num_zap+","+num_id+","+num_time+")";
            Connt.Open();
            NpgsqlCommand comma1 = new NpgsqlCommand(sql, Connt);
            comma1.ExecuteNonQuery();
            MessageBox.Show("Успешно!");
        }
    }
}
