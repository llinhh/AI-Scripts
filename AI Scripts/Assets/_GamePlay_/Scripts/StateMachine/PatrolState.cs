using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Bot BotToControl;
    private float patrolTimer;
    private float patrolDuration = Random.Range(5f, 7f);
    private float patrolToAttackDelay = Random.Range(1.5f, 2f);


    public void Enter(Bot bot)
    {
        this.BotToControl = bot;
    }

    public void Execute()
    {
        // find target around
        BotToControl.FindAround();

        // Moving around
        this.BotToControl.MovingAction();

        // Checking target around

        if(BotToControl.NearestCharacter != null)
        {
            patrolTimer += Time.deltaTime;
            if(patrolTimer >= patrolToAttackDelay)
            {
                BotToControl.CancelDestination();

                BotToControl.ChangeState(new IdleState());
            }
        }

        // Change State from Patrol to Idle
        patrolTimer += Time.deltaTime;

        if(patrolTimer >= patrolDuration)
        {
            BotToControl.ChangeState(new IdleState());
        }

    }

    public void Exit()
    {

    }

    
}
