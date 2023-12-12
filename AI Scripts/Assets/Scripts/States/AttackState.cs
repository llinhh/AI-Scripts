using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CharacterState
{
    private CharacterController _char;

    private float delayTime;

    Vector3 charPos, nearestCharPos;

    Transform nearestCharTransform;

    public void Enter(CharacterController character)
    {
        this._char = character;

        charPos = _char.transform.position;

        nearestCharPos = _char.nearestCharacter.transform.position;

        nearestCharTransform = _char.nearestCharacter.transform;

        CheckAttack();

        Attack();
    }

    public void Execute()
    {
        AttackToPatrol();

        AttackToIdle();
    }

    public void Exit()
    {

    }

    private void Attack()
    {
        if (_char.nearestCharacter != null)
        {
            _char.transform.LookAt(nearestCharTransform);
        }

        _char.StartCoroutine(_char.Attack());
    }

    public void CheckAttack()
    {
        if (Vector3.Distance(charPos, nearestCharPos) < _char.range && _char.timeCountdownt <= 0 && _char.checkFirstAttack && _char.isMoving == false)
        {
            _char.timeCountdownt = _char.timeStart;

            _char.checkFirstAttack = false;
        }
    }

    public void AttackToPatrol()
    {
        delayTime += Time.deltaTime;

        if (delayTime >= Random.Range(1f, 1.5f))
        {
            _char.ChangeState(new PatrolState());
        }

    }

    public void AttackToIdle()
    {
        if (_char.nearestCharacter == null)
        {
            _char.ChangeState(new IdleState());
        }
    }
}
