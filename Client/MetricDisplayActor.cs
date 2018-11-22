using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Akka.Actor;
using DevExpress.XtraGrid;
using WinSrvMonitor.Client.ViewModels;
using WinSrvMonitor.Messages;
using Metric = WinSrvMonitor.Messages.Metric;
using System;
using System.Globalization;

namespace WinSrvMonitor.Client
{
    public class MetricDisplayActor : ReceiveActor
    {
        private readonly IList<ServerMetrics> _serverMetrics;
        private readonly GridControl _gridControl;
        private readonly ActorSelection _metricCollector;

        public MetricDisplayActor(IList<ServerMetrics> serverMetrics, GridControl gridControl)
        {
            _serverMetrics = serverMetrics;
            _gridControl = gridControl;
            Receive<Metric>(m => HandleMetric(m));

            string monitorServer = ConfigurationManager.AppSettings["MonitorServer"];
            if (string.IsNullOrEmpty(monitorServer) || monitorServer == "localhost")
            {

            }
            else
            {
                string metricCollectorPath =
                    $"akka.tcp://WinSrvMonitorServer@{monitorServer}:8041/user/metricDistributer";
                _metricCollector = Program.MonitorActorSystem.ActorSelection(metricCollectorPath);
                _metricCollector.Tell(new SubscribeToMetrics(Self));
            }
        }

        private void HandleMetric(Metric m)
        {
            ServerMetrics serverMetrics = _serverMetrics.FirstOrDefault(sm => sm.Name == m.ServerName);
            if (serverMetrics == null)
            {
                serverMetrics = new ServerMetrics(m.GroupName, m.ServerName);
                _serverMetrics.Add(serverMetrics);

                //_gridControl.Views[0].RefreshData();
            }
            serverMetrics.UpdateMetric(m);

            if (m.GroupName == "Webfarm" && m.MetricName == "RequestsPerSec")
            {
                LogTotalRequestPerSec();
            }
        }

        protected override void PostStop()
        {
            _metricCollector?.Tell(new UnsubscribeToMetrics(Self));
            base.PostStop();
        }

        private DateTime _lastLogWrite = DateTime.Now;
        private static CultureInfo _cultureInfo;

        static MetricDisplayActor()
        {
            _cultureInfo = new CultureInfo("dk-DK");
            _cultureInfo.NumberFormat.NumberGroupSeparator = "";
        }

        private void LogTotalRequestPerSec()
        {
            if ((DateTime.Now - _lastLogWrite).TotalSeconds < 10)
                return;

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Temp\ReqPerSec.log", true))
                {
                    float totalReqPerSec = _serverMetrics.Where(sm => sm.Group == "Webfarm").Sum(sm => sm.RequestsPerSec.Value);
                    file.WriteLine($"{DateTime.Now};{totalReqPerSec.ToString("n0", _cultureInfo)}");
                    file.Flush();
                }
                _lastLogWrite = DateTime.Now;
            }
            catch
            { }
        }
    }
}
