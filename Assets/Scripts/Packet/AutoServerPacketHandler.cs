using Google.Protobuf;
using ServerCore;
using System;
using System.Collections.Generic;

public abstract class AutoServerPacketHandler
{
	public enum MsgId
    {
		PKT_S_ENTER_GAME = 1000,
		PKT_S_LEAVE_GAME = 1001,
		PKT_S_SPAWN = 1002,
		PKT_S_DESPAWN = 1003,
		PKT_S_MOVE = 1004,
		PKT_S_SKILL = 1005,
		PKT_S_CHANGE_HP = 1006,
		PKT_S_DIE = 1007,
		PKT_C_MOVE = 1008,
		PKT_C_SKILL = 1009,
	}

	public AutoServerPacketHandler()
	{
		Register();
	}

	public Action<PacketSession, IMessage, ushort> CustomHandler { get; set; }

	Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
	Dictionary<ushort, Action<PacketSession, IMessage>> _handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();
	public abstract void Handle_S_ENTER_GAME(PacketSession session, IMessage packet);
	public abstract void Handle_S_LEAVE_GAME(PacketSession session, IMessage packet);
	public abstract void Handle_S_SPAWN(PacketSession session, IMessage packet);
	public abstract void Handle_S_DESPAWN(PacketSession session, IMessage packet);
	public abstract void Handle_S_MOVE(PacketSession session, IMessage packet);
	public abstract void Handle_S_SKILL(PacketSession session, IMessage packet);
	public abstract void Handle_S_CHANGE_HP(PacketSession session, IMessage packet);
	public abstract void Handle_S_DIE(PacketSession session, IMessage packet);
		
	public void Register()
	{
		_onRecv.Add((ushort)MsgId.PKT_S_ENTER_GAME, MakePacket<Protocol.S_ENTER_GAME>);
		_onRecv.Add((ushort)MsgId.PKT_S_LEAVE_GAME, MakePacket<Protocol.S_LEAVE_GAME>);
		_onRecv.Add((ushort)MsgId.PKT_S_SPAWN, MakePacket<Protocol.S_SPAWN>);
		_onRecv.Add((ushort)MsgId.PKT_S_DESPAWN, MakePacket<Protocol.S_DESPAWN>);
		_onRecv.Add((ushort)MsgId.PKT_S_MOVE, MakePacket<Protocol.S_MOVE>);
		_onRecv.Add((ushort)MsgId.PKT_S_SKILL, MakePacket<Protocol.S_SKILL>);
		_onRecv.Add((ushort)MsgId.PKT_S_CHANGE_HP, MakePacket<Protocol.S_CHANGE_HP>);
		_onRecv.Add((ushort)MsgId.PKT_S_DIE, MakePacket<Protocol.S_DIE>);
		_handler.Add((ushort)MsgId.PKT_S_ENTER_GAME, Handle_S_ENTER_GAME);
		_handler.Add((ushort)MsgId.PKT_S_LEAVE_GAME, Handle_S_LEAVE_GAME);
		_handler.Add((ushort)MsgId.PKT_S_SPAWN, Handle_S_SPAWN);
		_handler.Add((ushort)MsgId.PKT_S_DESPAWN, Handle_S_DESPAWN);
		_handler.Add((ushort)MsgId.PKT_S_MOVE, Handle_S_MOVE);
		_handler.Add((ushort)MsgId.PKT_S_SKILL, Handle_S_SKILL);
		_handler.Add((ushort)MsgId.PKT_S_CHANGE_HP, Handle_S_CHANGE_HP);
		_handler.Add((ushort)MsgId.PKT_S_DIE, Handle_S_DIE);
	}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
	{
		ushort count = 0;

		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;

		if (_onRecv.TryGetValue(id, out var action))
			action.Invoke(session, buffer, id);
	}

	void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer, ushort id) where T : IMessage, new()
	{
		T pkt = new T();
		pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);
		CustomHandler.Invoke(session, pkt, id);
	}

	public Action<PacketSession, IMessage> GetPacketHandler(ushort id)
	{
		if (_handler.TryGetValue(id, out var action))
			return action;
		return null;
	}
}