namespace Coworking.Application.Options
{
    public class BlobOptions
    {
        public static string Position = "BlobOptions";

        public string ConnectionString { get; set; }
        public string AccessKey { get; set; }
        public string EndpointRoute { get; set; }
    }
}