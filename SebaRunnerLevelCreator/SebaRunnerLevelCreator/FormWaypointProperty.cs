using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SebaRunnerLevelCreator
{
    public partial class FormWaypointProperty : Form
    {
        public FormWaypointProperty()
        {
            InitializeComponent();
        }

        private void comboBoxWaypointType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBoxWaypointType.ValueMember)
            {
                case "Wait":
                    break;
                case "Straight":
                    break;
                case "Bezier":
                    break;
                default:
                    break;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
