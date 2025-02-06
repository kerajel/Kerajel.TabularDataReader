namespace Kerajel.TabularDataReader.Helpers;

public static class StreamHelper
{
    public static async Task<byte[]> ReadToByteArrayAsync(this Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        if (stream.CanSeek)
        {
            if (stream.Position != 0)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            long length = stream.Length;
            if (length > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(stream), "Stream is too long to be read into a single byte array.");
            }

            byte[] buffer = new byte[length];
            await ReadExactlyAsync(stream, buffer);
            return buffer;
        }
        else
        {
            using MemoryStream memoryStream = new();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

    private static async Task ReadExactlyAsync(Stream stream, byte[] buffer)
    {
        int offset = 0;
        int remaining = buffer.Length;
        while (remaining > 0)
        {
            int bytesRead = await stream.ReadAsync(buffer.AsMemory(offset, remaining));
            if (bytesRead == 0)
            {
                throw new EndOfStreamException("Reached the end of the stream before reading the required number of bytes.");
            }
            offset += bytesRead;
            remaining -= bytesRead;
        }
    }
}