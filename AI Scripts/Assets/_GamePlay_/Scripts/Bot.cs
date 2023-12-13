using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    public Transform tf_bot;
    public List<Transform> wayPoints = new List<Transform>();
    int CurrentWayPointIndex;
    public NavMeshAgent agent;
    public Collider Collider;
    public Rigidbody rb;
    private IEnemyState currentState;
    public GameObject NearestCharacter;
    public bool checkFirstAttack;
    public float Range;
    public float timeStart = 3f;
    public float timeCountdownt = 0;
    public bool isMoving;
    public GameObject WeaponInHand;
    public GameObject WeaponToThrow;
    void Start()
    {
        CurrentWayPointIndex = Random.Range(Random.Range(0, 10), wayPoints.Count);
        NearestCharacter = null;
        BotManager.Ins.GetRandomWeapon(this);
        ChangeState(new IdleState());
    }

    void Update()
    {
        FindAround();
        currentState.Execute();
    }

    public void FireAction()
    {
        Debug.Log("Spawn Weapon");

        if(isMoving || NearestCharacter == null)
        {
            return;
        }

        if(WeaponToThrow.name == "WeaponCandy")
        {

        }

    }
   
    public void FindAround()
    {
        float shortestDistance = Mathf.Infinity;

        GameObject target = null;

        for(int i = 0; i < BotManager.Ins.ListBotOnMap.Count; i++)
        {
            if(this != BotManager.Ins.ListBotOnMap[i])
            {
                float distanceToOtherCharacter = Vector3.Distance(this.gameObject.transform.position, BotManager.Ins.ListBotOnMap[i].tf_bot.position);
                if (distanceToOtherCharacter < shortestDistance)
                {
                    shortestDistance = distanceToOtherCharacter;

                    target = BotManager.Ins.ListBotOnMap[i].gameObject;
                }
            }
        }
        NearestCharacter = target;
    }

    public void MovingAction()
    {
        Transform wp = wayPoints[CurrentWayPointIndex];

        if (Vector3.Distance(agent.transform.position, wp.position) < 0.01f)
        {
            agent.transform.position = wp.position;

            CurrentWayPointIndex = Random.Range(Random.Range(0, 10), wayPoints.Count);
        }
        else
        {
            agent.SetDestination(wp.position);

            agent.transform.LookAt(wp.position);
        }
    }

    public void CancelDestination()
    {
        CurrentWayPointIndex = Random.Range(Random.Range(0, 10), wayPoints.Count);

        agent.SetDestination(agent.transform.position);
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
