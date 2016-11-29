// <author>Jevon Davis</author>

using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CrossReferencing
{

    public partial class Form1 : Form
    {
        //string fileExcel;
        public Form1()
        {
            InitializeComponent();
            fillCari();
            if (string.IsNullOrEmpty(comboBox4.Text))
            {
            }
            else
            {

                FillCombo();
            }

        }
 


        void mnuPaste_Click(object sender, EventArgs e)
        {
            // take action
        }
        void mnuCut_Click(object sender, EventArgs e)
        {
            // take action
        }
        void mnuCopy_Click(object sender, EventArgs e)
        {
            // take action
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        //fills comboBox4 with the tables from the Pharmacy Database
        private void FillCombo()
        {

         

                comboBox4.Items.Clear();


                try
                {

                    string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacies;Integrated Security=True";
                    using (SqlConnection con2 = new SqlConnection(connectionString))
                    {
                        con2.Open();
                        string query = "SELECT * FROM INFORMATION_SCHEMA.TABLES ";
                        SqlCommand cmd2 = new SqlCommand(query, con2);

                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        while (dr2.Read())
                        {
                            int col = dr2.GetOrdinal("TABLE_NAME");
                            comboBox4.Items.Add(dr2[col].ToString());
                        }
                       // comboBox4.SelectedIndex = 0;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {


        }



        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void openToolStripMenuItem_Click(object sender, EventArgs e)//creates new instance of a form and invokes it
        {
            Form3 form3 = new Form3();
            form3.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }


        async void fillLiguanea()//fills pharmacy item comboBox with values(through a query) from the file that was originally uploaded to the system
        {
            comboBox2.Items.Clear();
            try
            {

                string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacies;Integrated Security=True";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string query = "SELECT * FROM " + comboBox4.Text;
                SqlCommand cmd = new SqlCommand(query, con);
                var reader = await cmd.ExecuteReaderAsync();
                comboBox2.BeginUpdate();
                while (reader.Read())
                {
                    string scode = reader.GetString(reader.GetOrdinal("code"));
                    comboBox2.Items.Add(scode);
                }
                comboBox2.EndUpdate();
                comboBox2.SelectedIndex = 0;
                // comboBox2.Sorted = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        void fillCari()//fill Cari-med dropdown with values
        {
            try
            {

                string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Carimed_Inventory;Integrated Security=True";
                SqlConnection con2 = new SqlConnection(connectionString);
                con2.Open();
                string query = "SELECT * FROM dbo.Carimed";//select Convert(nvarchar(50),Item_Description)+ ':' +Convert(nvarchar(50),Item#) as Combined from Carimed
                SqlCommand cmd2 = new SqlCommand(query, con2);

                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    string cari_des = dr2.GetString(dr2.GetOrdinal("Item_Description"));
                    //mycollection2.Add(dr2.GetString(0));
                    comboBox3.Items.Add(cari_des);
                    comboBox3.Text.Trim();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }



        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //carimed Inventory

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CR Tool Version 1.0.1\nDeveloped by J. Davis");
        }



        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(comboBox4.Text))
            {
                MessageBox.Show("Cannot skip an empty record, please load a table!");
            }
            else
            {
                string connectionString2 = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacies;Integrated Security=True";
                string query2 = "UPDATE dbo.[" + comboBox4.Text + "] SET Progress= '1' where code = '" + comboBox2.Text + "'; ";


                using (SqlConnection connection = new SqlConnection(connectionString2))
                {
                    SqlCommand command = new SqlCommand(query2, connection);

                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();

                }

                textBox1.Clear();
                textBox3.Clear();
                comboBox3.ResetText();
                if (comboBox2.SelectedIndex < comboBox2.Items.Count - 1)
                {
                    comboBox2.SelectedIndex += 1;

                    var i = dataGridView3.CurrentRow.Index;
                    refreshDataGrid();
                    dataGridView3.CurrentCell = dataGridView3.Rows[Math.Min(i + 1, dataGridView3.Rows.Count - 1)].Cells[0];

                }
                else
                {
                    refreshDataGrid();
                    MessageBox.Show("You have reached the end of the list!");
                    comboBox2.SelectedIndex = 0;
                  
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(comboBox5.Text))
            {
                MessageBox.Show("Please select output file to be written to!");
            }
            else
            {

                if (comboBox1.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("All fields must be filled in before saving!");

                }
                else
                {


                    //  StringBuilder csvconten = new StringBuilder();
                    // csvconten.AppendFormat("{0},{1},{2},{3},{4},{5}\r\n", comboBox2.Text, textBox5.Text, textBox2.Text, comboBox3.Text, textBox3.Text, comboBox1.Text);
                    // string csvpath = "cross_check.csv";
                    // File.AppendAllText(csvpath, csvconten.ToString());

                    string connectionString3 = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacy_Output_File;Integrated Security=True";
                    string query3 = "INSERT INTO dbo.[" + comboBox5.Text + "] VALUES('" + comboBox2.Text + "','" + textBox5.Text.Replace("'", "''") + "','" + textBox7.Text.Replace("'", "''") + "','" + textBox2.Text.Replace("'", "''") + "','" + comboBox3.Text.Replace("'", "''") + "','" + textBox3.Text + "','" + comboBox1.Text + "');";

                    using (SqlConnection connection = new SqlConnection(connectionString3))
                    {
                        SqlCommand command = new SqlCommand(query3, connection);

                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();

                    }
                    string connectionString2 = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacies;Integrated Security=True";
                    string query2 = "UPDATE dbo.[" + comboBox4.Text + "] SET Progress= '1' where code = '" + comboBox2.Text + "'; ";


                    using (SqlConnection connection = new SqlConnection(connectionString2))
                    {
                        SqlCommand command = new SqlCommand(query2, connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }


                    textBox2.Clear();
                    textBox3.Clear();
                    comboBox3.ResetText();
                    comboBox1.ResetText();
                }


                if (comboBox2.SelectedIndex < comboBox2.Items.Count - 1)
                {
                    comboBox2.SelectedIndex += 1;

                    var i = dataGridView3.CurrentRow.Index;
                    refreshDataGrid();
                    refreshDataGrid2();
                    dataGridView3.CurrentCell = dataGridView3.Rows[Math.Min(i + 1, dataGridView3.Rows.Count - 1)].Cells[0];

                }
                else
                {
                    refreshDataGrid();
                    refreshDataGrid2();
                    MessageBox.Show("You have reached the end of the list!");
                    comboBox2.SelectedIndex=0;

                }

            }
            }
        
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacies;Integrated Security=True";

                string query = "SELECT * FROM " + comboBox4.Text + " WHERE code = '" + comboBox2.Text + "' ; ";
                string mystring = query;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();//"SELECT * FROM "+comboBox4.Text
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string sdes = dr.GetString(dr.GetOrdinal("description"));
                    textBox5.Text = sdes;



                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            try
            {

                string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacies;Integrated Security=True";
                string query = "SELECT * FROM " + comboBox4.Text + " WHERE code = '" + comboBox2.Text + "' ; ";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();//"SELECT * FROM "+comboBox4.Text
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string ndc = dr.GetString(dr.GetOrdinal("ndc"));
                    textBox7.Text = ndc;


                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            // ComboBox c = ((ComboBox)sender);
            // string[] items = c.Items.OfType<string>().ToArray();
            // matched = items.Any(i => i == c.Text.Trim().ToLower());

        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Carimed_Inventory;Integrated Security=True";
                string query = "SELECT * FROM dbo.Carimed WHERE Item_Description = '" + comboBox3.Text.Replace("'", "''") + "' ; ";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string cari_code = dr.GetString(dr.GetOrdinal("item#"));
                    textBox2.Text = cari_code;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }




        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            /*  if (comboBox4.SelectedIndex == 0)
            {
                fillLiguanea();

            }
            else
            {

                MessageBox.Show("This table doesn't exist within the database");
            }*/
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }



        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keypress = e.KeyChar;
            if (char.IsDigit(keypress) || e.KeyChar == Convert.ToChar(Keys.Back))
            {


            }
            else
            {
                MessageBox.Show("Only Integers(numbers) are allowed!");
                e.Handled = true;
            }

        }

        private void button7_Click_2(object sender, EventArgs e)
        {

            //string path = "C:\\Users\\jdavis\\Downloads\\cross_check2.xls ";
            // string path = " \"C:\\Users\\jdavis\\Downloads\\Pharmacies\\CrossReferencing v3\\CrossReferencing\\bin\\Debug\\cross_check.xls\" ";
            //  OleDbConnection con = new OleDbConnection("Provider=Microsoft.Ace.OLEDB.12.0;Data Source=" + path + ";Extended Properties= Excel 12.0;");
            //OleDbCommand command = new OleDbCommand("SELECT *FROM [cross_check2]", con);
            //DataSet cross = new DataSet();
            //  OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            // adapter.Fill(cross);
            // dataGridView2.DataSource = cross.Tables;
        }



        private void button9_Click(object sender, EventArgs e)
        {

        }
        //originally this was the delete previous file button
        private void button6_Click_1(object sender, EventArgs e)
        {
            /*if (System.IO.File.Exists("cross_check.csv"))
            {
                var confirmResult = MessageBox.Show("Are you sure to delete the previous file?",
                                      "Confirm Delete",
                                      MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {

                    System.IO.File.Delete("cross_check.csv");
                    MessageBox.Show("File deleted successfully!");
                }
                else if (confirmResult == DialogResult.No)
                {

                }
                //MessageBox.Show("There is no file currently present.");
            }
            else
            {
                MessageBox.Show("There is no file currently present.");
            }*/
        }

        private void button7_Click_3(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to permanently delete this record!?",
                                      "Confirm Delete",
                                      MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {

                string connectionString2 = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacy_Output_File;Integrated Security=True";
                string query2 = "DELETE FROM " + comboBox5.Text + " WHERE ID = '" + textBox4.Text + "'; ";


                using (SqlConnection connection = new SqlConnection(connectionString2))
                {
                    SqlCommand command = new SqlCommand(query2, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                //this.liguanea_ProgressTableAdapter1.Fill(this.pharmaciesDataSet7.Liguanea_Progress);
                refreshDataGrid2();
            }
            else if (confirmResult == DialogResult.No)
            {

            }


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           

            if (string.IsNullOrEmpty(comboBox4.Text))
            {
            }
         
                FillCombo();
            
            // refreshDataGrid();
        }
        private void refreshDataGrid()
        {
            if (string.IsNullOrEmpty(comboBox4.Text))
            {

                MessageBox.Show("Table can't be refreshed on empty values. Please select a value in the dropdown!");
            }
            else
            {
                try
                {

                    string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacies;Integrated Security=True";
                    using (SqlConnection con2 = new SqlConnection(connectionString))
                    {
                        con2.Open();
                        string query = "SELECT Code, Description, Progress FROM " + comboBox4.Text;
                        SqlCommand cmd2 = new SqlCommand(query, con2);

                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        DataTable dt = new DataTable();

                        dt.Load(dr2);
                        dataGridView3.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void createPharmacyOutputFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            fillPharmacyOutputFile();
            refreshDataGrid2();
            //comboBox5.Items.Clear();

        }
        private void fillPharmacyOutputFile()
        {
            comboBox5.Items.Clear();
            try
            {

                string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacy_Output_File;Integrated Security=True";
                using (SqlConnection con2 = new SqlConnection(connectionString))
                {
                    con2.Open();
                    string query = "SELECT * FROM INFORMATION_SCHEMA.TABLES ";
                    SqlCommand cmd2 = new SqlCommand(query, con2);

                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        int col = dr2.GetOrdinal("TABLE_NAME");
                        comboBox5.Items.Add(dr2[col].ToString());
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
        private void refreshDataGrid2()
        {

            if (string.IsNullOrEmpty(comboBox5.Text))
            {

                // MessageBox.Show("Table can't be refreshed on empty values. Please select a value in the dropdown!");
            }
            else
            {
                try
                {

                    string connectionString = "Data Source=LPMSW09000012JD\\SQLEXPRESS;Initial Catalog=Pharmacy_Output_File;Integrated Security=True";
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "SELECT * FROM " + comboBox5.Text;
                        SqlCommand cmd = new SqlCommand(query, con);

                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dt2 = new DataTable();

                        dt2.Load(dr);
                        dataGridView1.DataSource = dt2;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
        }

        private void textBox5_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
            if (comboBox4.SelectedIndex > -1)
            {
                refreshDataGrid();
                fillLiguanea();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
           
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView3.Select();
            DataObject o = dataGridView3.GetClipboardContent();
            Clipboard.SetDataObject(o);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_2(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(comboBox5.Text))
            {
                MessageBox.Show("Cannot export unless table name is specified!");
            }
            else
            {
                int count_row = dataGridView1.RowCount;
            int count_cell = dataGridView1.Rows[0].Cells.Count;

           
                string path = "C:\\Users\\jdavis\\Desktop\\" + comboBox5.Text + ".csv";
                string rxHeader = "Code" + "," + "Description" + "," + "NDC" + "," + "Supplier Code"
                + "," + "Supplier Description" + "," + "Pack Size" + "," + "UOM" + Environment.NewLine;


                MessageBox.Show("Please wait while " + comboBox5.Text + " table is being exported..");

                for (int row_index = 0; row_index <= count_row - 2; row_index++)
                {

                    for (int cell_index = 1; cell_index <= count_cell - 1; cell_index++)
                    {
                        textBox8.Text = textBox8.Text + dataGridView1.Rows[row_index].Cells[cell_index].Value.ToString() + ",";

                    }
                    textBox8.Text = textBox8.Text + "\r\n";

                    if (!File.Exists(path))
                    {
                        System.IO.File.WriteAllText(path, rxHeader);
                        // System.IO.File.AppendAllText(path, textBox8.Text);
                    }
                    else
                    {
                        System.IO.File.AppendAllText(path, textBox8.Text);
                        textBox8.Clear();
                    }

                }
                MessageBox.Show("Export  of " + comboBox5.Text + " table is complete!");
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
         /*   string searchValue = textBox9.Text;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                bool valueResult = false;
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().Equals(searchValue))
                        {
                            int rowIndex = row.Index;
                            dataGridView3.Rows[rowIndex].Selected = true;
                            valueResult = true;
                            break;
                        }
                    }

                }
                if (!valueResult)
                {
                    MessageBox.Show("Unable to find " + textBox9.Text, "Not Found");
                    return;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }*/
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex > -1)
            {
                refreshDataGrid2();

        }
    }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
           
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = string.Format("Description LIKE '%{0}%'", textBox9.Text);
         
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            (dataGridView3.DataSource as DataTable).DefaultView.RowFilter = string.Format("Code LIKE '%{0}%'", textBox10.Text);
        }
    }
    }







