using System;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace AspNetCore.Uploader.Helper
{
    public class RequestValidator
    {
        /// <summary>
        ///     Content-Type:  multipart/form-data; boundary="----WebKitFormBoundarymx2fSWqWSd0OxQqq"
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="lengthLimit">Reasonable limit by https://tools.ietf.org/html/rfc2046#section-5.1 is 70</param>
        /// <returns></returns>
        public string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit = 70)
        {
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;

            if (string.IsNullOrWhiteSpace(boundary)) throw new InvalidDataException("Missing content-type boundary.");

            if (boundary.Length > lengthLimit)
                throw new InvalidDataException($"Multipart boundary length limit {lengthLimit} exceeded.");

            return boundary;
        }

        /// <summary>
        ///     MulitplartContentType is requird for FileUpload
        /// </summary>
        public bool IsMultipartContentType(string contentType)
        {
            return !string.IsNullOrEmpty(contentType)
                   && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
        }


        public bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
            return contentDisposition.DispositionType.Equals("form-data")
                   && (!string.IsNullOrEmpty(contentDisposition.FileName.Value)
                       || !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value));
        }
    }
}