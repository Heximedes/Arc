using DotNetty.Transport.Channels;

namespace Arc.Network
{
    public interface ISocketFactory
    {
        ISocket Build(IChannel channel, uint seqSend, uint seqRecv);
    }
}