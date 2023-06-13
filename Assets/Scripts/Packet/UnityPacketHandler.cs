using Google.Protobuf;
using Protocol;
using ServerCore;

public class UnityPacketHandler : AutoServerPacketHandler
{
    #region Singleton
    public static UnityPacketHandler _instance = new UnityPacketHandler();
    public static UnityPacketHandler Instance { get { return _instance; } }
    #endregion
    public override void Handle_C_CHAT(PacketSession session, IMessage packet)
    {
        C_CHAT chatPacket = packet as C_CHAT;
        print(chatPacket.Msg);
    }

    public override void Handle_C_ENTER_GAME(PacketSession session, IMessage packet)
    {
    }

    public override void Handle_C_LOGIN(PacketSession session, IMessage packet)
    {
    }
   
}
