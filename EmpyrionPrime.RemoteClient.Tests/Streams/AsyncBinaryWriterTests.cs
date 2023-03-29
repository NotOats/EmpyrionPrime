using EmpyrionPrime.RemoteClient.Streams;
using System.Text;

namespace EmpyrionPrime.RemoteClient.Tests.Streams;

public class AsyncBinaryWriterTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async void Test_LeaveOpen(bool value)
    {
        using var stream = new MemoryStream();
        using var writer = new AsyncBinaryWriter(stream, leaveOpen: value);

        writer.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(async () => await writer.FlushAsync());
        Assert.Equal(value, stream.CanRead);
    }

    [Theory]
    [InlineData(0x00, 0)]
    [InlineData(0x00, 100)]
    [InlineData(0x10, 0)]
    [InlineData(0x10, 50)]
    [InlineData(0x49, 15615)]
    public async void Test_WriteBytes(byte element, int length)
    {
        var bytes = Enumerable.Repeat(element, length).ToArray();

        var (reader, writer) = await CreateWriter(async writer =>
        {
            await writer.WriteAsync(bytes);
        });

        var result = reader.ReadBytes(length);

        Assert.Equal(bytes.Length, result.Length);
        Assert.Equal(bytes, result);
    }

    [Theory]
    [InlineData((short)0)]
    [InlineData((short)3443)]
    [InlineData((short)-5181)]
    [InlineData(short.MinValue)]
    [InlineData(short.MaxValue)]
    public async void Test_WriteInt16(short value)
    {
        var (reader, writer) = await CreateWriter(async writer =>
        {
            await writer.WriteAsync(value);
        });

        var result = reader.ReadInt16();

        Assert.Equal(value, result);
    }

    [Theory]
    [InlineData((short)0)]
    [InlineData((ushort)3443)]
    [InlineData(ushort.MaxValue)]
    public async void Test_WriteUInt16(ushort value)
    {
        var (reader, writer) = await CreateWriter(async writer =>
        {
            await writer.WriteAsync(value);
        });

        var result = reader.ReadUInt16();

        Assert.Equal(value, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(321651)]
    [InlineData(-165816)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public async void Test_WriteInt32(int value)
    {
        var (reader, writer) = await CreateWriter(async writer =>
        {
            await writer.WriteAsync(value);
        });

        var result = reader.ReadInt32();

        Assert.Equal(value, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(23425)]
    [InlineData(uint.MaxValue)]
    public async void Test_WriteUInt32(uint value)
    {
        var (reader, writer) = await CreateWriter(async writer =>
        {
            await writer.WriteAsync(value);
        });

        var result = reader.ReadUInt32();

        Assert.Equal(value, result);
    }


    private static async Task<(BinaryReader, AsyncBinaryWriter)> CreateWriter(Func<AsyncBinaryWriter, Task> filler)
    {
        var stream = new MemoryStream();
        var writer = new AsyncBinaryWriter(stream, leaveOpen: false);
        var reader = new BinaryReader(stream, Encoding.Default, false);

        await filler.Invoke(writer);
        await writer.FlushAsync();

        stream.Seek(0, SeekOrigin.Begin);

        return (reader, writer);
    }
}
