using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using System.Linq;

public class CharacterController : CharacterManager
{
    private CharacterState currentState;

    public List<Transform> wayPoints = new List<Transform>();

    internal NavMeshAgent agent;

    internal Rigidbody rb;

    internal CharacterController scripts;

    internal Collider _collider;

    int currentWayPointIndex;

    public float timeStart = 3f;

    public float timeCountdownt = 0;

    public NameCharacter name;
    public override void Awake()
    {
        base.Awake();

        name = (NameCharacter)Random.Range(0, 10);

        Name.text = "" + name;
    }

    public override void Start()
    {
        characterTransform = this.transform;

        base.Start();

        checkFirstAttack = true;

        agent = GetComponent<NavMeshAgent>();

        _collider = GetComponent<Collider>();

        rb = GetComponent<Rigidbody>();

        scripts = GetComponent<CharacterController>();

        currentWayPointIndex = Random.Range(Random.Range(0, 10), wayPoints.Count);

        //ChangeState(new IdleSM());
    }

    public override void Update()
    {
        if (GameManager.Instance.isGameActive == true)
        {
            currentState.Execute();
        }

        deadFunction();

        timeCountdownt -= Time.deltaTime;

        timeCountdownt = Mathf.Clamp(timeCountdownt, 0, Mathf.Infinity);

        base.Update();

        if (timeCountdownt <= 0)
        {
            ShowWeapon();
        }

        if (isDead == true)
        {
            GameManager.Instance.TotalAlive -= 1;

            //GUIManager.Instance.EnemyCountNumber.text = GameManager.Instance.TotalAlive.ToString();
        }

        /*if (nearestCharacter != null)
        {
            TargetFoot.SetActive(true);
        }
        else
        {
            TargetFoot.SetActive(false);
        }*/
    }

    public void Fire()
    {
        if (nearestCharacter == null)
        {
            //MyAnimator.ResetTrigger(AnimAttackTag);
            return;
        }

        GameObject getWeapon = null;

        HideWeapon();

        if (weapon.name == HammerBulletName)
        {
            getWeapon = WeaponManager.Instance.GetWeapon();
        }
        else if (weapon.name == CandyBulletName)
        {
            getWeapon = WeaponManager.Instance.GetWeapon();
        }
        else if (weapon.name == KnifeBulletName)
        {
            getWeapon = WeaponManager.Instance.GetWeapon();
        }

        WeaponManager InfoBulletAfterPool = getWeapon.GetComponent<WeaponManager>();

        Transform bulletTransForm = getWeapon.transform;

        Transform nearestTransform = nearestCharacter.transform;

        bulletTransForm.position = bulletSpawnPoint.position;

        bulletTransForm.rotation = bulletTransForm.rotation;

        getWeapon.SetActive(true);

        InfoBulletAfterPool.SetTargetPosition(nearestTransform.position);

        InfoBulletAfterPool.SetOwnerChar(this);

        InfoBulletAfterPool.SetOwnerPos(characterTransform.position);

    }

    public IEnumerator Attack()
    {
        //MyAnimator.SetTrigger(AnimAttackTag);

        yield return new WaitForSeconds(0.4f);

        Fire();
    }

    public void deadFunction()
    {
        if (isDead == true)
        {
            _collider.enabled = false;

            rb.detectCollisions = false;

            scripts.enabled = false;

            agent.enabled = false;

            GameManager.Instance._listCharacter.Remove(this);
        }
    }
    public void Move()
    {
        Transform wp = wayPoints[currentWayPointIndex];

        if (Vector3.Distance(agent.transform.position, wp.position) < 0.01f)
        {
            //MyAnimator.SetBool(AnimIdleTag, true);

            agent.transform.position = wp.position;

            currentWayPointIndex = Random.Range(Random.Range(0, 10), wayPoints.Count);
        }
        else
        {
            //MyAnimator.SetBool(AnimIdleTag, false);

            agent.SetDestination(wp.position);

            agent.transform.LookAt(wp.position);
        }
    }

    public void CancelDestination()
    {
        currentWayPointIndex = Random.Range(Random.Range(0, 10), wayPoints.Count);

        agent.SetDestination(agent.transform.position);
    }

    public void ChangeState(CharacterState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }
}
