using IRF_project_x8lz9u.Entities;
using IRF_project_x8lz9u.valuataservice;
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
using System.Xml;

namespace IRF_project_x8lz9u
{
    public partial class Form1 : Form
    {



        public Form1()
        {
            InitializeComponent();

            fv();

        }

        

        private void fv()
        {
            BindingList<Money> moneydata = new BindingList<Money>();
            moneydata.Clear();

            dataGridView1.DataSource = moneydata;

            var valuta = new MNBArfolyamServiceSoapClient();

            var inquiry = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()

            };

            var feedback = valuta.GetExchangeRates(inquiry);

            var end = feedback.GetExchangeRatesResult;

            var xml = new XmlDocument();
            xml.LoadXml(end);

            foreach (XmlElement item in xml.DocumentElement)
            {
                var money = new Money();
                moneydata.Add(money);

                money.Date = DateTime.Parse(item.GetAttribute("date"));

                var childElement = (XmlElement)item.ChildNodes[0];
                money.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    money.Value = value / unit;
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            
            int row_count = dataGridView1.RowCount;
            int lines_count = dataGridView1.Rows[0].Cells.Count;
            

            for (int row_index = 0; row_index <= row_count - 2; row_index++)
            {
                

                for (int cell_index = 0; cell_index <= lines_count - 1; cell_index++)
                {
                    MessageBox.Show(dataGridView1.Rows[row_index].Cells[cell_index].Value.ToString());



                    textBox1.Text = textBox1.Text + dataGridView1.Rows[row_index].Cells[cell_index].Value.ToString() + ",";

                }

                
                textBox1.Text = textBox1.Text + "\r\n";

            }
  

            System.IO.File.WriteAllText(@"C:/Users/Nagy Kristóf/Desktop/export.csv", textBox1.Text);

        }


          

         

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            fv();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            fv();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fv();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Size = new Size(300, 340);
            textBox1.Location = new Point(470, 80);
            textBox1.Multiline = true;
            
        }

        
    }
}


           






        
    

