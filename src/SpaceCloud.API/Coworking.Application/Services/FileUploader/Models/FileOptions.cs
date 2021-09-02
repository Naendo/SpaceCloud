namespace AspNetCore.Uploader
{
    public class FileOptions
    {
        public const string POSITION = "FileOptions";

        public int SizeLimit { get; set; }
        public string[] PermittedExtensions { get; set; }
    }
}