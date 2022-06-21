namespace KWS.Infrastructure
{
    public class ConnectionSettings
    {
        public DbSettings AbsDbSettings { get; set; }

        public DbSettings HRDbSettings { get; set; }

        public DbSettings InvDbSettings { get; set; }

        public DbSettings BillgenixDbSettings { get; set; }

        public DbSettings DashboardDbSettings { get; set; }
    }
}
