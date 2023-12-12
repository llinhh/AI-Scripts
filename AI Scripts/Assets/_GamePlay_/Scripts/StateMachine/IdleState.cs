using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Bot BotToControl;

    private float idleTimer;

    private float idleDuration = Random.Range(2f, 4f);

    private float idleToAttackDelay = Random.Range(1.7f, 2.5f);
    public void Enter(Bot bot)
    {
        this.BotToControl = bot;

        this.BotToControl.CancelDestination();
    }

    public void Execute()
    {
        this.BotToControl.FindAround();

        // Lookat Nearest Character
        if(BotToControl.NearestCharacter != null)
        {
            BotToControl.tf_bot.LookAt(BotToControl.NearestCharacter.transform);
        }

        // Idle To Attack
        if(BotToControl.NearestCharacter != null)
        {
            idleTimer += Time.deltaTime;

            if(idleTimer >= idleToAttackDelay)
            {
                BotToControl.checkFirstAttack = true;

                BotToControl.ChangeState(new AttackState());
            }
        }

        // Idle To Patrol
        idleTimer += Time.deltaTime;
        if(idleTimer >= idleDuration)
        {
            BotToControl.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {

    }
}
