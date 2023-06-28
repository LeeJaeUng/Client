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
			_objects.Add(info.AccountData.AccountUID, go);

			MyPlayer = go.GetComponent<MyPlayerController>();
			MyPlayer.Id = info.AccountData.AccountUID;
			MyPlayer.PosInfo = info.PositionInfo;
			MyPlayer.SyncPos();

        }
		else
		{ 
			var go = Managers.Resource.Instantiate("Creature/Player");
			go.name = info.AccountData.NickName;
            _objects.Add(info.AccountData.AccountUID, go);

			var pc = go.GetComponent<PlayerController>();
			pc.Id = info.AccountData.AccountUID;
            pc.PosInfo = info.PositionInfo;
			pc.SyncPos();
        }
	}


	public void Remove(uint id)
	{
		var go = FindById(id);
		if(go == null)
		{
            return;
        }

		_objects.Remove(id);
		Managers.Resource.Destroy(go);
	}

	public void RemoveMyPlayer()
	{
		if (MyPlayer == null)
		{
			return;
		}

		Remove(MyPlayer.Id);
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
		foreach (GameObject obj in _objects.Values)
		{ 
			Managers.Resource.Destroy(obj);
		}
        _objects.Clear();
	}
}
