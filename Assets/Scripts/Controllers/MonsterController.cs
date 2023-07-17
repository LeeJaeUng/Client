﻿using Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{
	Coroutine _coSkill;


	[SerializeField]
	bool _rangedSkill = false;


	protected override void Init()
	{
		base.Init();

		State = CreatureState.Idle;
		Dir = MoveDir.Down;

        _rangedSkill = (Random.Range(0, 2) == 0 ? true : false);

    }

	protected override void UpdateIdle()
	{
		base.UpdateIdle();
	}

	public override void OnDamaged()
	{
		//Managers.Object.Remove(Id);
		//Managers.Resource.Destroy(gameObject);
	}
	IEnumerator CoStartPunch()
	{
		// 피격 판정
		GameObject go = Managers.Object.FindCreature(GetFrontCellPos());
		if (go != null)
		{
			CreatureController cc = go.GetComponent<CreatureController>();
			if (cc != null)
				cc.OnDamaged();
		}

		// 대기 시간
		yield return new WaitForSeconds(0.5f);
		State = CreatureState.Moving;
		_coSkill = null;
	}

	IEnumerator CoStartShootArrow()
	{
		GameObject go = Managers.Resource.Instantiate("Creature/Arrow");
		ArrowController ac = go.GetComponent<ArrowController>();
		ac.Dir = Dir;
		ac.CellPos = CellPos;

		// 대기 시간
		yield return new WaitForSeconds(0.3f);
		State = CreatureState.Moving;
		_coSkill = null;
	}
}
