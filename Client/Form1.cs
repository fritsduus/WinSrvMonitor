using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Akka.Actor;

namespace WinSrvMonitor.Client
{
    public partial class Form1 : Form
    {
        private IActorRef _displayActor;
        //private readonly Dictionary<string, MetricViewItem> _metricViewItems;
        private readonly DataTable _metricTable;

        public Form1()
        {
            InitializeComponent();
            _metricTable = new DataTable("Metrics");
            _metricTable.Columns.Add("Server", typeof(string));
            _metricTable.Columns.Add("Metric", typeof(string));
            _metricTable.Columns.Add("Value", typeof(float));
            _metricTable.Columns.Add("MovingAverage", typeof(float));
            _metricTable.Columns.Add("MovingAverageCalc", typeof(MovingAverage));
            //_metricTable.Rows.Add(new object[] { "Test", "Test", 10 });
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _metricTable;
            _displayActor = Program.MonitorActorSystem.ActorOf(
                Props.Create(() => new MetricDisplayActor(_metricTable, dataGridView1)), "displayMetrics");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _displayActor.Tell(PoisonPill.Instance);
        }
    }
}
