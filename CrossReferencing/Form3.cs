using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrossReferencing
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Contains(" "))
            {
                MessageBox.Show("Name cannot be blank or contain spaces!");
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Name cannot be blank or contain spaces!");
            }
            else
            {
                MessageBox.Show("Currently importing "+textBox2.Text+ "...\nA confirmation will be displayed when finished");

                string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacies;Integrated Security=True";
                string query = "CREATE TABLE [dbo].[" + textBox2.Text + "](" + "[Code] [varchar] (13) NOT NULL," +
               "[Description] [varchar] (50) NOT NULL," + "[NDC] [varchar] (50) NULL," +
                "[Supplier Code] [varchar] (38) NULL," + "[UOM] [varchar] (8) NULL," + "[Size] [varchar] (8) NULL," + "[Progress][varchar](2) DEFAULT '0')";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                SqlConnection con = new SqlConnection("Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacies;Integrated Security=True");
                string filepath = textBox1.Text; //"C:\\Users\\jdavis\\Desktop\\CRF_105402_New Port Maria Rx.csv";
                StreamReader sr = new StreamReader(filepath);
                string line = sr.ReadLine();
                string[] value = line.Split(',');
                DataTable dt = new DataTable();
                DataRow row;
                foreach (string dc in value)
                {
                    dt.Columns.Add(new DataColumn(dc));
                }

                while (!sr.EndOfStream)
                {
                    value = sr.ReadLine().Split(',');
                    if (value.Length == dt.Columns.Count)
                    {
                        row = dt.NewRow();
                        row.ItemArray = value;
                        dt.Rows.Add(row);
                    }
                }
                SqlBulkCopy bc = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.TableLock);
                bc.DestinationTableName = textBox2.Text;
                bc.BatchSize = dt.Rows.Count;
                con.Open();
                bc.WriteToServer(dt);
                bc.Close();
                con.Close();
                MessageBox.Show("Rx File Imported successfully!");
                this.Close();

            }
            //string connectionString3 = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacies;Integrated Security=True";
            //string query3 = "ALTER TABLE " + textBox2.Text +  "ADD [Progress] int default 0 NOT NULL;"; 


            //using (SqlConnection connection = new SqlConnection(connectionString3))
            //{
            //    SqlCommand command = new SqlCommand(query3, connection);
            //    command.Connection.Open();
            //    command.ExecuteNonQuery();
            //}
      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|XML files (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog.FileName;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //TextBox tb = sender as TextBox;
            //if(string.IsNullOrWhiteSpace(tb.Text)==true)
            //{
            //    MessageBox.Show("Name cannot be empty or contain spaces!");
                        
            //}
            
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //Keys key = e.KeyCode;
            //if(key == Keys.Space)
            //{
            //    e.Handled = true;
            //}
            //base.OnKeyDown(e);
        }

       

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            //TextBox tb = sender as TextBox;
            //if(string.IsNullOrWhiteSpace(tb.Text)==true)
            //{
            //    MessageBox.Show("Name cannot be empty or contain spaces!");
            //    e.Cancel = true;
            //    return;
            }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            //if(this.Text.Contains(" "))
            //{
            //    MessageBox.Show("Database Name cannot be empty or contain spaces!");
            //    this.Focus();
            //}
        }
    }
    }


