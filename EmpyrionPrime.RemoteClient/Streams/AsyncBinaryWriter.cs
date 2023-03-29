using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace EmpyrionPrime.RemoteClient.Streams
{
    internal class AsyncBinaryWriter : IDisposable
    {
        private readonly Stream _stream;
        private readonly byte[] _buffer = new byte[16];
        private readonly bool _leaveOpen;

        private int _disposeCount = 0;

        public AsyncBinaryWriter(Stream stream, bool leaveOpen)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _leaveOpen = leaveOpen;

            if (!_stream.CanWrite)
                throw new ArgumentException("Stream not writable", nameof(stream));
        }

        public void Dispose()
        {
            if (Interlocked.Increment(ref _disposeCount) != 1)
                return;

            _stream.Flush();

            if (!_leaveOpen)
                _stream.Dispose();
        }

        public Task FlushAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            return _stream.FlushAsync(cancellationToken);
        }

        public Task WriteAsync(byte[] value, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            return _stream.WriteAsync(value, 0, value.Length, cancellationToken);
        }

        public Task WriteAsync(byte[] value, int offset, int length, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            return _stream.WriteAsync(value, offset, length, cancellationToken);
        }

        public Task WriteAsync(short value, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            var buffer = _buffer.AsSpan(0, sizeof(short));
            BinaryPrimitives.WriteInt16LittleEndian(buffer, value);

            return _stream.WriteAsync(_buffer, 0, sizeof(short), cancellationToken);
        }

        public Task WriteAsync(ushort value, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            var buffer = _buffer.AsSpan(0, sizeof(ushort));
            BinaryPrimitives.WriteUInt16LittleEndian(buffer, value);

            return _stream.WriteAsync(_buffer, 0, sizeof(ushort), cancellationToken);
        }

        public Task WriteAsync(int value, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            var buffer = _buffer.AsSpan(0, sizeof(int));
            BinaryPrimitives.WriteInt32LittleEndian(buffer, value);

            return _stream.WriteAsync(_buffer, 0, sizeof(int), cancellationToken);
        }

        public Task WriteAsync(uint value, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            var buffer = _buffer.AsSpan(0, sizeof(uint));
            BinaryPrimitives.WriteUInt32LittleEndian(buffer, value);

            return _stream.WriteAsync(_buffer, 0, sizeof(uint), cancellationToken);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfDisposed()
        {
            if (_disposeCount > 0) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
