using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ControlCommands2
{
    public class Tools
    {
        static Random r = new Random();

        public static void loadConfig()
        {
            String[] lines = File.ReadAllLines("");
            foreach (String s in lines)
            {
                String[] s_ = s.Split('#');
                if (s_.Length > 1)
                {
                    String cmd = s_[0];
                    String script = s_[1];
                }
            }
        }

        public static void saveConfig()
        {

        }

        public static void startCMD(String cmd, bool hidden)
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

        public static void startProcess(String proc)
        {
            try
            {
                System.Diagnostics.Process.Start(proc);
            }
            catch (Exception ex) { }
        }

        public static void screenshot()
        {
            Bitmap b = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(0, 0, 0, 0, b.Size);
            g.Dispose();
            String path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\" +  DateTime.Now.ToString("ddMM_HHmmss_") + r.Next(100).ToString() + ".png";
            b.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static void killProcess(String name)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(name))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " for " + name);
            }
        }
    }
}
