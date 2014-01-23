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
    // example: [cmd] : [batch command]
    // the [batch command] will be saved into a batch file, unfortunately only win batch commands available though

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
    // - screenshot

    public partial class Form1 : Form
    {

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public Form1()
        {
            InitializeComponent();

            int id = 0;
            RegisterHotKey(this.Handle, id, 2, Keys.B.GetHashCode());
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                if (this.Opacity < 0.1)
                {
                    this.Opacity = 1;
                }
                else
                {
                    this.Opacity = 0;
                }
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2, 10);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String command = textBox1.Text;

                // is false if no default command could be executed
                bool success = true;

                switch (command)
                {
                    case "exit":
                        Application.Exit();
                        break;
                    default:
                        // TODO: open process

                        success = false;
                        break;
                }

                // update listbox
                if (success)
                {
                    listBox1.Items.Add(command);
                    if (listBox1.Items.Count < 11)
                    {
                        listBox1.Height = listBox1.ItemHeight * (listBox1.Items.Count + 1);
                    }
                }
                return;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, 0);
        }
    }
}
