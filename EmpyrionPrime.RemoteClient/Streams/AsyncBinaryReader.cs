using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace EmpyrionPrime.RemoteClient.Streams
{
    internal class AsyncBinaryReader : IDisposable
    {
        private readonly Stream _stream;
        private readonly byte[] _buffer = new byte[16];
        private readonly bool _leaveOpen;

        private int _disposeCount = 0;

        public AsyncBinaryReader(Stream stream, bool leaveOpen)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _leaveOpen = leaveOpen;

            if (!_stream.CanRead)
                throw new ArgumentException("Stream not readable", nameof(stream));
        }

        public void Dispose()
        {
            if (Interlocked.Increment(ref _disposeCount) != 1)
                return;

            _stream.Flush();

            if (!_leaveOpen)
                _stream.Dispose();
        }

        public Task<byte[]> ReadAsync(int count, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            return InternalRead(new byte[count], count, cancellationToken);
        }

        public async Task<byte> ReadByteAsync(CancellationToken cancellationToken = default)
        {
            var bytes = await FillBuffer(1, cancellationToken);
            return bytes[0];
        }

        public async Task<short> ReadInt16Async(CancellationToken cancellationToken = default) 
            => BinaryPrimitives.ReadInt16LittleEndian(await FillBuffer(sizeof(short), cancellationToken));
        public async Task<ushort> ReadUInt16Async(CancellationToken cancellationToken = default) 
            => BinaryPrimitives.ReadUInt16LittleEndian(await FillBuffer(sizeof(ushort), cancellationToken));

        public async Task<int> ReadInt32Async(CancellationToken cancellationToken = default) 
            => BinaryPrimitives.ReadInt32LittleEndian(await FillBuffer(sizeof(int), cancellationToken));
        public async Task<uint> ReadUInt32Async(CancellationToken cancellationToken = default) 
            => BinaryPrimitives.ReadUInt32LittleEndian(await FillBuffer(sizeof(uint), cancellationToken));

        private Task<byte[]> FillBuffer(int count, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            return InternalRead(_buffer, count, cancellationToken);
        }

        private async Task<byte[]> InternalRead(byte[] buffer, int count, CancellationToken cancellationToken = default)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (count > buffer.Length) throw new ArgumentException("Count is larger than buffer size", nameof(buffer));

            var totalRead = 0;
            while (totalRead < count && !cancellationToken.IsCancellationRequested)
            {
                var read = await _stream.ReadAsync(buffer, totalRead, count - totalRead, cancellationToken);

                if (read == 0)
                    throw new EndOfStreamException();

                totalRead += read;
            }

            if (cancellationToken.IsCancellationRequested)
                cancellationToken.ThrowIfCancellationRequested();

            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfDisposed()
        {
            if (_disposeCount > 0) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
