using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
//using Microsoft.WindowsAPICodePack.Net;
//using Microsoft.SDK.Samples.VistaBridge.Library.Network;


namespace Windows_Tool
{

    public partial class Form1 : Form
    {

        private List<NetworkInterfaceInfo> NetworkInterfaceInfos = new List<NetworkInterfaceInfo> ();
        private String ApplicationPath;
        private String ApplicationName;

        public Form1()
        {
            InitializeComponent();
            UpdateIPList();
        }


        private void UpdateIPList()
        {

            /*
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress IP in localIPs)
            {
                this.dataGridView1.Rows.Add("",IP);
                Console.WriteLine("{0}", IP);
            }
            */

            List<NetworkInterfaceInfo> temp = new List<NetworkInterfaceInfo>();

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            Console.WriteLine(ip.Address.ToString());

                            NetworkInterfaceInfo NI = new NetworkInterfaceInfo { Name = ni.Name, Address = ip.Address.ToString() };

                            temp.Add(NI);

                        }
                    }
                }
            }

            if (!(NetworkInterfaceInfos.SequenceEqual(temp)))
            {

                NetworkInterfaceInfos = temp;

                this.dataGridView1.Rows.Clear();
                foreach (NetworkInterfaceInfo i in NetworkInterfaceInfos)
                {
                    this.dataGridView1.Rows.Add(i.Name, i.Address);
                }

            }


            //var manager = new NetworkListManager();

            /*
            var networks = NetworkListManager.GetNetworks(NetworkConnectivityLevels.Connected);
            foreach (Network network in networks)
            {
                //Name property corresponds to the name I originally asked about
                Console.WriteLine("[" + network.Name + "]");

                Console.WriteLine("\t[NetworkConnections]");
                foreach (NetworkConnection conn in network.Connections)
                {
                    //Print network interface's GUID
                    Console.WriteLine("\t\t" + conn.AdapterId.ToString());
                }
            }
            */
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateIPList();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Choose_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\";
            //openFileDialog1.Filter = "exe files (*.exe, *.lnk)|*.exe;*.lnk";
            openFileDialog1.Filter = "exe files (*.exe)|*.exe";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                Console.WriteLine(openFileDialog1.SafeFileName);
                textBox1.Text = openFileDialog1.FileName;
                ApplicationPath = openFileDialog1.FileName;
                String[] substrings = openFileDialog1.SafeFileName.Split('.');
                ApplicationName = substrings[0];
            }
        }
        
        

        /*
        public static void CreateShortcut(string SourceFile, string ShortcutFile)
        {
            CreateShortcut(SourceFile, ShortcutFile, null, null, null, null);
        }

        public static void CreateShortcut(string TargetPath, string ShortcutFile, string Description,
           string Arguments, string HotKey, string WorkingDirectory)
        {
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(TargetPath))
                throw new ArgumentNullException("TargetPath");
            if (String.IsNullOrEmpty(ShortcutFile))
                throw new ArgumentNullException("ShortcutFile");

            // Create WshShellClass instance:
            var wshShell = new WshShell();

            // Create shortcut object:
            IWshRuntimeLibrary.IWshShortcut shorcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(ShortcutFile);

            // Assign shortcut properties:
            shorcut.TargetPath = TargetPath;
            shorcut.Description = Description;
            if (!String.IsNullOrEmpty(Arguments))
                shorcut.Arguments = Arguments;
            if (!String.IsNullOrEmpty(HotKey))
                shorcut.Hotkey = HotKey;
            if (!String.IsNullOrEmpty(WorkingDirectory))
                shorcut.WorkingDirectory = WorkingDirectory;

            // Save the shortcut:
            shorcut.Save();
        }
        */
        private void AddToAutostart_Click(object sender, EventArgs e)
        {

            try
            {

                WshShell wshShell = new WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut;
                string startUpFolderPath =
                  Environment.GetFolderPath(Environment.SpecialFolder.Startup);

                // Create the shortcut
                shortcut =
                  (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(
                    startUpFolderPath + "\\" +
                    ApplicationName + ".lnk");

                shortcut.TargetPath = ApplicationPath;
                shortcut.WorkingDirectory = Application.StartupPath;
                shortcut.Description = "Launch My Application";
                // shortcut.IconLocation = Application.StartupPath + @"\App.ico";
                shortcut.Save();

                Status_label.Text = "Success";
                Status_label.ForeColor = Color.Green;
                //pictureBox1.Image = Image.FromFile(Application."greencircle.png");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Status_label.Text = "Failed";
                Status_label.ForeColor = Color.Red;
            }

            /*


            // Make sure you use try/catch block because your App may has no permissions on the target path!
            try
            {
                CreateShortcut(@"C:\temp", @"C:\Users\Johnny\AppData\Roaming\Microsoft\MyShortcutFile.lnk",
                    "Custom Shortcut", "/param", "Ctrl+F", @"c:\");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


    */






            //C: \Users\Johnny\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Autostart
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RefreshServices_Click(object sender, EventArgs e)
        {

        }
    }
}
