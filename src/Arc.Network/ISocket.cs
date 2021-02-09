using System;
using System.Threading.Tasks;
using Arc.Network.Packets;

namespace Arc.Network
{
    public interface ISocket
    {
        uint SeqSend { get; set; }
        uint SeqRecv { get; set; }
        bool EncryptData { get; }

        public string GetIP();
        public int GetPort();

        Task OnPacket(IPacket packet);
        Task OnDisconnect();
        Task OnException(Exception exception);

        Task SendPacket(IPacket packet);
        Task Close();
    }
}