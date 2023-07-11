using Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectManager
{
	public MyPlayerController MyPlayer { get; set; }
	Dictionary<uint, GameObject> _objects = new Dictionary<uint, GameObject>();
	public static GameObjectType GetObjectTypeById(uint id)
	{
        uint type = (id >> 24) & 0x7F;
		return (GameObjectType)type;
	}

	public void Add(ObjectInfo info, bool myPlayer = false)
	{
		GameObjectType objectType = GetObjectTypeById(info.ObjectID);
		if (objectType == GameObjectType.Player)
		{
			if (myPlayer)
			{
				GameObject go = Managers.Resource.Instantiate("Creature/MyPlayer");
				go.name = info.Name;
				_objects.Add(info.ObjectID, go);

				MyPlayer = go.GetComponent<MyPlayerController>();
				MyPlayer.Id = info.ObjectID;
				MyPlayer.PosInfo = info.PositionInfo;
				MyPlayer.Stat = info.StatInfo;
				MyPlayer.SyncPos();
			}
			else
			{
				GameObject go = Managers.Resource.Instantiate("Creature/Player");
				go.name = info.Name;
				_objects.Add(info.ObjectID, go);

				PlayerController pc = go.GetComponent<PlayerController>();
				pc.Id = info.ObjectID;
				pc.PosInfo = info.PositionInfo;
				pc.Stat = info.StatInfo;
				pc.SyncPos();
			}
		}
		else if (objectType == GameObjectType.Monster)
		{

		}
		else if (objectType == GameObjectType.Projectile)
		{
			GameObject go = Managers.Resource.Instantiate("Creature/Arrow");
			go.name = "Arrow";
			_objects.Add(info.ObjectID, go);

			ArrowController ac = go.GetComponent<ArrowController>();
			ac.PosInfo = info.PositionInfo;
			ac.Stat = info.StatInfo;
			ac.SyncPos();
		}
	}

	public void Remove(uint id)
	{
		GameObject go = FindById(id);
		if (go == null)
			return;

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
		_objects.TryGetValue(id, out var go);
		return go;
	}

	public GameObject Find(Vector3Int cellPos)
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
