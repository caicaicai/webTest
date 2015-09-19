using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPocalipse.Json.Viewer;

namespace webTest.View
{
    public partial class JsonViewForm : Form
    {
        public JsonViewForm(string json)
        {
            InitializeComponent();
            jsonViewer1.Json = json;
        }

    }
}
