using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol;
using static Define;
using Google.Protobuf;

public class MyPlayerController : PlayerController
{
    bool _moveKeyPressed = false;
    protected override void Init()
    {
        base.Init();
    }

    protected override void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Idle:
                GetDirInput();
                break;
            case CreatureState.Moving:
                GetDirInput();
                break;
        }

        base.UpdateController();
    }
    protected override void UpdateIdle()
    {
        // 이동 상태로 갈지 확인
        if (_moveKeyPressed)
        {
            State = CreatureState.Moving;
            return;
        }

        // 스킬 상태로 갈지 확인
        if (Input.GetKey(KeyCode.Space) && _coSkillCoolTime == null)
        {
            Debug.Log("Skill !!");

            C_SKILL skill = new C_SKILL()
            {
                Info = new SkillInfo()
                {
                    SkillID = 2
                }
            };

            Managers.Network.Send(skill);

            _coSkillCoolTime = StartCoroutine(CoInputCoolTime(0.2f));

        }
    }
    Coroutine _coSkillCoolTime;
    IEnumerator CoInputCoolTime(float time)
    { 
        yield return new WaitForSeconds(time);
        _coSkillCoolTime = null;
    }

    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    // 키보드 입력
    void GetDirInput()
    {

        _moveKeyPressed = true;

        if (Input.GetKey(KeyCode.W))
        {
            Dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = MoveDir.Right;
        }
        else
        {
            _moveKeyPressed = false;
        }
    }

    protected override void MoveToNextPos()
    {
        if (!_moveKeyPressed)
        {
            State = CreatureState.Idle;
            CheckUpdatedFlag();
            return;
        }

        Vector3Int destPos = CellPos;

        switch (Dir)
        {
            case MoveDir.Up:
                destPos += Vector3Int.up;
                break;
            case MoveDir.Down:
                destPos += Vector3Int.down;
                break;
            case MoveDir.Left:
                destPos += Vector3Int.left;
                break;
            case MoveDir.Right:
                destPos += Vector3Int.right;
                break;
        }

        if (Managers.Map.CanGo(destPos))
        {
            if (Managers.Object.FindCreature(destPos) == null)
            {
                CellPos = destPos;
            }
        }

        CheckUpdatedFlag();
    }

    protected override void CheckUpdatedFlag()
    {
        if (_updated)
        {
            C_MOVE movePacket = new C_MOVE();
            movePacket.PositionInfo = PosInfo;
            Managers.Network.Send(movePacket);
            _updated = false;
        }
    }
}
