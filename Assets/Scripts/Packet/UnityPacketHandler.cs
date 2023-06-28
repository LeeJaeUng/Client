using Google.Protobuf;
using Protocol;
using ServerCore;
using System;
using System.Threading;
using UnityEngine;

public class UnityPacketHandler : AutoServerPacketHandler
{
    
    #region Singleton
    public static UnityPacketHandler _instance = new UnityPacketHandler();
    public static UnityPacketHandler Instance { get { return _instance; } }

    public override void Handle_S_ENTER_GAME(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_ENTER_GAME;
        Managers.Object.Add(pkt.PlayerInfo, true);
    }

    public override void Handle_S_LEAVE_GAME(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_LEAVE_GAME;
        Managers.Object.RemoveMyPlayer();
    }
    public override void Handle_S_SPAWN_PLAYER(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_SPAWN_PLAYER;

        foreach(var player in pkt.PlayerInfos)
        {
            Managers.Object.Add(player, myPlayer: false);
        }


    }
    public override void Handle_S_DESPAWN(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_DESPAWN;

        foreach (var id in pkt.AccountUIDs)
        {
            Managers.Object.Remove(id);
        }
    }



    public override void Handle_S_MOVE(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_MOVE;

        var go = Managers.Object.FindById(pkt.AccountUID);

        if (go == null)
        {
            return;
        }

        var cc = go.GetComponent<CreatureController>();
        if(cc== null)
        {
            return;
        }

        cc.PosInfo = pkt.PositionInfo;

    }

    public override void Handle_S_SKILL(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_SKILL;

        var go = Managers.Object.FindById(pkt.AccountUID);

        if (go == null)
        {
            return;
        }
        var pc = go.GetComponent<PlayerController>();
        
        if (pc == null)
        {
            return;
        }

        pc.UseSkill(pkt.Info.SkillID);

    }



    #endregion
}
