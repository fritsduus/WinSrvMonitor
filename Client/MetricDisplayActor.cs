using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using Akka.Actor;
using WinSrvMonitor.Messages;

namespace WinSrvMonitor.Client
{
    public class MetricDisplayActor : ReceiveActor
    {
        private readonly DataGridView _dataGridView;
        private readonly DataTable _metricTable;
        private readonly ActorSelection _metricCollector;

        public MetricDisplayActor(DataTable metricTable, DataGridView dataGridView)
        {
            _dataGridView = dataGridView;
            _metricTable = metricTable;
            Receive<Metric>(m => HandleMetric(m));

            string monitorServer = ConfigurationManager.AppSettings["MonitorServer"];
            string metricCollectorPath = string.Format("akka.tcp://WinSrvMonitorServer@{0}:8041/user/metricDistributer", monitorServer);
            _metricCollector = Program.MonitorActorSystem.ActorSelection(metricCollectorPath);
            _metricCollector.Tell(new SubscribeToMetrics(Self));
        }

        private void HandleMetric(Metric m)
        {
            DataRow[] rows = _metricTable.Select($"Server = '{m.ServerName}' AND Metric = '{m.MetricName}'");
            if (rows.Any())
            {
                rows[0].SetField<float>(2, m.Value);
            }
            else
            {
                _metricTable.Rows.Add(new object[] { m.ServerName, m.MetricName, m.Value });
            }
            _dataGridView.Update();
        }

        protected override void PostStop()
        {
            _metricCollector.Tell(new UnsubscribeToMetrics(Self));
            base.PostStop();
        }
    }
}
