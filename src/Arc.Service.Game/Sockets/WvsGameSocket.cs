using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Arc.Core.Services;
using Arc.Database.Models.Characters;
using Arc.Network;
using Arc.Network.Channels;
using Arc.Network.Packets;
using Serilog;

namespace Arc.Service.Game.Sockets
{
    public partial class WvsGameSocket : AbstractSocket
    {
        private readonly ILogger _logger;
        public GameNode GameNode { get; }

        public Character Character { get; set; }

        private Dictionary<int, int> _shuffledOpCodes = new Dictionary<int, int>();


        public WvsGameSocket(
            IChannel channel,
            uint seqSend,
            uint seqRecv,
            GameNode gameNode,
            ILogger logger
        ) : base(channel, seqSend, seqRecv)
        {
            GameNode = gameNode;
            _logger = logger;
        }

        public override Task OnPacket(IPacket inPacket)
        {

            var encryptedOpCode = inPacket.Decode<short>();
            InboundPacketOperation operation;
            if (encryptedOpCode > (int)InboundPacketOperation.BEGIN_USER)
            {
                operation = (InboundPacketOperation)_shuffledOpCodes[encryptedOpCode];
            }
            else
            {
                operation = (InboundPacketOperation)encryptedOpCode;
            }

            if (Enum.IsDefined(typeof(InboundPacketOperation), operation))
            {

                _logger.Information($"[IN] [{operation} | {(int)operation}] {(inPacket.Length <= 2 ? "" : BitConverter.ToString(inPacket.Buffer, 2, inPacket.Length).Replace("-", " "))}");
            }

            switch (operation)
            {
                case InboundPacketOperation.MigrateInRequest:
                    return OnMigrateInRequest(inPacket);
                case InboundPacketOperation.PrivateServerPacket:
                    return OnPrivateServerPacket(inPacket);
                default:
                    _logger.Warning($"[IN] [Unhandled packet operation [{operation}]] {(inPacket.Length <= 2 ? "" : BitConverter.ToString(inPacket.Buffer, 2, inPacket.Length).Replace("-", " "))}");
                    return Task.CompletedTask;
            }
        }

        public override async Task OnDisconnect()
        {
            await Task.CompletedTask;
        }



        public Task SendPacketToClient(IPacket outPacket)
        {
            _logger.Information($"[OUT] [{(OutboundPacketOperation)outPacket.GetHeader()} | {outPacket.GetHeader()}] {(outPacket.Length <= 2 ? "" : BitConverter.ToString(outPacket.Buffer, 2, outPacket.Length - 2).Replace("-", " "))}");
            return SendPacket(outPacket);
        }

        public override Task OnException(Exception exception)
        {
            _logger.Error(exception, "Caught exception in socket handling");
            return Task.CompletedTask;
        }
    }
}