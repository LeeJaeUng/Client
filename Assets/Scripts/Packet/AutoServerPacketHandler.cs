using Google.Protobuf;
using ServerCore;
using System;
using System.Collections.Generic;

public abstract class AutoServerPacketHandler
{
	enum MsgId
    {
		PKT_S_ENTER_GAME = 1000,
		PKT_S_LEAVE_GAME = 1001,
		PKT_S_SPAWN_PLAYER = 1002,
		PKT_S_DESPAWN = 1003,
		PKT_S_MOVE = 1004,
		PKT_C_MOVE = 1005,
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
	public abstract void Handle_S_SPAWN_PLAYER(PacketSession session, IMessage packet);
	public abstract void Handle_S_DESPAWN(PacketSession session, IMessage packet);
	public abstract void Handle_S_MOVE(PacketSession session, IMessage packet);
		
	public void Register()
	{
		_onRecv.Add((ushort)MsgId.PKT_S_ENTER_GAME, MakePacket<Protocol.S_ENTER_GAME>);
		_onRecv.Add((ushort)MsgId.PKT_S_LEAVE_GAME, MakePacket<Protocol.S_LEAVE_GAME>);
		_onRecv.Add((ushort)MsgId.PKT_S_SPAWN_PLAYER, MakePacket<Protocol.S_SPAWN_PLAYER>);
		_onRecv.Add((ushort)MsgId.PKT_S_DESPAWN, MakePacket<Protocol.S_DESPAWN>);
		_onRecv.Add((ushort)MsgId.PKT_S_MOVE, MakePacket<Protocol.S_MOVE>);
		_handler.Add((ushort)MsgId.PKT_S_ENTER_GAME, Handle_S_ENTER_GAME);
		_handler.Add((ushort)MsgId.PKT_S_LEAVE_GAME, Handle_S_LEAVE_GAME);
		_handler.Add((ushort)MsgId.PKT_S_SPAWN_PLAYER, Handle_S_SPAWN_PLAYER);
		_handler.Add((ushort)MsgId.PKT_S_DESPAWN, Handle_S_DESPAWN);
		_handler.Add((ushort)MsgId.PKT_S_MOVE, Handle_S_MOVE);
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