using System;

namespace Arc.Network.Packets
{
    public interface IPacket : IDisposable
    {
        byte[] Buffer { get; }
        int Length { get; }

        int GetHeader();

        IPacket Encode<T>(T value);
        IPacket Encode<T>(T value, int length);
        T Decode<T>();
        T Decode<T>(int length);
    }
}