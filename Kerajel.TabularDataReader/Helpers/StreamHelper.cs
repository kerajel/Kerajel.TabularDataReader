namespace Kerajel.TabularDataReader.Helpers;

public static class StreamHelper
{
    public static async Task<byte[]> ReadToByteArrayAsync(this Stream stream, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(stream);
        ct.ThrowIfCancellationRequested();

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
            await ReadExactlyAsync(stream, buffer, ct);
            return buffer;
        }
        else
        {
            using MemoryStream memoryStream = new();
            await stream.CopyToAsync(memoryStream, ct);
            return memoryStream.ToArray();
        }
    }

    private static async Task ReadExactlyAsync(Stream stream, byte[] buffer, CancellationToken ct = default)
    {
        int offset = 0;
        while (offset < buffer.Length)
        {
            int bytesRead = await stream.ReadAsync(buffer.AsMemory(offset, buffer.Length - offset), ct);
            if (bytesRead == 0)
            {
                throw new EndOfStreamException("Could not read the complete stream.");
            }
            offset += bytesRead;
            ct.ThrowIfCancellationRequested();
        }
    }
}