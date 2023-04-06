using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Journal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            (new AddForm()).ShowDialog();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (ConnectionClass.ConnectDB()) ;
            else
            {
                MessageBox.Show("Неполадки при подключении к базе данных.\nОбратитесь к администратору");
                Application.Exit();
            }
            MySqlCommand command = new MySqlCommand("call num_of_records();", ConnectionClass.connection);

            labelNUM.Text = "Всего записей: " + command.ExecuteScalar().ToString();
            ConnectionClass.FillDataGridViewFromSQL("select * from records_all", dataGridView1);
            dataGridView1.Columns[4].Visible = false;
            if (dataGridView1.Rows.Count != 0)
                textBoxDesc.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[3].Width = 140;
            dataGridView1.Columns[5].Width = 125;
            dataGridView1.Columns[6].Width = 125;
            dataGridView1.Columns[8].Width = 150;
            dataGridView1.Columns[9].Width = 150;

            ConnectionClass.FillDataGridViewFromSQL("select * from ypk_records where idRecord = " + dataGridView1.SelectedRows[0].Cells[0].Value, dataGridView2);
            ConnectionClass.FillDataGridViewFromSQL("select * from yk_records where idRecord = " + dataGridView1.SelectedRows[0].Cells[0].Value, dataGridView3);
            ConnectionClass.FillDataGridViewFromSQL("select * from movement_records where idRecord = " + dataGridView1.SelectedRows[0].Cells[0].Value, dataGridView4);
            dataGridView2.Columns[0].Visible = false;
            dataGridView3.Columns[0].Visible = false;
            dataGridView4.Columns[0].Visible = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count != 0)
                if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
                {
                    ConnectionClass.FillDataGridViewFromSQL("select * from ypk_records where idRecord = " + dataGridView1.SelectedRows[0].Cells[0].Value, dataGridView2);
                    ConnectionClass.FillDataGridViewFromSQL("select * from yk_records where idRecord = " + dataGridView1.SelectedRows[0].Cells[0].Value, dataGridView3);
                    ConnectionClass.FillDataGridViewFromSQL("select * from movement_records where idRecord = " + dataGridView1.SelectedRows[0].Cells[0].Value, dataGridView4);
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView3.Columns[0].Visible = false;
                    dataGridView4.Columns[0].Visible = false;
                    textBoxDesc.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] articlesArray = textArticlesYK.Text.Split('\n');
            string articlesString = null;
            for (int i = 0; i < articlesArray.Length; i++)
                articlesString += articlesArray[i] + ',';
            articlesString = articlesString.Remove(articlesString.Length-1,1);
            ConnectionClass.FillDataGridViewFromSQL("call searchQuery('"+dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")+"','"+dateTimePicker2.Value.Date.ToString("yyyy-MM-dd")+"','"+textSearchKYESP.Text+"','"+articlesString+"')", dataGridView1);
            labelFILTNUM.Text = "Отсортированных записей: " + (dataGridView1.Rows.Count);
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Rows[0].Selected = true;
            if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
                textBoxDesc.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[3].Width = 140;
            dataGridView1.Columns[5].Width = 125;
            dataGridView1.Columns[6].Width = 125;
            dataGridView1.Columns[8].Width = 150;
            dataGridView1.Columns[9].Width = 150;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new AddForm()).ShowDialog();
            Form1_Load(sender,e);
        }
    }
}
