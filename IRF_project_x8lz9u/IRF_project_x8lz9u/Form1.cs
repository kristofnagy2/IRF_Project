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
        public Form1()
        {
            InitializeComponent();

            var valuta = new MNBArfolyamServiceSoapClient();
          


        }
    }
}
