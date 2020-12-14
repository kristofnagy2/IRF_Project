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

namespace IRF_project_x8lz9u
{
    public partial class Form1 : Form
    {

        BindingList<Money> moneydata = new BindingList<Money>();

        
        public Form1()
        {
            InitializeComponent();

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

        }
    }
}
