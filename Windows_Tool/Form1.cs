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
//using Microsoft.WindowsAPICodePack.Net;
//using Microsoft.SDK.Samples.VistaBridge.Library.Network;


namespace Windows_Tool
{

    public partial class Form1 : Form
    {

        private List<NetworkInterfaceInfo> NetworkInterfaceInfos = new List<NetworkInterfaceInfo> ();

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
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\";
            //openFileDialog1.Filter = "exe files (*.exe, *.lnk)|*.exe;*.lnk";
            openFileDialog1.Filter = "exe files (*.exe)|*.exe";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                textBox1.Text = openFileDialog1.FileName;
            }
        }
        /*
        public static void CreateShortcut(string SourceFile, string ShortcutFile)
        {
            CreateShortcut(SourceFile, ShortcutFile, null, null, null, null);
        }

        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="SourceFile">A file you want to make shortcut to</param>
        /// <param name="ShortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        /// <param name="Description">Shortcut description</param>
        /// <param name="Arguments">Command line arguments</param>
        /// <param name="HotKey">Shortcut hot key as a string, for example "Ctrl+F"</param>
        /// <param name="WorkingDirectory">"Start in" shorcut parameter</param>
        public static void CreateShortcut(string TargetPath, string ShortcutFile, string Description,
           string Arguments, string HotKey, string WorkingDirectory)
        {
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(TargetPath))
                throw new ArgumentNullException("TargetPath");
            if (String.IsNullOrEmpty(ShortcutFile))
                throw new ArgumentNullException("ShortcutFile");

            // Create WshShellClass instance:
            var wshShell = new WshShellClass();

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
            /*
            // Make sure you use try/catch block because your App may has no permissions on the target path!
            try
            {
                CreateShortcut(@"C:\temp", @"C:\MyShortcutFile.lnk",
                    "Custom Shortcut", "/param", "Ctrl+F", @"c:\");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            /// <summary>
            /// Create Windows Shorcut
            /// </summary>
            /// <param name="SourceFile">A file you want to make shortcut to</param>
            /// <param name="ShortcutFile">Path and shorcut file name including file extension (.lnk)</param>
            
            */



            //C: \Users\Johnny\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Autostart
        }
    }
}
