using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A_Automation.HealthCheck
{
    public partial class DynamicPopup : Form
    {
        public DynamicPopup()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }
    }
}
