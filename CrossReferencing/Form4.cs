using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrossReferencing
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains(" "))
            {
                MessageBox.Show("Name cannot be blank or contain spaces!");
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Name cannot be blank or contain spaces!");
            }
            else
            {
                string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacy_Output_File;Integrated Security=True";
                string query = "CREATE TABLE [dbo].[" + textBox1.Text + "](" + "ID int IDENTITY (1,1)," + "[Code] [varchar] (13) NOT NULL," +
               "[Description] [varchar] (50) NOT NULL," + "[NDC] [varchar] (50) NULL," +
                "[Supplier Code] [varchar] (38) NULL," + "[Supplier Description] [varchar] (38) NULL," + "[UOM] [varchar] (8) NULL," + "[Size] [varchar] (8) NULL,)";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Table Created in Database successfully!");
                this.Close();
               
            }
        }
    }
}
