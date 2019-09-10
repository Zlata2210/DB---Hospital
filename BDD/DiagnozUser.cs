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
    public partial class DiagnozUser : Form
    {
        String connectionString;
        String num_id;
        public DiagnozUser(string connectionString,string num_id)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.num_id = num_id;
            ttable();
        }
        int num;
        public void ID()
        {
            NpgsqlConnection Connt = new NpgsqlConnection(connectionString);
            string ss = "SELECT \"Код пациента\" FROM Пациент WHERE ((Пациент.Номер_паспорта)= '" + num_id + "')";
            Connt.Open();
            NpgsqlCommand comma1 = new NpgsqlCommand(ss, Connt);
            NpgsqlDataReader reade1r = comma1.ExecuteReader();

            while (reade1r.Read())
            {
                try
                {
                    num = reade1r.GetInt32(0);
                }
                catch { break; }
            }
            Connt.Close();
        }

        public void dia()
        {
            ID();
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);

            string ssql = "SELECT Диагноз,Лечение FROM Диагноз WHERE((Диагноз.\"Код пациента\")=" + num_id + ") ";
            Conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(ssql, Conn);
            DataTable data = new DataTable();
            da.Fill(data);
            this.dataGridView1.DataSource = data;


            Conn.Close();
        }

        public void ttable()
        {
            dia();

        }
    }
}
