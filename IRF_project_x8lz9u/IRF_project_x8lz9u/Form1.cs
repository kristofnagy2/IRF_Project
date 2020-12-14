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

            private void writeCSV(DataGridView gridIn, string outputFile)
            {
                
                if (gridIn.RowCount > 0)
                {
                    string value = "";
                    DataGridViewRow dr = new DataGridViewRow();
                    StreamWriter swOut = new StreamWriter(outputFile);

                    
                    for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                    {
                        if (i > 0)
                        {
                            swOut.Write(",");
                        }
                        swOut.Write(gridIn.Columns[i].HeaderText);
                    }

                    swOut.WriteLine();

                    for (int j = 0; j <= gridIn.Rows.Count - 1; j++)
                    {
                        if (j > 0)
                        {
                            swOut.WriteLine();
                        }

                        dr = gridIn.Rows[j];

                        for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                        {
                            if (i > 0)
                            {
                                swOut.Write(",");
                            }

                            value = dr.Cells[i].Value.ToString();
                            
                            value = value.Replace(',', ' ');
                           
                            value = value.Replace(Environment.NewLine, " ");

                            swOut.Write(value);
                        }
                    }
                    swOut.Close();
                }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            writeCSV(dataGridView1, "result.csv");
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
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
    }
    };

           






        
    

