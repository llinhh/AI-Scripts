using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float timer;

    public float speed;

    public Vector3 positionTarget;

    public Vector3 BotOwnerPos;

    public Vector3 fixedDirectToCharacter;

    public Bot BotOwner;

    public Vector3 posSpawnBullet;

    private void Update()
    {
        UpdateState();

        autoDespawnIfOutOfRange();
    }

    public virtual void UpdateState()
    {

    }

    public void autoDespawnIfOutOfRange()
    {
        if (Vector3.Distance(BotOwnerPos, transform.position) > BotOwner.Range)
        {
            gameObject.SetActive(false);
        }
    }

    public void setOwnerPos(Vector3 botOwnerPos)
    {
        BotOwnerPos = botOwnerPos;

        fixedDirectToCharacter = (positionTarget - botOwnerPos).normalized;
    }

    public void setTargetPosition(Vector3 targetPos)
    {
        positionTarget = targetPos;
    }

    public void setOwnerChar(Bot botOwner)
    {
        BotOwner = botOwner;
    }
}
