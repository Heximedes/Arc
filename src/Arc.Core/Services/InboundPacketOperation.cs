namespace Arc.Core.Services
{
    public enum InboundPacketOperation : ushort
    {
        BEGIN_SOCKET = 101,
        PermissionRequest = 103,
        AccountInfoRequest = 104,
        SelectWorldButton = 106,
        SelectWorld = 107,
        CheckPinRequest = 108,
        CharacterSelect = 109,
        MigrateInRequest = 112,
        WorldInfoRequest = 115,
        CheckDuplicateID = 116,
        CreateNewCharacter = 125,
        ExceptionLog = 132,
        PrivateServerPacket = 133,
        AliveAcknowledgement = 150,
        ClientError = 153,
        CurrentHotFix = 156,
        WvsSetupUp = 158,
        CharacterSelectNoPic = 165,
        FinishedLoadingDataFromWzFiles = 196,
        LoadingDataFromWzFiles = 197,
        END_SOCKET = 198,

        BEGIN_USER = 198,
        UserTransferFieldRequest = 199,
        UserChannelChannelRequest = 200,

        UserRequestInstanceTable = 383,
        
        NO = 2500
    }
}