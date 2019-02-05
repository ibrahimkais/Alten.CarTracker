namespace Alten.CarTracker.Infrastructure.ServiceDiscovery
{
    public class ConsulConfig
    {
        public string Address { get; set; }
        public string ServiceName { get; set; }
        public string ServiceID { get; set; }

		public string HealthCheckTemplate { get; set; }
	}
}