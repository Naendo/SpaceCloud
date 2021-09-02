using System;

namespace AspNetCore.Uploader.Exceptions
{
    [Serializable]
    public class FileUploadException : Exception
    {
        public FileUploadException(string message) : base(message)
        {
        }
    }
}