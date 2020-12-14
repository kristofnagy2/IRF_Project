using IRF_project_x8lz9u.Entities;
using IRF_project_x8lz9u.valuataservice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace IRF_project_x8lz9u
{
    public partial class Form1 : Form
    {

        BindingList<Money> moneydata = new BindingList<Money>();

        
        public Form1()
        {
            InitializeComponent();

            resfreshdata();

           void resfreshdata()
            {
                moneydata.Clear();

                dataGridView1.DataSource = moneydata;

                var valuta = new MNBArfolyamServiceSoapClient();

                var inquiy = new GetExchangeRatesRequestBody()
                {
                    currencyNames = "USD",
                    startDate = "2020-11-01",
                    endDate = "2020-12-03"

                };

                var feedback = valuta.GetExchangeRates(inquiy);

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
                };

            }
        }
            
            

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            refreshdat
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
