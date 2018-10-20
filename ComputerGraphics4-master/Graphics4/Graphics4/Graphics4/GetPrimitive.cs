using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics4
{
    public partial class GetPrimitive : Form
    {
        public GetPrimitive()
        {
            InitializeComponent();
            comboBox1.SelectedItem = comboBox1.Items[0];
            label2.Hide();
            comboBox2.Hide();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                main.current_primitive = this.comboBox1.SelectedItem.ToString();
                if (comboBox1.SelectedItem == comboBox1.Items[2])
                    main.number_of_edges = int.Parse(this.comboBox2.SelectedItem.ToString());
            }
            this.Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == comboBox1.Items[2])
            {
                label2.Show();
                comboBox2.Show();
                comboBox2.SelectedItem = comboBox2.Items[0];
            }

        }
    }
}
