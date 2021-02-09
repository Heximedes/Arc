namespace Arc.Core.Services
{
    public enum OutboundPacketOperation : ushort
    {
        BEGIN_LOGIN = 0,
        CheckPasswordResult = 0,
        WorldInformation = 1,
        LatestConnectedWorld = 2,
        RecommendWorldMessage = 3,
        SelectWorldButton = 5,
        SelectWorldResult = 6,
        SelectCharacterResult = 7,
        AccountInfoResult = 8,
        CheckDuplicatedIDResult = 10,
        CreateNewCharacterResult = 11,
        DeleteCharacterResult = 12,
        END_LOGIN = 12,

        BEGIN_SOCKET = 16,
        MigrateCommand = 16,
        AliveReq = 17,
        PrivateServerPacket = 25,
        ApplyHotFix = 42,
        InitializeOpCodeEncryption = 43,
        END_SOCKET = 27,

        BEGIN_MAP = 75,
        SetBackgroundEffect = 75,
        SetMapTaggedObjectVisible = 76,
        

        BEGIN_CHARACTERDATA = 95,

        InstanceTableResult = 213,
        
        END_CHARACTERDATA = 536,


        SetField = 541,

        END_STAGE = 541,




        BEGIN_FIELD = 543,
        
            BEGIN_USERPOOL = 666,
            UserEnterField = 666,
            UserLeaveField = 667,

                BEGIN_USERCOMMON,

                    BEGIN_PET,
        
                    END_PET,

                    BEGIN_DRAGON,

                    END_DRAGON,

                END_USERCOMMON,

                BEGIN_USERREMOTE,
        
                END_USERREMOTE,

                BEGIN_USERLOCAL,

                END_USERLOCAL,

            END_USERPOOL,

            BEGIN_SUMMONED,

            END_SUMMONED,

            BEGIN_MOBPOOL,

                BEGIN_MOB,

                END_MOB,

            END_MOBPOOL,

            BEGIN_NPCPOOL,

                BEGIN_NPC,
        
                END_NPC,

                BEGIN_NPCTEMPLATE,
        
                END_NPCTEMPLATE,

            END_NPCPOOL,

            BEGIN_EMPLOYEEPOOL,

            END_EMPLOYEEPOOL,

            BEGIN_DROPPOOL,

            END_DROPPOOL,

        END_FIELD,

        BEGIN_CASHSHOP,

        END_CASHSHOP,

        BEGIN_FUNCKEYMAPPED,

        END_FUNCKEYMAPPED,


        BEGIN_GOLDHAMMER,

        END_GOLDHAMMER,


        BEGIN_ITEMUPGRADE,

        END_ITEMUPGRADE,

        LogoutGift,

        NO
    }
}