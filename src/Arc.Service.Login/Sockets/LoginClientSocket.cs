using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Arc.Core.Services;
using Arc.Database.Models;
using Arc.Network;
using Arc.Network.Packets;
using Serilog;

namespace Arc.Service.Login.Sockets
{
    public partial class LoginClientSocket : AbstractSocket
    {
        private readonly ILogger _logger;
        public LoginNode LoginNode { get; }



        public IChannel Channel { get; set; }
        public Account Account { get; set; }
        public User User { get; set; }


        public LoginClientSocket(
            IChannel channel,
            uint seqSend,
            uint seqRecv,
            LoginNode loginNode,
            ILogger logger
        ) : base(channel, seqSend, seqRecv)
        {
            LoginNode = loginNode;
            Channel = channel;
            _logger = logger;
        }

        public override Task OnPacket(IPacket inPacket)
        {
            var operation = (InboundPacketOperation)inPacket.Decode<short>();
            if (Enum.IsDefined(typeof(InboundPacketOperation), operation))
            {

                _logger.Information($"[IN ] [{operation} | {(int)operation}] {(inPacket.Length <= 2 ? "" : BitConverter.ToString(inPacket.Buffer, 2, inPacket.Length).Replace("-", " "))}");
            }

            switch (operation)
            {
                case InboundPacketOperation.PermissionRequest:
                    return OnPermissionRequest(inPacket);
                case InboundPacketOperation.WorldInfoRequest:
                    return OnWorldInfoRequest(inPacket);
                case InboundPacketOperation.CurrentHotFix:
                    return OnCurrentHotFix(inPacket);
                case InboundPacketOperation.SelectWorldButton:
                    return OnSelectWorldButton(inPacket);
                case InboundPacketOperation.SelectWorld:
                    return OnSelectWorld(inPacket);
                case InboundPacketOperation.WvsSetupUp:
                case InboundPacketOperation.LoadingDataFromWzFiles:
                case InboundPacketOperation.FinishedLoadingDataFromWzFiles:
                    return Task.CompletedTask;
                case InboundPacketOperation.ExceptionLog:
                    return OnExceptionLog(inPacket);
                case InboundPacketOperation.PrivateServerPacket:
                    return OnPrivateServerPacket(inPacket);
                case InboundPacketOperation.ClientError:
                    return OnClientError(inPacket);
                case InboundPacketOperation.CheckDuplicateID:
                    return OnCheckDuplicateID(inPacket);
                case InboundPacketOperation.CreateNewCharacter:
                    return OnCreateNewCharacter(inPacket);
                case InboundPacketOperation.CharacterSelect:
                    return SelectCharacterResult(inPacket);
                case InboundPacketOperation.CharacterSelectNoPic:
                    return CharacterSelectNoPic(inPacket);
                default:
                    _logger.Warning($"[IN ] [Unhandled packet operation [{operation}]] {(inPacket.Length <= 2 ? "" : BitConverter.ToString(inPacket.Buffer, 2, inPacket.Length).Replace("-", " "))}");
                    return Task.CompletedTask;
            }
        }

        public override async Task OnDisconnect()
        {
            await Task.CompletedTask;
        }

        public Task SendPacketToClient(IPacket outPacket)
        {
            if (!Channel.Active) { return Task.CompletedTask; }
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