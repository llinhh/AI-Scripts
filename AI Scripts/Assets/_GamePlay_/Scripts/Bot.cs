using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject ammo;
    internal Transform botTransform;
    public Transform weaponSpawnPoint;
    public bool isDead;
    internal Collider collider;
    internal Bot script;
    [HideInInspector] public const string ammoTag = "Ammo";
    int heal;
    public int Damage;

    public void Awake()
    {
        botTransform = this.transform;
    }

    void Start()
    {
        CurrentWayPointIndex = Random.Range(Random.Range(0, 10), wayPoints.Count);
        NearestCharacter = null;
        //BotManager.Ins.GetRandomWeapon(this);
        ChangeState(new IdleState());

        Damage = 10;
    }

    void Update()
    {
        FindAround();
        currentState.Execute();
    }

    public void ShowWeapon()
    {
        WeaponInHand.SetActive(true);
    }

    public void FireAction()
    {
        Debug.Log("Spawn Weapon");

        if(isMoving || NearestCharacter == null)
        {
            return;
        }

        HideWeapon();

        if(WeaponToThrow.name == "WeaponCandy")
        {
            ammo = AmmoCandyThrow.Instance.GetAmmoThrow();
        }

        else if (WeaponToThrow.name == "WeaponLolipop")
        {
            ammo = AmmoLolipopThrow.Instance.GetAmmoThrow();
        }

        else if (WeaponToThrow.name == "WeaponCandyCane")
        {
            ammo = AmmoCandyCaneThrow.Instance.GetAmmoThrow();
        }

        Weapon ammoAfterSpawn = ammo.GetComponent<Weapon>();

        Transform ammoTransForm = ammoAfterSpawn.transform;

        Transform nearestTransform = NearestCharacter.transform;

        ammoTransForm.localScale = botTransform.localScale;

        ammoTransForm.position = weaponSpawnPoint.position;

        ammoTransForm.rotation = ammoTransForm.rotation;

        ammo.SetActive(true);

        ammoAfterSpawn.setTargetPosition(nearestTransform.position);

        ammoAfterSpawn.setOwnerChar(this);

        ammoAfterSpawn.setOwnerPos(botTransform.position);

    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.44f);

        FireAction();
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

    private void OnTriggerEnter(Collider other)
    {
        Candy CandyWeaponScript = other.gameObject.GetComponent<Candy>();

        Transform ammoOfOwnerTransForm = CandyWeaponScript.BotOwner.transform;

        if (other.gameObject.CompareTag(ammoTag))
        {
            if (this != CandyWeaponScript.BotOwner) // kiem tra neu thang nem vu khi khac chinh no thi thuc hien
            {
                OnHit(Damage);

                other.gameObject.SetActive(false);

                /*GameObject BGKillFeed = Instantiate(GUIManager.Instance.KillFeed, GUIManager.Instance.SpawnKillFeedPos);

                TextMeshProUGUI EnemyTextOfKillfeed = BGKillFeed.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

                TextMeshProUGUI PlayerTextOfKillfeed = BGKillFeed.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                PlayerTextOfKillfeed.text = CandyWeaponScript.characterOwner.Name.text;

                EnemyTextOfKillfeed.text = this.Name.text;

                Destroy(BGKillFeed, 2);*/
            }

            /*if (this.name != CandyWeaponScript.BotOwner.name)
            {
                CandyWeaponScript.BotOwner.Score++;

                CandyWeaponScript.BotOwner.ScoreText.text = CandyWeaponScript.BotOwner.Score.ToString();

                bulletOfOwnerTransForm.localScale += new Vector3(0.1f, 0.1f, 0.1f);

                CandyWeaponScript.BotOwner.Damage += 1;

                if (CandyWeaponScript.BotOwner.Damage >= 15)
                {
                    CandyWeaponScript.BotOwner.Damage = 15;
                }

                CandyWeaponScript.BotOwner.range += 0.025f;

                if (CandyWeaponScript.BotOwner.range >= 0.4f)
                {
                    CandyWeaponScript.BotOwner.range = 0.4f;
                }

                if (bulletOfOwnerTransForm.localScale.x >= 1.5)
                {
                    bulletOfOwnerTransForm.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
            }*/
        }
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

    public void HideWeapon()
    {
        WeaponInHand.SetActive(false);
    }

    public void OnHit(int damage)
    {
        heal -= damage;

        if (heal <= 0)
        {
            heal = 0;

            isDead = true;
        }
        Dead();
    }

    public void Dead()
    {
        if (isDead == true)
        {
            collider.enabled = false;

            rb.detectCollisions = false;

            script.enabled = false;

            agent.enabled = false;

            BotManager.Ins.ListBotOnMap.Remove(this);
        }
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
