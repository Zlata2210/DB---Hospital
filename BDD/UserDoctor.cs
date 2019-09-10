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
    public partial class UserDoctor : Form
    {
        String connectionString;
        Int32 num;
        public UserDoctor(string connectionString,int num)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.num = num;
            label1.Text = "WELCOME!";
            button1.Text = "Просмотреть свои записи";
            button2.Text = "Записать на прием";
            button3.Text = "Поставить диагноз";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoctorWrite();
        }
        public void DoctorWrite()
        {
            DoctorPriem dp = new DoctorPriem(connectionString, num);
            dp.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoctorWrite dw = new DoctorWrite(connectionString,num);
            dw.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Diagnoz diagnoz = new Diagnoz(connectionString, num);
            diagnoz.Show();
        }
    }
}
