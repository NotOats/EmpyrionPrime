using EmpyrionPrime.RemoteClient.Streams;
using System.Text;

namespace EmpyrionPrime.RemoteClient.Tests.Streams;

public class AsyncBinaryReaderTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async void Test_LeaveOpen(bool value)
    {
        using var stream = new MemoryStream();
        var reader = new AsyncBinaryReader(stream, leaveOpen: value);

        reader.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(async () => await reader.ReadByteAsync());
        Assert.Equal(value, stream.CanRead);
    }

    [Theory]
    [InlineData(0x00, 0)]
    [InlineData(0x00, 100)]
    [InlineData(0x10, 0)]
    [InlineData(0x10, 50)]
    [InlineData(0x49, 15615)]
    public async void Test_ReadBytes(byte element, int length)
    {
        var bytes = Enumerable.Repeat(element, length).ToArray();

        using var reader = CreateReader(writer => writer.Write(bytes));
        var result = await reader.ReadAsync(length);

        Assert.Equal(bytes.Length, result.Length);
        Assert.Equal(bytes, result);
    }

    [Fact]
    public async void Test_ReadBytesCancel()
    {
        // Large buffer so we can cancell mid read
        var length = 50_000_000;
        var bytes = Enumerable.Repeat<byte>(0x70, length).ToArray();

        using var reader = CreateReader(writer => writer.Write(bytes));

        var cts = new CancellationTokenSource();
        cts.CancelAfter(5);

        await Assert.ThrowsAsync<OperationCanceledException>(async () => await reader.ReadAsync(length, cts.Token));
    }


    [Theory]
    [InlineData((short)0)]
    [InlineData((short)3443)]
    [InlineData((short)-5181)]
    [InlineData(short.MinValue)]
    [InlineData(short.MaxValue)]
    public async void Test_ReadInt16(short value)
    {
        using var reader = CreateReader(writer => writer.Write(value));

        Assert.Equal(value, await reader.ReadInt16Async());
    }

    [Theory]
    [InlineData((short)0)]
    [InlineData((ushort)3443)]
    [InlineData(ushort.MaxValue)]
    public async void Test_ReadUInt16(ushort value)
    {
        using var reader = CreateReader(writer => writer.Write(value));

        Assert.Equal(value, await reader.ReadUInt16Async());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(321651)]
    [InlineData(-165816)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public async void Test_ReadInt(int value)
    {
        using var reader = CreateReader(writer => writer.Write(value));

        Assert.Equal(value, await reader.ReadInt32Async());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(23425)]
    [InlineData(uint.MaxValue)]
    public async void Test_ReadUInt(uint value)
    {
        using var reader = CreateReader(writer => writer.Write(value));

        Assert.Equal(value, await reader.ReadUInt32Async());
    }

    private static AsyncBinaryReader CreateReader(Action<BinaryWriter> filler, bool leaveOpen = false)
    {
        var ms = new MemoryStream();

        using (var writer = new BinaryWriter(ms, Encoding.Default, true))
        {
            filler?.Invoke(writer);
            writer.Flush();
        }

        ms.Seek(0, SeekOrigin.Begin);

        var reader = new AsyncBinaryReader(ms, leaveOpen);

        return reader;
    }
}
