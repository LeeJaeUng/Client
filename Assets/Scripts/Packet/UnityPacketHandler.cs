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
        Managers.Object.Add(pkt.ObjectInfo, true);
    }

    public override void Handle_S_LEAVE_GAME(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_LEAVE_GAME;
        Managers.Object.Clear();
    }
    public override void Handle_S_SPAWN(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_SPAWN;

        foreach(var obj in pkt.Objects)
        {
            Managers.Object.Add(obj, myPlayer: false);
        }


    }
    public override void Handle_S_DESPAWN(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_DESPAWN;

        foreach (var id in pkt.ObjectIDs)
        {
            Managers.Object.Remove(id);
        }
    }



    public override void Handle_S_MOVE(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_MOVE;

        var go = Managers.Object.FindById(pkt.ObjectID);

        if (go == null)
        {
            return;
        }

        var cc = go.GetComponent<BaseController>();
        if(cc== null)
        {
            return;
        }

        cc.PosInfo = pkt.PositionInfo;

    }

    public override void Handle_S_SKILL(PacketSession session, IMessage packet)
    {
        var pkt = packet as S_SKILL;

        var go = Managers.Object.FindById(pkt.ObjectID);

        if (go == null)
        {
            return;
        }
        var cc = go.GetComponent<CreatureController>();
        
        if (cc == null)
        {
            return;
        }

        cc.UseSkill(pkt.Info.SkillID);

    }

    public override void Handle_S_CHANGE_HP(PacketSession session, IMessage packet)
    {
        
        var pkt = packet as S_CHANGE_HP;

        var go = Managers.Object.FindById(pkt.ObjectID);

        if (go == null)
        {
            return;
        }
        var cc = go.GetComponent<CreatureController>();

        if (cc == null)
        {
            return;
        }

        cc.Hp = pkt.Hp;


    }

    public override void Handle_S_DIE(PacketSession session, IMessage packet)
    {
        
    }



    #endregion
}
