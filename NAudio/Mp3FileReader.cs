using System.IO;

// ReSharper disable once CheckNamespace
namespace NAudio.Wave
{

    /// <summary>
    /// Class for reading from MP3 files
    /// </summary>
    public class Mp3FileReader : Mp3FileReaderBase
    {
        public static Mp3FileReader TryOpen(FileStream stream)
        {
            byte[] first_bytes = new byte[3];
            stream.Read(first_bytes, 0, 3);
            stream.Position = 0;
            if ((first_bytes[0] == 0x49 && first_bytes[1] == 0x44 && first_bytes[2] == 0x33) || first_bytes[0] == 0xFF && first_bytes[1] == 0xFB)
                return new Mp3FileReader(stream, true);
            return null;
        }

        /// <summary>Supports opening a MP3 file</summary>
        private Mp3FileReader(Stream inputStream, bool ownInputStream)
            : base(inputStream, CreateAcmFrameDecompressor, ownInputStream)
        {
        }

        /// <summary>Supports opening a MP3 file</summary>
        public Mp3FileReader(string mp3FileName)
            : base(File.OpenRead(mp3FileName), CreateAcmFrameDecompressor, true)
        {
        }

        /// <summary>
        /// Opens MP3 from a stream rather than a file
        /// Will not dispose of this stream itself
        /// </summary>
        /// <param name="inputStream">The incoming stream containing MP3 data</param>
        public Mp3FileReader(Stream inputStream)
            : base(inputStream, CreateAcmFrameDecompressor, false)
        {

        }

        /// <summary>
        /// Creates an ACM MP3 Frame decompressor. This is the default with NAudio
        /// </summary>
        /// <param name="mp3Format">A WaveFormat object based </param>
        /// <returns></returns>
        public static IMp3FrameDecompressor CreateAcmFrameDecompressor(WaveFormat mp3Format)
        {
            // new DmoMp3FrameDecompressor(this.Mp3WaveFormat); 
            return new AcmMp3FrameDecompressor(mp3Format);
        }
    }
}
