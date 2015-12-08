using System;
using System.Windows.Forms;

namespace SebaRunnerLevelCreator
{
    public partial class NodePicker : Form
    {
        public int selectedNodeIndex;
        public Form1 form1;

        public NodePicker()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void NodePicker_Load(object sender, EventArgs e)
        {
            CenterToParent();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                selectedNodeIndex = listBoxNodes.SelectedIndex;
                form1.AssignNode();
                Hide();
            }
            catch
            {
                MessageBox.Show("Something wrong happened.");
            }
            
            
        }
    }
}
