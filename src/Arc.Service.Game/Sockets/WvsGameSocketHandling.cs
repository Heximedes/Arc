using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arc.Core.Services;
using Arc.Core.Types;
using Arc.Database;
using Arc.Database.Models.Characters;
using Arc.Game.Characters.Encodings;
using Arc.Network.Crypto;
using Arc.Network.Packets;
using Microsoft.EntityFrameworkCore;

namespace Arc.Service.Game.Sockets
{
    public partial class WvsGameSocket
    {
        private async Task OnPrivateServerPacket(IPacket inPacket)
        {
            using (var outPacket = new Packet(OutboundPacketOperation.PrivateServerPacket))
            {
                outPacket.Encode<int>((int)OutboundPacketOperation.PrivateServerPacket ^ inPacket.Decode<int>());
                await SendPacketToClient(outPacket);
            }
        }

        private async Task OnMigrateInRequest(IPacket inPacket)
        {
            int worldID = inPacket.Decode<int>();
            int charID = inPacket.Decode<int>();

            Character character;

            using (var context = new DatabaseContext())
            {
                character = context.Characters
                    .Include(chr => chr.AvatarLooks)
                    .Include(chr => chr.Equips)
                    .Single(chr => chr.ID == charID);
            }

            using (var outPacket = new Packet(OutboundPacketOperation.InitializeOpCodeEncryption))
            {
                Random random = new Random();
                string opCodeString = "";
                List<int> usedOpCodes = new List<int>();
                for (int opCode = (int)InboundPacketOperation.BEGIN_USER; opCode < (int)InboundPacketOperation.NO; opCode++)
                {
                    int randomOpCode = random.Next((int)InboundPacketOperation.BEGIN_USER, 9999);
                    while (usedOpCodes.Contains(randomOpCode))
                    {
                        randomOpCode = random.Next((int)InboundPacketOperation.BEGIN_USER, 9999);
                    }
                    string randomOpCodeString = $"{randomOpCode:0000}";
                    if (!usedOpCodes.Contains(randomOpCode))
                    {
                        _shuffledOpCodes.Add(randomOpCode, opCode);
                        usedOpCodes.Add(randomOpCode);
                        opCodeString += randomOpCodeString;
                    }
                }

                byte[] finalEncryptedBuffer = new byte[short.MaxValue + 1];
                byte[] encryptedBuffer = TripleDes.Encrypt(opCodeString, GameNode.NodeInfo.DKey);

                Array.Copy(encryptedBuffer, finalEncryptedBuffer, encryptedBuffer.Length);


                for (int i = encryptedBuffer.Length; i < finalEncryptedBuffer.Length; i++)
                {
                    finalEncryptedBuffer[i] = (byte)random.Next();
                }

                outPacket.Encode<int>(4);
                outPacket.Encode<int>(finalEncryptedBuffer.Length);
                outPacket.Encode<byte[]>(finalEncryptedBuffer);

                await SendPacketToClient(outPacket);
            }

            using (var outPacket = new Packet(OutboundPacketOperation.SetField))
            {
                outPacket.Encode<int>(0); // Damn nexon, randomly switching between starting at 1 and 0...
                outPacket.Encode<byte>(0);
                outPacket.Encode<int>(0);

                outPacket.Encode<byte>(1);
                outPacket.Encode<int>(0); // unused
                outPacket.Encode<byte>(0);
                outPacket.Encode<int>(800);
                outPacket.Encode<int>(600);
                outPacket.Encode<bool>(true);
                short notifierCheck = 0;
                outPacket.Encode<short>(notifierCheck);
                if (notifierCheck > 0)
                {
                    outPacket.Encode<string>(""); // pBlockReasonIter
                    for (int i = 0; i < notifierCheck; i++)
                    {
                        outPacket.Encode<string>(""); // sMsg2
                    }
                }

                if (true)
                {
                    Random random = new Random();
                    int s1 = random.Next();
                    int s2 = random.Next();
                    int s3 = random.Next();
                    outPacket.Encode<int>(s1);
                    outPacket.Encode<int>(s2);
                    outPacket.Encode<int>(s3);

                    //chr.setDamageCalc(new DamageCalc(chr, s1, s2, s3));
                    character.EncodeData(outPacket, CharacterFields.All | CharacterFields.StolenSkills); // <<<<------------------------------------
                                                                                                         //String packet = "";
                                                                                                         //outPacket.encodeArr(Util.getByteArrayByString(packet));
                                                                                                         // unk sub (not in kmst)
                                                                                                         // logout event (mushy)
                                                                                                         //encodeLogoutEvent(outPacket);

                    bool someFlag = false;
                    //outPacket.Encode<bool>(someFlag);
                    if (someFlag)
                    {
                        bool anotherFlag = false;
                        outPacket.Encode<bool>(anotherFlag);
                        if (anotherFlag)
                        {
                            outPacket.Encode<int>(0); //????
                        }
                        outPacket.Encode<long>(0);
                        outPacket.Encode<byte>(0);
                        outPacket.Encode<long>(0);
                    }

                }
                else
                {
                    outPacket.Encode<bool>(false);
                    outPacket.Encode<int>(character.CurrentField);
                    outPacket.Encode<byte>(character.CurrentPortal);
                    outPacket.Encode<int>(character.HP);
                    bool blool = false;
                    outPacket.Encode<bool>(blool);
                    if (blool)
                    {
                        outPacket.Encode<int>(0);
                        outPacket.Encode<int>(0);
                    }
                }

                // 41 bytes below
                outPacket.Encode<bool>(false);
                //outPacket.Encode<byte>(0); // unsure
                outPacket.Encode<System.DateTime>(System.DateTime.UtcNow);
                outPacket.Encode<int>(100);
                bool hasFieldCustom = false;
                outPacket.Encode<bool>(hasFieldCustom);
                if (hasFieldCustom)
                {
                    //fieldCustom.encode(outPacket);
                }
                outPacket.Encode<bool>(false); // is pvp map, deprecated
                outPacket.Encode<byte>(0);

                outPacket.Encode<byte>(0);
                /*
                if (stackEventGauge >= 0)
                {
                    outPacket.Encode<int>(stackEventGauge);
                }*/
                // sub_16A52D0
                //outPacket.Encode<byte>(0); // Star planet, not interesting
                //outPacket.Encode<byte>(0); // more star planet
                // CUser::DecodeTextEquipInfo
                int size = 0;
                outPacket.Encode<int>(size);
                for (int i = 0; i < size; i++)
                {
                    outPacket.Encode<int>(0);
                    outPacket.Encode<string>("");
                }
                // FreezeAndHotEventInfo::Decode
                outPacket.Encode<byte>(0); // nAccountType
                outPacket.Encode<int>(1);
                // CUser::DecodeEventBestFriendInfo
                outPacket.Encode<int>(0); // dwEventBestFriendAID

                // unks
                bool read = true;
                outPacket.Encode<bool>(read);
                if (read)
                {
                    outPacket.Encode<int>(-1);
                    outPacket.Encode<int>(0);
                    outPacket.Encode<int>(0);
                    outPacket.Encode<int>(999999999);
                    outPacket.Encode<int>(999999999);
                    outPacket.Encode<string>("");
                }
                bool showEventUI = false;
                outPacket.Encode<bool>(showEventUI);
                if (showEventUI)
                {
                    //outPacket.Encode<string>(ServerConfig.LOGIN_NOTICE_POPUP); TODO [HEX]: figure out the format
                    /**
                     * UI/UIWindowEvent.img/sundayMaple
                     * #sunday# #fnArial##fs20##fc0xFFFFD800#50% off #fc0xFFFFFFFF#ability resets!
                     * #fnArial##fs15##fc0xFFB7EC00#Sunday, 10/11/2020
                     * 50
                     * 232
                     */
                }

                outPacket.Encode<int>(0);

                outPacket.Encode<byte>(0);// v202.3

                // sub_16A4D10
                outPacket.Encode<int>(0); // ?
                                          // sub_16D99C0

                // sub_962150
                outPacket.Encode<int>(0);
                outPacket.Encode<int>(0);
                outPacket.Encode<string>("");

                await SendPacketToClient(outPacket);
            }
        }
    }
}
