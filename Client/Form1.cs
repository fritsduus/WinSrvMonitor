using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Akka.Actor;
using WinSrvMonitor.Client.ViewModels;
using WinSrvMonitor.Collector;

namespace WinSrvMonitor.Client
{
    public partial class Form1 : Form
    {
        private IActorRef _displayActor;
        private readonly BindingSource _serverMetricsBindingSource = new BindingSource();

        private readonly IList<ServerMetrics> _serverMetrics;

        public Form1()
        {
            InitializeComponent();
            BindingList<ServerMetrics> list = new BindingList<ServerMetrics>();
            _serverMetrics = list; //new List<ServerMetrics>();

            _serverMetricsBindingSource.DataSource = list;
            gridControl1.DataSource = _serverMetricsBindingSource;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _displayActor = Program.MonitorActorSystem.ActorOf(
                Props.Create(() => new MetricDisplayActor(_serverMetrics, gridControl1)), "displayMetrics");

            MetricCollectorFactory collectorFactory = new MetricCollectorFactory(Program.MonitorActorSystem);
            int collectorIntervalMs = int.Parse(ConfigurationManager.AppSettings["CollectorIntervalMs"]);

            string[] servers =
                ConfigurationManager.AppSettings["OtherServers"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for(int i = 0; i < servers.Length; i++)
            {
                collectorFactory.CreatePerformanceCounterActors("Other", servers[i].Trim(), collectorIntervalMs, _displayActor);
            }

            servers =
                ConfigurationManager.AppSettings["WebfarmServers"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < servers.Length; i++)
            {
                collectorFactory.CreatePerformanceCounterActors("Webfarm", servers[i].Trim(), collectorIntervalMs, _displayActor);
            }

            //collectorFactory.CreatePerformanceCounterActors("Other", "iWeb02", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Other", "web6", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Other", "ES-DATA04", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Other", "ES-DATA05", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Other", "ES-DATA06", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Other", "Redis.shgdmz.dk", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Other", "RavenDbNode04.shgdmz.dk", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Webfarm", "web8.shgdmz.dk", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Webfarm", "web9.shgdmz.dk", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Webfarm", "web10.shgdmz.dk", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Webfarm", "web11.shgdmz.dk", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Webfarm", "web12.shgdmz.dk", collectorIntervalMs, _displayActor);
            //collectorFactory.CreatePerformanceCounterActors("Webfarm", "web13.shgdmz.dk", collectorIntervalMs, _displayActor);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _displayActor.Tell(PoisonPill.Instance);
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            ServerMetrics serverMetrics = (ServerMetrics) gridView1.GetRow(e.RowHandle);
            if (serverMetrics.HasError)
                e.Appearance.BackColor = Color.DarkSalmon;
        }
    }
}
