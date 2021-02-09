using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arc.Core.Gameplay.Constants;
using Arc.Core.Services;
using Arc.Core.Types;
using Arc.Database;
using Arc.Database.Models;
using Arc.Database.Models.Characters;
using Arc.Database.Models.Characters.Extensions;
using Arc.Database.Models.Inventories;
using Arc.Database.Models.Inventories.Items;
using Arc.Game.Characters.Encodings;
using Arc.Network.Packets;
using Microsoft.EntityFrameworkCore;
using Arc.Core.Gameplay.Types;
using DateTime = Arc.Network.Packets.Extensions.DateTime;

namespace Arc.Service.Login.Sockets
{
    public partial class LoginClientSocket
    {
        private async Task OnPermissionRequest(IPacket inPacket)
        {
            byte locale = inPacket.Decode<byte>();
            short version = inPacket.Decode<short>();
            string minorVersion = inPacket.Decode<string>(3);

            if (version != LoginNode.NodeInfo.Version)
            {
                _logger.Information($"Client {GetIP()} has an incorrect verion");
                await Close();

            }
        }
        private async Task SelectCharacterResult(IPacket inPacket)
        {
            int Id = inPacket.Decode<int>();
            using (var outPacket = new Packet(OutboundPacketOperation.SelectCharacterResult))
            {
                outPacket.Encode<byte>(0);
                outPacket.Encode<byte>(0);

                outPacket.Encode<byte[]>(new byte[] { 54, 203, 83, 148 });
                outPacket.Encode<short>(8585);

                outPacket.Encode<int>(Id);
                outPacket.Encode<string>("Scania");
                outPacket.Encode<string>("Scania");

                outPacket.Encode<byte[]>(new byte[69]);

                await SendPacketToClient(outPacket);
            }
        }
        private async Task OnWorldInfoRequest(IPacket packet)
        {
            using (var p = new Packet(OutboundPacketOperation.SetMapTaggedObjectVisible))
            {
                p.Encode<byte>(0);
                p.Encode<string>(LoginNode.NodeInfo.SplashScreen);
                p.Encode<byte>(1);
                p.Encode<int>(0);
                p.Encode<int>(0);
                await SendPacketToClient(p);
            }
            await Task.WhenAll(LoginNode.NodeInfo.Worlds.Select(world =>
            {

                using (var p = new Packet(OutboundPacketOperation.WorldInformation))
                {
                    p.Encode<byte>(world.ID);
                    p.Encode<string>(world.Name);
                    p.Encode<int>(0);
                    p.Encode<int>(0);
                    p.Encode<byte>(0);
                    p.Encode<byte>(0);
                    p.Encode<string>("");
                    p.Encode<bool>(world.BlockCharCreation);


                    // channels
                    p.Encode<byte>(4);

                    for (int i = 0; i < 4; i++)
                    {
                        p.Encode<string>($"Scania-{i}");
                        p.Encode<int>(25); // User Capacity
                        p.Encode<byte>(world.ID);
                        p.Encode<byte>((byte)i);
                        p.Encode<bool>(false);
                    }

                    p.Encode<short>(0);

                    p.Encode<int>(0);
                    p.Encode<short>(0);

                    return SendPacketToClient(p);
                }
            }));


            using (var outPacket = new Packet(OutboundPacketOperation.WorldInformation))
            {
                outPacket.Encode<byte>(0xFF);
                outPacket.Encode<short>(0);
                outPacket.Encode<int>(-1);

                await SendPacketToClient(outPacket);
            }

            using (var p = new Packet(OutboundPacketOperation.LatestConnectedWorld))
            {
                p.Encode<int>(LoginNode.NodeInfo.Worlds.FirstOrDefault()?.ID ?? 0);

                await SendPacketToClient(p);
            }
        }

        private async Task OnSelectWorldButton(IPacket inPacket)
        {
            byte unk = inPacket.Decode<byte>();
            int worldId = inPacket.Decode<int>();

            using (var outPacket = new Packet(OutboundPacketOperation.SelectWorldButton))
            {
                outPacket.Encode<byte>(unk);
                outPacket.Encode<int>(worldId);
                outPacket.Encode<int>(0);

                await SendPacketToClient(outPacket);
            }
        }

        private async Task OnSelectWorld(IPacket inPacket)
        {
            inPacket.Decode<byte>();
            byte worldId = inPacket.Decode<byte>();
            byte channel = (byte)(inPacket.Decode<byte>() + 1);
            inPacket.Decode<byte>();
            string token = inPacket.Decode<string>();
            byte[] machineID = inPacket.Decode<byte[]>(16);
            inPacket.Decode<int>();
            inPacket.Decode<byte>();
            inPacket.Decode<byte>();
            inPacket.Decode<short>();
            inPacket.Decode<byte>();
            string processerInfo = inPacket.Decode<string>();
            string osInfo = inPacket.Decode<string>();

            inPacket.Decode<int>();// ip

            //String accountName = ApiFactory.getFactory().getAccountByToken(c, token);
            //Account account = Account.getFromDBByName(accountName);
            string accountName = token;
            using (var context = new DatabaseContext())
            {
                Account = context.Accounts
                    .Include(acc => acc.Users.Where(user => user.WorldID == worldId))
                    .SingleOrDefault(acc => acc.Username == accountName);


                if (Account == null)
                {
                    Account = new Account
                    {
                        Username = accountName,
                        Password = accountName,
                        Pic = "111111",
                        CreationDate = System.DateTime.UtcNow


                    };
                    Account.Users.Add(new User
                    {
                        WorldID = worldId
                    });
                    context.Accounts.Add(Account);
                    context.SaveChanges();
                };
            }

            User = Account.Users
                .Where(user => user.WorldID == worldId)
                .FirstOrDefault();

            if (User == null)
            {
                using (var context = new DatabaseContext())
                {
                    User = new User
                    {
                        WorldID = worldId
                    };
                    Account.Users.Add(User);
                    context.Update(Account);
                    context.SaveChanges();

                }
            }

            using (var outPacket = new Packet(OutboundPacketOperation.AccountInfoResult))
            {
                outPacket.Encode<byte>(0); // succeed
                outPacket.Encode<int>(0);
                outPacket.Encode<byte>(0);
                outPacket.Encode<int>(0);
                outPacket.Encode<int>(0);
                outPacket.Encode<byte>(0);
                outPacket.Encode<string>(Account.Username);
                outPacket.Encode<byte>(0);
                outPacket.Encode<byte>(0);
                outPacket.Encode<System.DateTime>(DateTime.MaxTime);
                outPacket.Encode<string>("");
                outPacket.Encode<long>(0);
                outPacket.Encode<int>(0);
                outPacket.Encode<long>(0);
                outPacket.Encode<string>(""); //v25 = CInPacket::DecodeStr(iPacket_1, &nAge);
                                              //JobConstants.encode(outPacket, account.isManagerAccount());

                JobConstants.EncodeLoginJobs(outPacket, LoginNode.NodeInfo.JobOrder);
                outPacket.Encode<byte>(0);
                outPacket.Encode<short>(1);
                outPacket.Encode<byte>(1);
                outPacket.Encode<byte>(1);
                outPacket.Encode<short>(1);
                outPacket.Encode<int>(-1);
                //outPacket.encodeArr("00 01 00 01 01 01 00 FF FF FF FF");
                await SendPacketToClient(outPacket);
            }



            using (var outPacket = new Packet(OutboundPacketOperation.SelectWorldResult))
            {

                List<Character> characters;
                outPacket.Encode<byte>(0);
                outPacket.Encode<byte>(0);
                outPacket.Encode<string>(User.WorldID == 45 ? "reboot" : "normal");
                outPacket.Encode<string>(User.WorldID == 45 ? "reboot" : "normal");

                outPacket.Encode<int>(14); // Trunk Slots
                outPacket.Encode<byte>(1); // bBurningEventBlock
                outPacket.Encode<byte>(0); //UNK, new in 216
                int reserved = 0;
                outPacket.Encode<int>(reserved); // Reserved size
                outPacket.Encode<System.DateTime>(DateTime.ZeroTime); //Reserved timestamp

                bool isEdited = false;
                outPacket.Encode<bool>(isEdited); // edited characters



                using (var context = new DatabaseContext())
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    // the code that you want to measure comes here

                    User = context.Users
                        .Include(user => user.Characters)
                        .ThenInclude(chr => chr.AvatarLooks)
                        .Single(user => user.ID == User.ID);


                    characters = User.Characters.ToList();



                    watch.Stop();

                    //outPacket.Encode<byte>((byte)characters.Count);
                    //outPacket.Encode<byte>(0);
                    _logger.Information($"Character count :{characters.Count}");
                    var elapsedMs = watch.ElapsedMilliseconds;





                    _logger.Information($"Loading characters took : {elapsedMs}");
                }
                outPacket.Encode<int>(characters.Count);
                foreach (var chr in characters)
                {
                    outPacket.Encode<int>(chr.ID);
                }



                outPacket.Encode<byte>((byte)characters.Count);
                characters.ForEach(chr =>
                {
                    chr.EncodeAvatarData(outPacket, User.WorldID);
                    outPacket.Encode<bool>(false);
                });


                //EncodeStats(outPacket);
                //outPacket.Encode<int>(0);
                //outPacket.Encode<int>(0);
                //outPacket.Encode<long>(0);
                //EncodeLook(outPacket);
                /*
                for (Char chr : chars)
                {
                    chr.getAvatarData().encode(outPacket);
                    outPacket.encodeByte(false); // family stuff, deprecated (v61 = &v2->m_abOnFamily.a[v59];)
                    /*boolean hasRanking = chr.getRanking() != null && !JobConstants.isGmJob(chr.getJob());
                    outPacket.encodeByte(hasRanking);
                    if (hasRanking) {
                        chr.getRanking().encode(outPacket);
                    }
                }
            */
                outPacket.Encode<byte>(1); // pic status
                outPacket.Encode<bool>(false); // bQuerySSNOnCreateNewCharacter
                outPacket.Encode<int>(User.CharacterSlots);

                outPacket.Encode<int>(0); // buying char slots
                outPacket.Encode<int>(-1); // nEventNewCharJob
                outPacket.Encode<System.DateTime>(DateTime.ZeroTime);
                outPacket.Encode<byte>(0); // nRenameCount
                outPacket.Encode<byte>(0);
                outPacket.Encode<byte>(0);
                outPacket.Encode<byte>(0);
                outPacket.Encode<int>(0);
                outPacket.Encode<int>(0);
                outPacket.Encode<int>(0);
                outPacket.Encode<int>(0);
                await SendPacketToClient(outPacket);
            }
        }

        private async Task CharacterSelectNoPic(IPacket inPacket)
        {
            await Task.CompletedTask;
        }
        private async Task OnCurrentHotFix(IPacket packet)
        {
            using (var p = new Packet(OutboundPacketOperation.ApplyHotFix))
            {
                p.Encode<byte>(0);

                await SendPacketToClient(p);
            }
        }

        private async Task OnCreateNewCharacter(IPacket inPacket)
        {
            using (var outPacket = new Packet(OutboundPacketOperation.CreateNewCharacterResult))
            {
                var name = inPacket.Decode<string>();
                var keyLayoutType = inPacket.Decode<int>();
                var eventNewCharSaleJob = inPacket.Decode<int>();
                var loginJob = inPacket.Decode<int>();
                Job job = Jobs.GetJobByLoginJob(loginJob);
                var selectedSubJob = inPacket.Decode<short>();
                var gender = inPacket.Decode<byte>();
                var skin = inPacket.Decode<byte>();
                var baseAvatarDataLength = inPacket.Decode<byte>();
                int[] baseAvatarData = new int[baseAvatarDataLength];
                for (int i = 0; i < baseAvatarDataLength; i++)
                {
                    baseAvatarData[i] = inPacket.Decode<int>();
                };
                foreach (int item in baseAvatarData)
                {
                    _logger.Information($"id: {item}");
                }
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();

                    Character chr = new Character(baseAvatarData)
                    {
                        Name = name,
                        Job = Jobs.GetJobIDFromJob(job),
                        SubJob = selectedSubJob
                    };
                    chr.AvatarLooks.Add(new AvatarLook
                    {
                        Face = baseAvatarData[0],
                        Hair = baseAvatarData[1],
                        Skin = skin,
                        Gender = (Gender) gender,
                        EarType = Ear.VerdantFlora // GetEarTypeByRace
                    });

                    chr.AvatarLooks.Add(chr.AvatarLook());

                    ItemEquip familiarManger = new ItemEquip
                    {
                        TemplateID = 1172000,
                        BagIndex = (short)BodyPart.FamiliarManager,

                    };
                    chr.Equips.Add(familiarManger);
                    var user = context.Users
                        .Include(user => user.Characters)
                        .Single(user => user.ID == User.ID);

                    user.Characters.Add(chr);
                    //context.Characters.Add(chr);
                    context.Users.Update(user);
                    context.SaveChanges();
                    outPacket.Encode<byte>(0); // LoginType
                    chr.EncodeAvatarData(outPacket, 0);
                    outPacket.Encode<byte>(0);
                }
                await SendPacketToClient(outPacket);
            }
        }
        private async Task OnCheckDuplicateID(IPacket inPacket)
        {
            string name = inPacket.Decode<string>();

            // Check for duplicate id

            using (var outPacket = new Packet(OutboundPacketOperation.CheckDuplicatedIDResult))
            {
                outPacket.Encode<string>(name);
                outPacket.Encode<byte>(0);

                await SendPacketToClient(outPacket);
            }
        }

        private async Task OnExceptionLog(IPacket inPacket)
        {
            string exception = inPacket.Decode<string>();

            _logger.Warning(exception);
            await Task.CompletedTask;
        }

        private async Task OnPrivateServerPacket(IPacket inPacket)
        {
            using (var outPacket = new Packet(OutboundPacketOperation.PrivateServerPacket))
            {
                outPacket.Encode<int>((int)OutboundPacketOperation.PrivateServerPacket ^ inPacket.Decode<int>());
                await SendPacketToClient(outPacket);
            }
        }

        private async Task OnClientError(IPacket inPacket)
        {
            if (inPacket.Buffer.Length < 8)
            {
                _logger.Error($"Error: {inPacket}");
                return;
            }
            short type = inPacket.Decode<short>();
            string type_str = "Unknown?!";
            if (type == 0x01)
            {
                type_str = "SendBackupPacket";
            }
            else if (type == 0x02)
            {
                type_str = "Crash Report";
            }
            else if (type == 0x03)
            {
                type_str = "Exception";
            }
            int errortype = inPacket.Decode<int>();
            short data_length = inPacket.Decode<short>();

            int idk = inPacket.Decode<int>();

            short op = inPacket.Decode<short>();


            _logger.Error($"[Error {errortype}] [{(OutboundPacketOperation)op} | {op}] Data: {BitConverter.ToString(inPacket.Buffer).Replace("-", " ")}");
            await Task.CompletedTask;
        }


    }
}