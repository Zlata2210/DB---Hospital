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
    public partial class User : Form
    {
        String connectionString;
        String num_id;
        public User(string connectionString,string num_id)
        {
            InitializeComponent();
            this.num_id = num_id;
            button1.Text = "Запись на прием";
            button2.Text = "Посмотреть диагноз";
            button1.AutoSize = true;
            textBox1.Text = num_id;
            this.connectionString = connectionString;

        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            Write new_form = new Write(connectionString, num_id);
            new_form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DiagnozUser du = new DiagnozUser(connectionString,num_id);
            du.Show();
            this.Hide();
        }
    }
}
