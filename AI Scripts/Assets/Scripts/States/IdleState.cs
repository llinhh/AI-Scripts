using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CharacterState
{
    private CharacterController _char;

    private float idleTimer;

    private float idleDuration = Random.Range(2f, 4f);

    private float idleToAttackDelay = Random.Range(1.7f, 2.5f);

    private const string AnimIdleTag = "IsIdle";
    public void Enter(CharacterController character)
    {
        this._char = character;

        character.CancelDestination();
    }

    public void Execute()
    {
        _char.FindAround();

        Idle();

        IdleToAttack();

        IdleToPatrol();
    }

    public void Exit()
    {

    }

    private void Idle()
    {
        //_char.MyAnimator.SetBool(AnimIdleTag, true);

        if (_char.nearestCharacter != null)
        {
            _char.transform.LookAt(_char.nearestCharacter.transform);
        }

    }

    public void IdleToAttack()
    {
        if (_char.nearestCharacter != null)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleToAttackDelay)
            {
                _char.checkFirstAttack = true;

                _char.ChangeState(new AttackState());
            }
        }
    }

    public void IdleToPatrol()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            _char.ChangeState(new PatrolState());
        }
    }
}
