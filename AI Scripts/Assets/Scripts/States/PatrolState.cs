using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PatrolState : CharacterState
{
    private float patrolTimer;

    private float patrolDuration = Random.Range(3f, 4f);

    private float patrolToAttackDelay = Random.Range(1.5f, 2f);

    private const string AnimIdleTag = "IsIdle";

    private CharacterController _char;

    public void Enter(CharacterController character)
    {
        this._char = character;
    }

    public void Execute()
    {
        _char.FindAround();

        Patrol();

        CheckingTarget();

        PatrolToIdle();
    }

    public void Exit()
    {

    }

    private void Patrol()
    {
        //_char.MyAnimator.SetBool(AnimIdleTag, false);

        _char.Move();
    }

    public void CheckingTarget()
    {
        if (_char.nearestCharacter != null)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolToAttackDelay)
            {
                _char.CancelDestination();

                //_char.MyAnimator.SetBool(AnimIdleTag, true);

                _char.ChangeState(new IdleState());
            }
        }
    }

    public void PatrolToIdle()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            _char.ChangeState(new IdleState());
        }
    }
}
