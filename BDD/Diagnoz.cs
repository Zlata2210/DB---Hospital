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
    public partial class Diagnoz : Form
    {
        String connectionString;
        Int32 num;
        public Diagnoz(string connectionString,int num)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.num = num;
            tablee();
        }

        public void tablee()
        {
            string sql = "SELECT Номер_паспорта FROM Пациент";
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

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dia();
        }
        int num_id;
        public void ID()
        {
            NpgsqlConnection Connt = new NpgsqlConnection(connectionString);
            string ss = "SELECT \"Код пациента\" FROM Пациент WHERE ((Пациент.Номер_паспорта)= '" + comboBox1.Text + "')";
            Connt.Open();
            NpgsqlCommand comma1 = new NpgsqlCommand(ss, Connt);
            NpgsqlDataReader reade1r = comma1.ExecuteReader();
           
            while (reade1r.Read())
            {
                try
                {
                    num_id = reade1r.GetInt32(0);
                }
                catch { break; }
            }
            Connt.Close();
        }

        public void dia()
        {
            ID();
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);

            string ssql = "SELECT Диагноз,Лечение FROM Диагноз WHERE((Диагноз.\"Код пациента\")="+num_id+") ";
            Conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(ssql, Conn);
            DataTable data = new DataTable();
            da.Fill(data);
            this.dataGridView1.DataSource = data;


            Conn.Close();
        }
    }
}
