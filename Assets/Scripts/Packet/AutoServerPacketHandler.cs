using Google.Protobuf;
using ServerCore;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoServerPacketHandler : MonoBehaviour
{
	enum MsgId
    {
		PKT_C_LOGIN = 1000,
		PKT_S_LOGIN = 1001,
		PKT_C_ENTER_GAME = 1002,
		PKT_S_ENTER_GAME = 1003,
		PKT_C_CHAT = 1004,
		PKT_S_CHAT = 1005,
	}

	public AutoServerPacketHandler()
	{
		Register();
	}

	Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
	Dictionary<ushort, Action<PacketSession, IMessage>> _handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();
	public abstract void Handle_C_LOGIN(PacketSession session, IMessage packet);
	public abstract void Handle_C_ENTER_GAME(PacketSession session, IMessage packet);
	public abstract void Handle_C_CHAT(PacketSession session, IMessage packet);
		
	public void Register()
	{
		_onRecv.Add((ushort)MsgId.PKT_S_LOGIN, MakePacket<Protocol.S_LOGIN>);
		_onRecv.Add((ushort)MsgId.PKT_S_ENTER_GAME, MakePacket<Protocol.S_ENTER_GAME>);
		_onRecv.Add((ushort)MsgId.PKT_S_CHAT, MakePacket<Protocol.S_CHAT>);
		_handler.Add((ushort)MsgId.PKT_C_LOGIN, Handle_C_LOGIN);
		_handler.Add((ushort)MsgId.PKT_C_ENTER_GAME, Handle_C_ENTER_GAME);
		_handler.Add((ushort)MsgId.PKT_C_CHAT, Handle_C_CHAT);
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
		if (_handler.TryGetValue(id, out var action))
			action.Invoke(session, pkt);
	}

	public Action<PacketSession, IMessage> GetPacketHandler(ushort id)
	{
		if (_handler.TryGetValue(id, out var action))
			return action;
		return null;
	}
}