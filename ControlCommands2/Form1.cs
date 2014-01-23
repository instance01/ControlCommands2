using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlCommands2
{

    // TODO: add a console design

    // TODO: make custom commands available through config file

    // TODO: create a few commands
    // - processclean
    // - dir
    // - ping
    // - trace/tracert
    // - exit
    // - shutdown
    // - [open web urls]
    // - google
    // - [something with fav music]

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(10, 10);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String command = textBox1.Text;
                switch (command)
                {
                    case "":

                        return;
                    default:
                        // TODO: open process
                        return;
                }
            }
        }
    }
}
