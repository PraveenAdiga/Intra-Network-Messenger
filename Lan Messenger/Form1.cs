using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;
using System.Collections;

namespace Lan_messenger
{

    struct my_data_struct
    {
        public string connection_type;
        public string resolve_name;
        public TcpClient client;
        public Socket sock;
    }
    struct form_entry
    {
        public string status;
        public Form2 form_refrence;

    }
    public partial class Form1 : Form
    {
        public static List<Thread> myThreads = new List<Thread>();
        public static List<Socket> socket_list = new List<Socket>();
        public static int check = 0;
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        public bool exit = true;
        public static int c = 0;
        static Hashtable arp_cache = new Hashtable();
        IPAddress localaddress = IPAddress.Parse("192.168.235.1");
        static int i, j, k, inr = 0;
        static Hashtable form_hash_table = new Hashtable();
        static Hashtable live_list = new Hashtable();
        static List<TcpListener> listener_list = new List<TcpListener>();
        static List<StreamReader> read_stream_list = new List<StreamReader>();
        static List<StreamWriter> write_stream_list = new List<StreamWriter>();
        public void online_radio_clicked()
        {
            try
            {
                Thread t = new Thread(() => Main1(listView1, live_list));
                myThreads.Add(t);
                t.Start();
            }
            catch (Exception k)
            {
                //MessageBox.Show("online1" + k.Message); 
            }

            try
            {
                Thread t1 = new Thread(() => list_listner());
                myThreads.Add(t1);
                t1.Start();
                button1.Enabled = true;
            }

            catch (Exception p)
            {//MessageBox.Show("online2"+p.Message);
            }
            //MessageBox.Show("jjjjjj");


        }
        public void offline_radio_clicked()
        {
            //this.Hide();
            try
            {
                live_list.Clear();
                try
                {
                    foreach (var t in myThreads)
                        t.Abort();
                }
                catch (Exception w)
                { }

                try
                {
                    foreach (var t in read_stream_list)
                        t.Close();
                }
                catch (Exception w)
                { }
                try
                {
                    foreach (var t in write_stream_list)
                        t.Close();
                }
                catch (Exception w)
                { }

                try
                {
                    foreach (var t in socket_list)
                    {
                        t.Disconnect(false);
                    }
                }
                catch (Exception e)
                { //MessageBox.Show("start_listner :" + e.Message); 
                }


                try
                {
                    foreach (var t in listener_list)
                        t.Stop();
                }
                catch (Exception w)
                { }

                myThreads.Clear();

                read_stream_list.Clear();
                write_stream_list.Clear();

                // form_hash_table.Clear();


                button1.Enabled = false;
                listView1.Items.Clear();
                foreach (DictionaryEntry t in form_hash_table)
                {
                    form_entry r;

                    r = (form_entry)t.Value;
                    (r.form_refrence).myself_offline();

                }
            }
            catch (Exception p)
            { MessageBox.Show("offline clicked "); }
        }

        public static object hash_obj()
        {
            return form_hash_table;
        }
        public bool CheckOpened(string name)
        {
            //foreach (DictionaryEntry de in form_hash_table)
            //  MessageBox.Show(de.Key.ToString());
            if (!form_hash_table.ContainsKey(name))
                return false;
            else return true;
        }
        public Form2 returnOpened(string name)
        {
            form_entry t;
            t = (form_entry)form_hash_table[name];
            return (t.form_refrence);

        }
        public void list_listner()
        {
            try
            {
                string array = network_interface();
                string[] network_ips = array.Split('|');
                // MessageBox.Show(array);
                foreach (var intrfc_ip in network_ips)
                {
                    Thread t = new Thread(() => interface_thread(intrfc_ip));
                    myThreads.Add(t);
                    t.IsBackground = true;
                    t.Start();


                }
            }

            catch (Exception p)
            {
                //MessageBox.Show("List_listner error:" + p.Message);
            }

        }
        public void refresh()
        {
            //  listView1.Items.Clear();
            foreach (DictionaryEntry de in live_list)
            {

                my_data_struct n1;
                n1 = (my_data_struct)live_list[de.Key];
                ListViewItem item = new ListViewItem(n1.resolve_name);
                item.SubItems.Add(de.Key.ToString());


                listView1.Items.Add(item);
            }
        }
        public void read_stream(StreamReader sr, string name, string ip)
        {
            try
            {
                Thread t = null;
                Form2 f2;
                int c = 1;
                int c1 = 0;
                string read_txt = null;

                while (true)
                {
                    c1++;
                    string inp_msg = null;



                    if ((live_list.ContainsKey(ip)))
                    {
                        // MessageBox.Show("killed1");
                        inp_msg = sr.ReadLine();

                    }
                    else
                        sr.Close();
                    if (inp_msg == null || inp_msg == "\r\n" || inp_msg == "\n" || inp_msg == "")
                    {
                        // MessageBox.Show("readthreadabort");
                        live_list.Remove(ip);
                        listView1.Items.Clear();
                        refresh();
                        sr.Close();
                        return;
                    }

                    if (!(CheckOpened(name)))
                    {
                        f2 = new Form2(name);
                        form_entry m1 = new form_entry();
                        m1.form_refrence = f2;
                        form_hash_table.Add(name, m1);

                        t = new Thread(() => Application.Run(f2));

                        t.Start();
                        Thread.Sleep(60);

                    }
                    else
                        f2 = returnOpened(name);




                    if (CheckOpened(name))
                    {
                        f2 = returnOpened(name);
                        f2.set_text(name + ":" + inp_msg);

                    }

                    if (!(live_list.ContainsKey(ip)))
                    {
                        //MessageBox.Show("killed2");
                        return;
                    }
                    Thread.Sleep(100);
                }

            }
            catch (Exception e)
            {

                if (e.GetType().ToString() == "System.IO.IOException")
                {

                    live_list.Remove(ip);
                    listView1.Items.Clear();
                    refresh();
                    sr.Close();
                    return;
                }
                if (e.GetType().ToString() == "System.Threading.ThreadAbortException")
                {
                    //MessageBox.Show("readthreadabort");
                    live_list.Remove(ip);
                    //listView1.Items.Clear();
                    refresh();
                    sr.Close();
                    return;
                }


            }

        }
        public void req_write_stream(StreamWriter sw, string name)
        {
            try
            {
                Form2 f2;
                if (!CheckOpened(name))
                {
                    f2 = new Form2(name);
                    f2.Show();
                }
                else
                    f2 = returnOpened(name);

                try
                {
                    while (true)
                    {
                        string var = f2.get_text();
                        if (var != null)
                            sw.WriteLine("{0}", var);
                    }
                }
                catch (Exception e)
                {
                    //    MessageBox.Show("write stream error" + e.Message); 
                }
            }
            catch (Exception k)
            {
                //MessageBox.Show(k.Message); 
            }


        }
        public void list_listn_write_stream(StreamWriter sw, string name, string ip)
        {
            try
            {

                Form2 f2 = null;
                int c = 1;
                while (!CheckOpened(name))
                {
                    if (CheckOpened(name))
                        f2 = returnOpened(name);

                    Thread.Sleep(100);
                }
                if (CheckOpened(name))
                    f2 = returnOpened(name);
                string var1 = null;
                f2.signed_in();
                while (true)
                {

                    try
                    {

                        while (!CheckOpened(name))
                        {

                            if (CheckOpened(name))
                                f2 = returnOpened(name);
                            if (!(live_list.ContainsKey(ip)))
                                return;
                            Thread.Sleep(100);
                        }
                    }
                    catch (Exception k)
                    { //MessageBox.Show("outer" + k.Message); 
                    }

                    if (CheckOpened(name))
                    {
                        try
                        {

                            f2 = returnOpened(name);


                            var1 = f2.get_text();
                        }
                        catch (Exception e)
                        { //MessageBox.Show("listn_write stream error" + e.Message); 
                        }
                    }
                    try
                    {
                        if (var1 != null && var1 != "\r\n" && var1 != "\n" && var1 != "")
                        {

                            sw.WriteLine("{0}", var1);

                            if (CheckOpened(name))
                                f2.txt = null;
                        }

                        if (!(live_list.ContainsKey(ip)))
                        {
                            f2.signed_off(name);
                            sw.Close();
                            return;
                        }
                        Thread.Sleep(100);
                    }
                    catch (Exception e)
                    { //MessageBox.Show("writ_str" + e.Message); 
                    }
                }

            }
            catch (Exception e)
            { }
        }
        public void Service(Socket soc, string name, string ipaddr)
        {
            try
            {
                Stream s = new NetworkStream(soc);
                StreamReader sr = new StreamReader(s);
                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true; // enable automatic flushing
                read_stream_list.Add(sr);
                write_stream_list.Add(sw);
                Thread rd_thrd = new Thread(() => this.read_stream(sr, name, ipaddr));
                myThreads.Add(rd_thrd);
                rd_thrd.IsBackground = true;

                rd_thrd.Start();
                Thread wrt_thrd = new Thread(() => this.list_listn_write_stream(sw, name, ipaddr));
                myThreads.Add(wrt_thrd);
                wrt_thrd.IsBackground = true;

                wrt_thrd.Start();

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);


            }
            //soc.Close();

        }
        public Form1()
        {
            try
            {
                InitializeComponent();
                //IPInfo m = new IPInfo();
                //new Thread(m.Main1(listView1)).Start();
                my_data_struct obj;
                Thread t = new Thread(() => Main1(listView1, live_list));
                myThreads.Add(t);
                t.IsBackground = true;

                t.Start();
                Thread t1 = new Thread(() => list_listner());
                myThreads.Add(t1);
                t1.IsBackground = true;

                t1.Start();


                trayMenu = new ContextMenu();
                trayMenu.MenuItems.Add("Exit", OnExit);
                trayMenu.MenuItems.Add("Restore", restore_func);

                // Create a tray icon. In this example we use a
                // standard system icon for simplicity, but you
                // can of course use your own custom icon too.
                trayIcon = new NotifyIcon();
                trayIcon.Text = "Lan Messenger";

                System.Drawing.Icon icnTask;
                System.IO.Stream st=null;
                System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                st = a.GetManifestResourceStream("Lan_messenger.network.ico");
                //icnTask = new System.Drawing.Icon(st); 

                try
                {
                    trayIcon.Icon = new Icon(st);
                }
                catch (Exception w)
                { MessageBox.Show(w.Message); }
                trayIcon.Click += trayIcon_Click;
                // Add menu to tray icon and show it.
                trayIcon.BalloonTipText = "You are online";
                trayIcon.BalloonTipTitle = "Lan Messenger";
                trayIcon.ContextMenu = trayMenu;
            }
            catch (Exception e)
            { }
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null)
            {
                Form2 frm;
                if (!(CheckOpened(listView1.SelectedItems[0].Text)))
                {
                    try
                    {
                        string selected_ip = listView1.SelectedItems[0].SubItems[1].Text;
                        frm = new Form2(listView1.SelectedItems[0].Text);
                        form_entry f1 = new form_entry();
                        f1.form_refrence = frm;

                        form_hash_table.Add(listView1.SelectedItems[0].Text, f1);

                        new Thread(() => Application.Run(frm)).Start();
                        string selected_name = listView1.SelectedItems[0].Text;
                        //Thread t2 = new Thread(new ThreadStart(() => request(selected_ip, selected_name)));
                        //t2.Start();
                    }

                    catch (Exception p)
                    {
                        //MessageBox.Show(p.Message);
                    }

                }



            }
            //MessageBox.Show(listView1.SelectedItems[0].Text);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() => Main1(listView1, live_list));
            myThreads.Add(t);
            t.IsBackground = true;

            t.Start();
        }

        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception e)
            { }
        }
        public void Service(TcpClient client, string name, string ipaddr)
        {
            try
            {
                Stream s = client.GetStream();
                StreamReader sr = new StreamReader(s);
                StreamWriter sw = new StreamWriter(s);
                read_stream_list.Add(sr);
                write_stream_list.Add(sw);
                sw.AutoFlush = true;
                Thread rd_thrd = new Thread(() => read_stream(sr, name, ipaddr));
                myThreads.Add(rd_thrd);
                rd_thrd.IsBackground = true;

                rd_thrd.Start();
                Thread wrt_thrd = new Thread(() => list_listn_write_stream(sw, name, ipaddr));
                myThreads.Add(wrt_thrd);
                wrt_thrd.IsBackground = true;

                wrt_thrd.Start();
                // s.Close();
            }
            catch (Exception e)
            {

                // client.Close();
            }
        }

        static string network_interface()
        {
            string local_ip_set = null;

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces().Where(n => n.OperationalStatus == OperationalStatus.Up))
            {
                if ((ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
                {

                    //Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            string[] correct_ips = ip.Address.ToString().Split('.');
                            if (!(correct_ips[0] == "169"))
                                local_ip_set = local_ip_set + ip.Address.ToString() + "|";
                        }
                    }
                }
            }
            return local_ip_set.Remove(local_ip_set.Length - 1);
        }

        private static string GetARPResult()
        {
            Process p = null;
            string output = string.Empty;

            try
            {
                p = Process.Start(new ProcessStartInfo("arp", "-a")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                });

                output = p.StandardOutput.ReadToEnd();

                p.Close();
            }
            catch (Exception ex)
            {
                // throw new Exception("IPInfo: Error Retrieving 'arp -a' Results", ex);
            }
            finally
            {
                if (p != null)
                {
                    p.Close();
                }
            }

            return output;
        }
        public void Main1(ListView listView1, Hashtable live_list)
        {
            try
            {
                if (check == 1)
                    return;
                check = 1;
                //k = 0;
                //live_list.Clear();
                listView1.Items.Clear();
                string name = "";
                disconnect(listView1, live_list);
                foreach (DictionaryEntry de in arp_cache)
                {

                    if (!(live_list.ContainsKey(de.Key)))
                    {
                        Thread connect_thread = new Thread(() => connect(de.Key.ToString(), listView1, live_list));
                        myThreads.Add(connect_thread);
                        connect_thread.IsBackground = true;

                        connect_thread.Start();
                    }


                }

                check = 0;
            }
            catch (Exception e)
            {
                //MessageBox.Show("Main1" + e.Message); 
            }
        }
        public void disconnect(ListView listView1, Hashtable live_list)
        {
            arp_cache.Clear();
            foreach (var arp in GetARPResult().Split(new char[] { '\n', '\r' }))
            {


                if (!string.IsNullOrEmpty(arp))
                {
                    var pieces = (from piece in arp.Split(new char[] { ' ', '\t' })
                                  where !string.IsNullOrEmpty(piece)
                                  select piece).ToArray();
                    if (pieces.Length == 3)
                    {


                        if (pieces[2] == "dynamic")
                        {
                            try
                            {
                                arp_cache.Add(pieces[0], c++);
                            }
                            catch (Exception e)
                            {
                                //MessageBox.Show("diconnect" + e.Message);
                            }
                        }


                    }
                }


            }

            foreach (DictionaryEntry de in live_list)
            {
                if (!(arp_cache.ContainsKey(de.Key)))
                {
                    live_list.Remove(de.Key);

                }
                else
                {
                    my_data_struct n1;
                    n1 = (my_data_struct)live_list[de.Key];
                    ListViewItem item = new ListViewItem(n1.resolve_name);
                    item.SubItems.Add(de.Key.ToString());


                    listView1.Items.Add(item);
                }

            }


        }
        public void connect(string ipadress, ListView listView1, Hashtable live_list)
        {
            try
            {
                TcpClient tcp_check = new TcpClient();
                try
                {
                    tcp_check.Connect(ipadress, 2055);
                }
                catch (Exception e)
                {
                    return;
                }
                string name = null;
                try
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(ipadress);
                    name = hostEntry.HostName;
                }
                catch (SocketException ex)
                {
                    name = "?";
                }

                //string name = "?";
                my_data_struct node = new my_data_struct();
                node.connection_type = "client";
                node.client = tcp_check;
                node.sock = null;
                node.resolve_name = name;
                live_list.Add(ipadress, node);
                ListViewItem item = new ListViewItem(name);
                item.SubItems.Add(ipadress);
                listView1.Items.Add(item);
                Thread service_thread = new Thread(() => Service(tcp_check, name, ipadress));
                myThreads.Add(service_thread);
                service_thread.IsBackground = true;

                service_thread.Start();


            }
            //  listView1.SelectedItems[i].SubItems[0] = name;
            //listView1.SelectedItems[i].SubItems[1] = pieces[0];
            catch (Exception e)
            { //MessageBox.Show("connect" + e.Message); 
            }
        }
        public void interface_thread(string interface_ipaddress)
        {
            try
            {
                TcpListener listener1;
                listener1 = new TcpListener(IPAddress.Parse(interface_ipaddress), 2055);
                listener_list.Add(listener1);
                while (true)
                {

                    try
                    {
                        listener1.Start();
                        // MessageBox.Show("hell");
                        Socket soc1 = listener1.AcceptSocket();
                        socket_list.Add(soc1);
                        //MessageBox.Show("hell1");

                        string c_ip1 = (((IPEndPoint)soc1.RemoteEndPoint).Address.ToString());

                        string name = "";

                        if (!live_list.ContainsKey(c_ip1))
                        {

                            try
                            {
                                IPHostEntry hostEntry = Dns.GetHostEntry(c_ip1);
                                name = hostEntry.HostName;
                            }
                            catch (SocketException ex)
                            {
                                name = "?";
                            }
                            my_data_struct node = new my_data_struct();
                            node.connection_type = "server";
                            node.sock = soc1;
                            node.client = null;
                            node.resolve_name = name;
                            live_list.Add(c_ip1, node);


                            ListViewItem current_ip = new ListViewItem(name);
                            current_ip.SubItems.Add(c_ip1);
                            listView1.Items.Add(current_ip);
                            Thread service_thread1 = new Thread(() => Service(soc1, name, c_ip1));
                            myThreads.Add(service_thread1);
                            service_thread1.IsBackground = true;
                            service_thread1.Start();


                        }
                    }
                    catch (Exception e)
                    { }

                    Thread.Sleep(10);

                }
            }
            catch (Exception q)
            {
                // MessageBox.Show(q.GetType().ToString());
                // MessageBox.Show("k" + q.Message); 
            }
        }


        private void offline_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("off");
            if (offline_radioButton.Checked)
            {
                offline_radio_clicked();
            }
        }

        private void online_radiobutton_CheckedChanged(object sender, EventArgs e)
        {

            if (online_radiobutton.Checked)
            {
                //  MessageBox.Show("on");
                online_radio_clicked();
            }
        }

        private void busyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception u) { }
        }

        private void developersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 n1 = new AboutBox1();
            new Thread(() => Application.Run(n1)).Start();

        }


        private void OnExit(object sender, EventArgs e)
        {
            exit = false;
            Application.Exit();
        }
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                // Release the icon resource.
                trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }
        protected override void OnLoad(EventArgs e)
        {
            Visible = true; // Hide form window.
            ShowInTaskbar = true; // Remove from taskbar.

            base.OnLoad(e);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                trayIcon.Visible = true;
                ShowInTaskbar = true;
                //ToolTipIcon tip=new ToolTipIcon();
                trayIcon.ShowBalloonTip(3000, "Lan Messenger", "You are Online", ToolTipIcon.Info);
                trayIcon.BalloonTipClicked += trayIcon_BalloonTipClicked;
                //Application.Exit();
                e.Cancel = exit;
                exit = false;
                this.Hide();

            }
            catch (Exception u) { }
        }

        void trayIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            exit = true;
            this.Show();
        }

        void trayIcon_Click(object sender, EventArgs e)
        {
            exit = true;
            this.Show();
        }

        private void restore_func(object sender, EventArgs e)
        {
            exit = true;
            this.Show();
        }








    }
}
