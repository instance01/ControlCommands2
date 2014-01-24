using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const UInt32 LEFTDOWN = 0x0002;
        private const UInt32 LEFTUP = 0x0004;

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
                    this.TopMost = true;

                    // little hack to set focus on textbox:
                    Point bef = Cursor.Position;
                    Cursor.Position = new System.Drawing.Point(this.Location.X + textBox1.Left + 3, this.Location.Y + textBox1.Top + 3);
                    mouse_event((int)LEFTDOWN, 0, 0, 0, 0);
                    mouse_event((int)LEFTUP, 0, 0, 0, 0);
                    Cursor.Position = bef;
                }
                else
                {
                    this.Opacity = 0;
                    this.TopMost = false;
                }
                
            }
        }

        List<String> cmdlist = new List<string>() { 
            "exit",
            "processclean",
            "ping",
            "trace",
            "tracert",
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2, 10);
            AutoCompleteStringCollection a = new AutoCompleteStringCollection();
            a.AddRange(cmdlist.ToArray());
            textBox1.AutoCompleteCustomSource = a;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String command = textBox1.Text;
                String[] args = command.Split(' ');

                if (args.Length < 2)
                {
                    args = new String[] { command, " " };
                }

                // is false if no default command could be executed
                bool success = true;

                if (command.StartsWith(cmdlist[0]))
                {
                    // exit
                    Application.Exit();
                }
                else if (command.StartsWith(cmdlist[1]))
                {
                    // processclean

                }
                else if (command.StartsWith(cmdlist[2]))
                {
                    // ping
                    startCMD(command, false);
                }
                else if (command.StartsWith(cmdlist[3]))
                {
                    // trace
                    startCMD("tracert " + args[1], false);
                }
                else if (command.StartsWith(cmdlist[4]))
                {
                    // tracert
                    startCMD(command, false);
                }
                else
                {
                    try
                    {
                        System.Diagnostics.Process.Start(command);
                    }
                    catch (Exception ex) {}
                    success = false;
                }

                /*switch (command)
                {
                    case cmdlist[0]:
                        Application.Exit();
                        break;
                    case "processclean":
                        
                        break;
                    case "ping":
                        startCMD(command, false);
                        break;
                    case "trace":
                        startCMD("tracert " + args[1], false);
                        break;
                    case "tracert":
                        startCMD(command, false);
                        break;
                    default:
                        // TODO: open process

                        success = false;
                        break;
                }*/

                // update listbox
                if (success)
                {
                    listBox1.Items.Add(command);
                    if (listBox1.Items.Count < 11)
                    {
                        listBox1.Height = listBox1.ItemHeight * (listBox1.Items.Count + 1);
                    }
                }

                // update rest
                textBox1.Text = "";
                this.Opacity = 0;
                this.TopMost = false;

                return;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, 0);
        }


        public void loadConfig()
        {
            String[] lines = File.ReadAllLines("");
            foreach(String s in lines){
                String[] s_ = s.Split('#');
                if (s_.Length > 1)
                {
                    String cmd = s_[0];
                    String script = s_[1];
                }
            }
        }

        public void saveConfig()
        {

        }

        public void startCMD(String cmd, bool hidden)
        {
            if (hidden)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C " + cmd;
                process.StartInfo = startInfo;
                process.Start();
            }
            else
            {
                System.Diagnostics.Process.Start("CMD.exe", "/C " + cmd);
            }
        }
    }
}
