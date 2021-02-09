using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using DotNetty.Buffers;

namespace Arc.Network.Packets
{
    internal static class PacketMethods
    {
        internal static readonly Encoding StringEncoding = Encoding.ASCII;

        internal static readonly Dictionary<Type, Func<IByteBuffer, object>> DecodeMethods =
            new Dictionary<Type, Func<IByteBuffer, object>>
            {
                {typeof(byte), buffer => buffer.ReadByte()},
                {typeof(bool), buffer => buffer.ReadByte() > 0},
                {typeof(short), buffer => buffer.ReadShortLE()},
                {typeof(ushort), buffer => buffer.ReadUnsignedShortLE()},
                {typeof(int), buffer => buffer.ReadIntLE()},
                {typeof(uint), buffer => buffer.ReadUnsignedIntLE()},
                {typeof(long), buffer => buffer.ReadLongLE()},
                {typeof(string), buffer => buffer.ReadString(buffer.ReadShortLE(), StringEncoding)},
                {typeof(float), buffer => buffer.ReadFloatLE()},
                {typeof(double), buffer => buffer.ReadDoubleLE()},
                {typeof(DateTime), buffer => DateTime.FromFileTimeUtc(buffer.ReadLongLE())},
                {typeof(Point), buffer => new Point(buffer.ReadShortLE(), buffer.ReadShortLE())}
            };

        internal static readonly Dictionary<Type, Func<IByteBuffer, int, object>> DecodeLengthMethods =
            new Dictionary<Type, Func<IByteBuffer, int, object>>
            {
                {typeof(byte[]), (buffer, value) =>
                {
                    byte[] buf = new byte[value];
                    for (int i = 0; i < value; i++)
                    {
                        buf[i] = buffer.ReadByte();
                    }
                    return buf;
                }},
                {typeof(string), (buffer, value) => buffer.ReadString(value, StringEncoding)},
            };

        internal static readonly Dictionary<Type, Action<IByteBuffer, object>> EncodeMethods =
            new Dictionary<Type, Action<IByteBuffer, object>>
            {
                {typeof(byte), (buffer, value) => buffer.WriteByte((byte) value)},
                {typeof(bool), (buffer, value) => buffer.WriteByte((bool) value ? 1 : 0)},
                {typeof(short), (buffer, value) => buffer.WriteShortLE((short) value)},
                {typeof(int), (buffer, value) => buffer.WriteIntLE((int) value)},
                {typeof(long), (buffer, value) => buffer.WriteLongLE((long) value)},
                {
                    typeof(string), (buffer, value) =>
                    {
                        var str = (string) value ?? string.Empty;

                        buffer.WriteShortLE(str.Length);
                        buffer.WriteBytes(StringEncoding.GetBytes(str));
                    }
                },
                {typeof(float), (buffer, value) => buffer.WriteFloatLE((float) value)},
                {typeof(double), (buffer, value) => buffer.WriteDoubleLE((double) value)},
                {typeof(DateTime), (buffer, value) => buffer.WriteLongLE(((DateTime) value).ToFileTimeUtc())},
                {
                    typeof(byte[]), (buffer, value) =>
                    {
                        buffer.WriteBytes((byte[]) value);
                    }
                },
                {
                    typeof(Point), (buffer, value) =>
                    {
                        buffer.WriteShortLE(((Point) value).X);
                        buffer.WriteShortLE(((Point) value).Y);
                    }
                }
            };

        internal static readonly Dictionary<Type, Action<IByteBuffer, int, object>> EncodeLengthMethods =
            new Dictionary<Type, Action<IByteBuffer, int, object>>
            {
                {typeof(string),  (buffer, length, value) =>
                {
                    if (((string)value).Length > length)
                    {
                        value = ((string)value).Substring(0, length); 
                    }
                    buffer.WriteBytes(StringEncoding.GetBytes(((string)value).PadRight(length, '\0')));
                }
                }
            };
    }
}