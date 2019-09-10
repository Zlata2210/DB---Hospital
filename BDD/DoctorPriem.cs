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
    public partial class DoctorPriem : Form
    {
        String connectionString;
        Int32 num;
        public DoctorPriem(string connectionString,int num)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.num = num;

            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);

            string ssql = "SELECT Сотрудники.\"Код сотрудника\", Пациент.Фамилия, Пациент.Имя, Дежурства.Время, Дежурства.\"День недели\"" +
"FROM Сотрудники INNER JOIN(Пациент INNER JOIN (Дежурства INNER JOIN \"Запись на прием\" ON Дежурства.\"Код дежурства\" = \"Запись на прием\".\"Код дежурства\") ON Пациент.\"Код пациента\" = \"Запись на прием\".\"Код пациента\") ON Сотрудники.\"Код сотрудника\" = Дежурства.\"Код сотрудника\"" +
"WHERE (((Сотрудники.\"Код сотрудника\")= " + num + "))";

            Conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(ssql, Conn);
            DataTable data = new DataTable();
            da.Fill(data);
            this.dataGridView1.DataSource = data;

            Conn.Close();
        }


    }
}

