using Google.Protobuf;
using Protocol;
using ServerCore;
using System;
using System.Threading;

public class UnityPacketHandler : AutoServerPacketHandler
{
    
    #region Singleton
    public static UnityPacketHandler _instance = new UnityPacketHandler();
    public static UnityPacketHandler Instance { get { return _instance; } }

    public override void Handle_S_DESPAWN(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_DESPAWN;
    }

    public override void Handle_S_ENTER_GAME(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_ENTER_GAME;

    }

    public override void Handle_S_LEAVE_GAME(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_LEAVE_GAME;
    }

    public override void Handle_S_MOVE(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_MOVE;
    }

    public override void Handle_S_SPAWN_PLAYER(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_SPAWN_PLAYER;
    }

    #endregion
}
