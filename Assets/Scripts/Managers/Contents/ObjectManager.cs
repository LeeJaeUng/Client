using Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectManager
{
	public MyPlayerController MyPlayer { get; set; }
	Dictionary<uint, GameObject> _objects = new Dictionary<uint, GameObject>();

	public void Add(PlayerInfo info, bool myPlayer = false)
	{
		if (myPlayer == true)
		{
			var go =Managers.Resource.Instantiate("Creature/MyPlayer");
			go.name = info.AccountData.NickName;
			Add(info.AccountData.AccountUID, go);

			MyPlayer = go.GetComponent<MyPlayerController>();
			MyPlayer.id = info.AccountData.AccountUID;
			MyPlayer.PosInfo = info.PositionInfo;

        }
		else
		{ 
			var go = Managers.Resource.Instantiate("Creature/Player");
			go.name = info.AccountData.NickName;
			Add(info.AccountData.AccountUID, go);

			var pc = go.GetComponent<PlayerController>();
			pc.id = info.AccountData.AccountUID;
            MyPlayer.PosInfo = info.PositionInfo;
        }
	}

	private void Add(uint id, GameObject go)
	{
		_objects.Add( id, go);
	}

	public void Remove(uint id)
	{
		_objects.Remove(id);
	}

	public void RemoveMyPlayer()
	{
		if (MyPlayer == null)
		{
			return;
		}

		Remove(MyPlayer.id);
		MyPlayer = null;
	}

	public GameObject FindById(uint id)
	{
		_objects.TryGetValue((uint)id, out var go);
		return go;
	}

	public GameObject Find(Vector3 cellPos)
	{
		foreach (GameObject obj in _objects.Values)
		{
			CreatureController cc = obj.GetComponent<CreatureController>();
			if (cc == null)
				continue;

			if (cc.CellPos == cellPos)
				return obj;
		}

		return null;
	}

	public GameObject Find(Func<GameObject, bool> condition)
	{
		foreach (GameObject obj in _objects.Values)
		{
			if (condition.Invoke(obj))
				return obj;
		}

		return null;
	}

	public void Clear()
	{
		_objects.Clear();
	}
}
