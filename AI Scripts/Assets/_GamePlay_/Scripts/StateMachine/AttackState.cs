using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    private Bot BotToControl;
    private Vector3 BotPosition, NearestCharacterPosition;
    private Transform NearestCharacterTf;
    private float delayTime;

    public void Enter(Bot bot)
    {
        // Set info for bot and set nearest position
        this.BotToControl = bot;
        BotPosition = BotToControl.tf_bot.position;
        NearestCharacterPosition = BotToControl.NearestCharacter.transform.position;
        NearestCharacterTf = BotToControl.NearestCharacter.transform;

        // Checking Attack

        if (Vector3.Distance(BotPosition, NearestCharacterPosition) < this.BotToControl.Range && this.BotToControl.timeCountdownt <= 0 && this.BotToControl.checkFirstAttack && this.BotToControl.isMoving == false)
        {
            this.BotToControl.timeCountdownt = this.BotToControl.timeStart;
            this.BotToControl.checkFirstAttack = false;
        }

       
        //Attack
        if(this.BotToControl.NearestCharacter != null)
        {
            this.BotToControl.transform.LookAt(NearestCharacterTf);
        }
        this.BotToControl.FireAction();

    }

    public void Execute()
    {
        // Attack To Patrol
        delayTime += Time.deltaTime;
        if(delayTime >= Random.Range(1f, 1.5f))
        {
            this.BotToControl.ChangeState(new PatrolState());
        }

        // Attack To Idle
        if(this.BotToControl.NearestCharacter == null)
        {
            this.BotToControl.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    
}
