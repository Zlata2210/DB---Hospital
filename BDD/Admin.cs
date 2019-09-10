using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace BDD
{
    public partial class Admin : Form
    {
        String connectionString;
        public Admin(String connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            button1.Text = "Записать на прием";
            Check();
        }
        public void Check()
        {
            comboBox1.Items.Add("Дежурства");
            comboBox1.Items.Add("Диагноз");
            comboBox1.Items.Add("Запись на прием");
            comboBox1.Items.Add("Отделение");
            comboBox1.Items.Add("Пациент");
            comboBox1.Items.Add("Сотрудники");
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Table();
        }

        public void Table()
        {
            if (comboBox1.Text == "Дежурства") dezh();
            else if (comboBox1.Text == "Запись на прием") writep();
            else if (comboBox1.Text == "Диагноз") dia();
            else if (comboBox1.Text == "Отделение") otd();
            else if (comboBox1.Text == "Сотрудники") sotr();
        }
        public void sotr()
        {
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            string ssql = "SELECT Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Сотрудники.Должность, Сотрудники.Категория, Отделение.Название FROM Отделение INNER JOIN Сотрудники ON Отделение.\"Код отделения\" = Сотрудники.\"Код отделения\"";
            Conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(ssql, Conn);
            DataTable data = new DataTable();
            da.Fill(data);
            this.dataGridView1.DataSource = data;
            Conn.Close();
        }
        public void otd()
        {
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            string ssql = "Select Отделение.Название,Отделение.\"Время работы\" FROM Отделение";
             Conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(ssql, Conn);
            DataTable data = new DataTable();
            da.Fill(data);
            this.dataGridView1.DataSource = data;
            Conn.Close();
        }
        public void dia()
        {
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            string ssql = "SELECT Пациент.Фамилия, Пациент.Имя, Пациент.Отчество, Диагноз.Диагноз ,Диагноз.Лечение FROM Пациент INNER JOIN Диагноз ON Пациент.\"Код пациента\" = Диагноз.\"Код пациента\"";
           Conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(ssql, Conn);
            DataTable data = new DataTable();
            da.Fill(data);
            this.dataGridView1.DataSource = data;
            Conn.Close();
        }

        public void writep()
        {
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            string ssql = "SELECT Сотрудники.Должность, Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Дежурства.\"День недели\", Дежурства.Время, Пациент.Фамилия, Пациент.Имя, Пациент.Отчество FROM Пациент INNER JOIN(Сотрудники INNER JOIN (Дежурства INNER JOIN \"Запись на прием\" ON Дежурства.\"Код дежурства\" = \"Запись на прием\".\"Код дежурства\") ON Сотрудники.\"Код сотрудника\" = Дежурства.\"Код сотрудника\") ON Пациент.\"Код пациента\" = \"Запись на прием\".\"Код пациента\"";
            Conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(ssql, Conn);
            DataTable data = new DataTable();
            da.Fill(data);
            this.dataGridView1.DataSource = data;
            Conn.Close();
        }

        public void dezh()
        {
            NpgsqlConnection Conn = new NpgsqlConnection(connectionString);
            
             string ssql = "SELECT Сотрудники.Имя, Сотрудники.Фамилия, Сотрудники.Отчество, Сотрудники.Должность,Сотрудники.Категория, Дежурства.Время, Дежурства.\"День недели\"" +
 "FROM Сотрудники INNER JOIN Дежурства ON Сотрудники.\"Код сотрудника\" = Дежурства.\"Код сотрудника\" ";
            Conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(ssql, Conn);
            DataTable data = new DataTable();
            da.Fill(data);
            this.dataGridView1.DataSource = data;
            Conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminWrite admin = new AdminWrite(connectionString);
            admin.Show();
        }
    }
    }
