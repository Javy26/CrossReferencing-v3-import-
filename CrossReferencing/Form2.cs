using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace CrossReferencing
{
    public partial class Form2 : Form
    {
        string fileExcel;
        public Form2()
        {
            InitializeComponent();  
            
        }

        

        private  void button1_Click_1(object sender, EventArgs e)
        {
            
            fileExcel = "C:\\Users\\jdavis\\Downloads\\Pharmacies\\CrossReferencing v3\\CrossReferencing\\bin\\Debug\\cross_check.xls";
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            xlApp = new Excel.Application();

            //workbook open
            xlWorkBook = xlApp.Workbooks.Open(fileExcel, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,"\t", false, false, 0, true, 1, 0);
            xlApp.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

         
        }
    }
}
