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
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("select address from department where name ='"+comboDepartment.Text+"'",ConnectionClass.connection);
            textAddress.Text = command.ExecuteScalar().ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok=true;
            try
            {
                ConnectionClass.ModifyQuery("call insertRecord('" + comboOfficer.Text + "'," +
                    "'" + comboDepartment.Text + "','" + textAddress.Text + "'," + textKYESP.Text + ",'" + dateRec.Value.Date.ToString("yyyy-MM-dd") + "','" + dateAct.Value.Date.ToString("yyyy-MM-dd") + "'" +
                    ",'" + textDesc.Text + "','" + textApplicant.Text + "'," + textTerm.Text + ",'" + dateCheck.Value.Date.ToString("yyyy-MM-dd") + "');");
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    ConnectionClass.ModifyQuery("call insertYPKs(" + dataGridView2.Rows[i].Cells[0].Value.ToString() + ",'" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "')");
                }
                for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                {
                    ConnectionClass.ModifyQuery("call insertYKs(" + dataGridView3.Rows[i].Cells[0].Value.ToString() + ",'" + dataGridView3.Rows[i].Cells[1].Value.ToString() + "')");
                }
                for (int i = 0; i < dataGridView4.Rows.Count - 1; i++)
                {
                    string[] dateArray = dataGridView4.Rows[i].Cells[2].Value.ToString().Split('-');
                    string date = dateArray[2] + '-' + dateArray[1] + '-' + dateArray[0];
                    ConnectionClass.ModifyQuery("call insertMovement('" + dataGridView4.Rows[i].Cells[0].Value.ToString() + "','" + dataGridView4.Rows[i].Cells[1].Value.ToString() + "','" + date + "')");
                }               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                ok = false;
            }
            if (ok)
                MessageBox.Show("Запись успешно добавлена!");
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            //dataGridView4.Columns[2].DefaultCellStyle.Format = "d";
            ConnectionClass.FillComboBoxFromSQL("select name from department", comboDepartment);
            ConnectionClass.FillComboBoxFromSQL("select name from officers", comboOfficer);
        }
    }
}
