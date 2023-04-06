using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Journal
{
    class ConnectionClass
    {
        public static MySqlConnection connection { get; set; }
        public static bool ConnectDB() {
            StreamReader sr = new StreamReader("connectionInfo.txt");
            connection = new MySqlConnection(sr.ReadLine());
            sr.Close();
            try
            {
                connection.Open();
                return true;
            }catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static void FillDataGridViewFromSQL(string sql, DataGridView dataGridView)
        {
            try
            {
                dataGridView.DataSource = (new DataSet());
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView.DataSource = ds.Tables[0];
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void FillComboBoxFromSQL(string sql, ComboBox comboBox)
        {
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    comboBox.Items.Add(ds.Tables[0].Rows[i].ItemArray[0]);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        
        public static bool ModifyQuery(string sql)
        {
            try
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
