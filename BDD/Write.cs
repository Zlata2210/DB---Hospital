using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class Write : Form
    {
        String connectionString;
        String num_id;
        public Write(string connectionString,string num_id)
        {
            InitializeComponent();
            label4.Text = "Врач";
            button1.Text = "Записаться";
            this.connectionString = connectionString;
            this.num_id = num_id;
            label1.Text = num_id;
          
            Doctor(num_id);
        }
        

        public void Doctor(string num_id)
        {
            this.num_id = num_id;
            NpgsqlConnection con = new NpgsqlConnection(connectionString);
            //Врачи
            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Фамилия"; //текст в шапке
            column1.Width = 100; //ширина колонки
            column1.ReadOnly = true; //значение в этой колонке нельзя править
            column1.Name = "firstname"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column1.Frozen = true; //флаг, что данная колонка всегда отображается на своем месте
            column1.CellTemplate = new DataGridViewTextBoxCell();

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Имя";
            column2.Name = "name";
            column2.CellTemplate = new DataGridViewTextBoxCell();

            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Отчество";
            column3.Name = "Otche";
            column3.CellTemplate = new DataGridViewTextBoxCell();
            var column4 = new DataGridViewColumn();
            column4.HeaderText = "Должность";
            column4.Name = "Otche";
            column4.CellTemplate = new DataGridViewTextBoxCell();
            var column5 = new DataGridViewColumn();
            column5.HeaderText = "Категория";
            column5.Name = "categories";
            column5.CellTemplate = new DataGridViewTextBoxCell();
            var column6 = new DataGridViewColumn();
            column6.HeaderText = "Время";
            column6.Name = "times";
            column6.CellTemplate = new DataGridViewTextBoxCell();
            var column7 = new DataGridViewColumn();
            column7.HeaderText = "День недели";
            column7.Name = "datass";
            column7.CellTemplate = new DataGridViewTextBoxCell();


            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns.Add(column4);
            dataGridView1.Columns.Add(column5);
            dataGridView1.Columns.Add(column6);
            dataGridView1.Columns.Add(column7);
            dataGridView1.AllowUserToAddRows = false;
            string command = "SELECT Сотрудники.Имя, Сотрудники.Фамилия, Сотрудники.Отчество, Сотрудники.Должность,Сотрудники.Категория, Дежурства.Время, Дежурства.\"День недели\"" +
"FROM Сотрудники INNER JOIN Дежурства ON Сотрудники.\"Код сотрудника\" = Дежурства.\"Код сотрудника\" ";
            NpgsqlCommand com2 = new NpgsqlCommand(command, con);
            con.Open();
            NpgsqlDataReader reader;
            reader = com2.ExecuteReader();
            List<string[]> data = new List<string[]>();
            
            
            while (reader.Read())
            {
                data.Add(new string[7]);

                data[data.Count - 1][0] = reader["Фамилия"].ToString();
                data[data.Count - 1][1] = reader["Имя"].ToString();
                data[data.Count - 1][2] = reader["Отчество"].ToString();
                data[data.Count - 1][3] = reader["Должность"].ToString();
                data[data.Count - 1][4] = reader["Категория"].ToString();
               data[data.Count - 1][5] = reader["Время"].ToString();
               data[data.Count - 1][6] = reader["День недели"].ToString();
            }
            reader.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
            //int index = dataGridView1.CurrentRow.Index;



          //  string sql = "INSERT INTO \"Запись на прием\" (\"Код пациента\",\"Код сотрудника\",\"Код дежурства\") VALUES ("+id+")";




        }
      
        
    }
    }

     

      

