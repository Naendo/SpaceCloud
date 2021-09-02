using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Uploader.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace AspNetCore.Uploader.Helper
{
    public class StreamValidator
    {
        private readonly byte[] _allowedChars = { };

        private readonly Dictionary<string, List<byte[]>> _fileSignature = new Dictionary<string, List<byte[]>>
        {
            {".png", new List<byte[]> {new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A}}},
            {
                ".jpeg", new List<byte[]>
                {
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE2},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE3}
                }
            },
            {
                ".jpg", new List<byte[]>
                {
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE1},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE8}
                }
            }
        };

        public StreamValidator(Dictionary<string, List<byte[]>> fileSignature, byte[] allowedChars)
        {
            _allowedChars = allowedChars;
        }

        public StreamValidator()
        {
        }

        public async Task<byte[]> ProcessStreamedFile(
            MultipartSection section, ContentDispositionHeaderValue contentDisposition, string[] permittedExtensions,
            long sizeLimit)
        {
            await using (var memoryStream = new MemoryStream())
            {
                await section.Body.CopyToAsync(memoryStream);

                // Check if the file is empty or exceeds the size limit.
                if (memoryStream.Length == 0) return new byte[0];

                if (memoryStream.Length > sizeLimit)
                {
                    var megabyteSizeLimit = sizeLimit / 1048576;
                    throw new FileUploadException($"The file exceeds {megabyteSizeLimit:N1} MB.");
                }

                if (!IsValidFileExtensionAndSignature(contentDisposition.FileName.Value, memoryStream,
                    permittedExtensions))
                    throw new FileUploadException(
                        "The file type isn't permitted or the file's " +
                        "signature doesn't match the file's extension.");
                return memoryStream.ToArray();
            }
        }

        private bool IsValidFileExtensionAndSignature(string fileName, Stream data, string[] permittedExtensions)
        {
            if (string.IsNullOrEmpty(fileName) || data.Length == 0) return false;

            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext)) return false;

            data.Position = 0;

            using (var reader = new BinaryReader(data))
            {
                if (ext.Equals(".txt") || ext.Equals(".csv") || ext.Equals(".prn"))
                {
                    if (_allowedChars.Length == 0)
                    {
                        // Limits characters to ASCII encoding.
                        for (var i = 0; i < data.Length; i++)
                            if (reader.ReadByte() > sbyte.MaxValue)
                                return false;
                    }
                    else
                    {
                        // Limits characters to ASCII encoding and
                        // values of the _allowedChars array.
                        for (var i = 0; i < data.Length; i++)
                        {
                            var b = reader.ReadByte();
                            if (b > sbyte.MaxValue ||
                                !_allowedChars.Contains(b))
                                return false;
                        }
                    }

                    return true;
                }

                // File signature check
                // --------------------
                // With the file signatures provided in the _fileSignature
                // dictionary, the following code tests the input content's
                // file signature.
                var signatures = _fileSignature[ext];
                var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                return signatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature));
            }
        }
    }
}