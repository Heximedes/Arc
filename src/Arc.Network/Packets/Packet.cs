using System;
using DotNetty.Buffers;

namespace Arc.Network.Packets
{
    public class Packet : IPacket
    {
        private readonly IByteBuffer _byteBuffer;
        public byte[] Buffer => _byteBuffer.Array;
        public int Length => _byteBuffer.ReadableBytes;
        public short Header => ((short)(Buffer[0] + (Buffer[1] << 8)));

        public Packet(IByteBuffer byteBuffer)
            => _byteBuffer = byteBuffer;

        public Packet(Enum operation, int initialCapacity = byte.MaxValue)
            : this(Unpooled.Buffer(initialCapacity))
            => Encode<short>(Convert.ToInt16(operation));

        public Packet(int initialCapacity = byte.MaxValue)
            : this(Unpooled.Buffer(initialCapacity))
        {
        }

        public int GetHeader() // TODO: do i need this?
            => Length < 2 ? 0xFFFF : (Buffer[0] + (Buffer[1] << 8));

        public IPacket Encode<T>(T value)
        {
            var type = typeof(T);
            if (value == null) throw new NotSupportedException();
            if (PacketMethods.EncodeMethods.ContainsKey(type))
                PacketMethods.EncodeMethods[type](_byteBuffer, value);
            return this;
        }

        public IPacket Encode<T>(T value, int length)  
        {
            var type = typeof(T);
            if (value == null) throw new NotSupportedException();
            if (PacketMethods.EncodeLengthMethods.ContainsKey(type))
                PacketMethods.EncodeLengthMethods[type](_byteBuffer, length, value);
            return this;
        }

        public T Decode<T>()
        {
            var type = typeof(T);
            if (PacketMethods.DecodeMethods.ContainsKey(type))
                return (T) PacketMethods.DecodeMethods[type](_byteBuffer);
            throw new NotSupportedException();
        }
        public T Decode<T>(int length)
        {
            var type = typeof(T);
            if (PacketMethods.DecodeLengthMethods.ContainsKey(type))
                return (T)PacketMethods.DecodeLengthMethods[type](_byteBuffer, length);
            throw new NotSupportedException();
        }

        public void Dispose()
            => _byteBuffer.Release();




    }
}