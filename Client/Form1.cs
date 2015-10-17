using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Akka.Actor;

namespace WinSrvMonitor.Client
{
    public partial class Form1 : Form
    {
        private IActorRef _displayActor;
        private readonly Dictionary<string, MetricViewItem> _metricViewItems;
        private readonly DataTable _metricTable;

        public Form1()
        {
            InitializeComponent();
            _metricViewItems = new Dictionary<string, MetricViewItem>();
            _metricTable = new DataTable("Metrics");
            _metricTable.Columns.Add("Server", typeof(string));
            _metricTable.Columns.Add("Metric", typeof(string));
            _metricTable.Columns.Add("Value", typeof(float));
            //_metricTable.Rows.Add(new object[] { "Test", "Test", 10 });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _metricViewItems.Add("Test", new MetricViewItem { Server = "Test", Metric = "Test", Value = 10 });

            dataGridView1.DataSource = _metricTable; // _metricViewItems.Values;
            _displayActor = Program.MonitorActorSystem.ActorOf(
                Props.Create(() => new MetricDisplayActor(_metricTable, dataGridView1)), "displayMetrics");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _displayActor.Tell(PoisonPill.Instance);
        }
    }
}
