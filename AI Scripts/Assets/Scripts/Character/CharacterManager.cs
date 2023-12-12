using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public enum NameCharacter { Nemo, Carrot, Barbie, Coco, Uni, Crytal, David, Tucson, Niel, Tonado };
public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public const string CharacterTag = "Character";

    /*[HideInInspector] public const string AnimIdleTag = "IsIdle";

    [HideInInspector] public const string AnimAttackTag = "IsAttack";

    [HideInInspector] public const string AnimDeadTag = "IsDead";

    [HideInInspector] public const string AnimDanceTag = "IsDance";*/

    [HideInInspector] public const string OnDespawnTag = "OnDespawn";

    [HideInInspector] public const string showWeaponTag = "showWeapon";

    [HideInInspector] public const string weaponTag = "Weapon";

    [HideInInspector] public const string HammerBulletName = "Hammer Bullet";

    [HideInInspector] public const string CandyBulletName = "Candy Bullet";

    [HideInInspector] public const string KnifeBulletName = "Knife Bullet";

    /*[HideInInspector] public const string PlayerTag = "Player";

    [HideInInspector] public const string EnemyTag = "Enemy";*/

    public bool isDead;

    public bool checkFirstAttack;

    public bool isMoving;

    public float range;

    public float timer;

    public GameObject handWeapon;

    [HideInInspector] public GameObject nearestCharacter;

    public GameObject footTarget;

    public Transform bulletSpawnPoint;

    public TextMeshProUGUI Name;

    [SerializeField] int heal;

    public ParticleSystem effectOnDead;

    //public TextMeshProUGUI ScoreText;

    //public int Score = 0;

    public GameObject weapon;

    public Vector3 bulletPos;

    internal Transform characterTransform;

    //public Animator MyAnimator { get; private set; }

    public int Damage;

    //[HideInInspector] public Animator AnimName;

    public virtual void Awake()
    {
        characterTransform = this.transform;

        //AnimName = Name.GetComponent<Animator>();
    }
    public virtual void Start()
    {
        //MyAnimator = GetComponent<Animator>();

        GameManager.Instance.AddCharacter(this);

        nearestCharacter = null;

        Damage = 10;
    }

    public virtual void Update()
    {
        FindAround();

        if (this.gameObject.activeInHierarchy == false)
        {
            isDead = true;

            GameManager.Instance._listCharacter.Remove(this);
        }
    }
    public void FindAround()
    {
        float shortestDistance = Mathf.Infinity;

        GameObject target = null;

        for (int i = 0; i < GameManager.Instance._listCharacter.Count; i++)
        {
            if (this != GameManager.Instance._listCharacter[i])
            {
                float distanceToOtherCharacter = Vector3.Distance(this.gameObject.transform.position, GameManager.Instance._listCharacter[i].transform.position);

                if (distanceToOtherCharacter < shortestDistance)
                {
                    shortestDistance = distanceToOtherCharacter;

                    target = GameManager.Instance._listCharacter[i].gameObject;
                }
            }
        }
        nearestCharacter = target;

        if (target != null && shortestDistance < range * target.transform.localScale.z)
        {
            nearestCharacter = target;
            footTarget.gameObject.SetActive(true);
        }
        else
        {
            footTarget.gameObject.SetActive(false);
            nearestCharacter = null;
        }
    }

    public void OnDead()
    {
        if (isDead == true)
        {
            ParticleSystem effectDead = Instantiate(effectOnDead, transform.position, Quaternion.identity);

            Destroy(effectDead.gameObject, 1f);

            Invoke(OnDespawnTag, 1.2f);

            //MyAnimator.SetBool(AnimDeadTag, true);

            GameManager.Instance._listCharacter.Remove(this);
        }
    }

    public void ShowWeapon()
    {
        handWeapon.SetActive(true);
    }

    public void HideWeapon()
    {
        //yield return new WaitForSeconds(0.42f);

        handWeapon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        WeaponController weaponScript = other.gameObject.GetComponent<WeaponController>();

        Transform weaponOfOwnerTransForm = weaponScript.characterOwner.transform;

        if (other.gameObject.CompareTag(weaponTag))
        {
            if (this != weaponScript.characterOwner)
            {
                OnHit(Damage);

                other.gameObject.SetActive(false);

                //GameObject BGKillFeed = Instantiate(GUIManager.Instance.KillFeed, GUIManager.Instance.SpawnKillFeedPos);

                //TextMeshProUGUI EnemyTextOfKillfeed = BGKillFeed.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

                //TextMeshProUGUI PlayerTextOfKillfeed = BGKillFeed.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                //PlayerTextOfKillfeed.text = bulletWeaponScript.characterOwner.Name.text;

                //EnemyTextOfKillfeed.text = this.Name.text;

                //Destroy(BGKillFeed, 2);
            }

            else if (this.name != weaponScript.characterOwner.name)
            {
                //bulletWeaponScript.characterOwner.Score++;

                //bulletWeaponScript.characterOwner.ScoreText.text = bulletWeaponScript.characterOwner.Score.ToString();

                weaponOfOwnerTransForm.localScale += new Vector3(0.1f, 0.1f, 0.1f);

                weaponScript.characterOwner.Damage += 1;

                if (weaponScript.characterOwner.Damage >= 15)
                {
                    weaponScript.characterOwner.Damage = 15;
                }

                weaponScript.characterOwner.range += 0.025f;

                /*if (bulletWeaponScript.characterOwner.tag == PlayerTag)
                {
                    GameManager.Instance.cameraOnMenu.m_Lens.FieldOfView += 2;

                    if (GameManager.Instance.cameraOnMenu.m_Lens.FieldOfView >= 70)
                    {
                        GameManager.Instance.cameraOnMenu.m_Lens.FieldOfView = 70;
                    }
                }*/

                if (weaponScript.characterOwner.range >= 0.4f)
                {
                    weaponScript.characterOwner.range = 0.4f;
                }

                if (weaponOfOwnerTransForm.localScale.x >= 1.5)
                {
                    weaponOfOwnerTransForm.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    public void OnHit(int damage)
    {
        heal -= damage;

        if (heal <= 0)
        {
            heal = 0;

            isDead = true;
        }
        OnDead();
    }

    public void GetWeaponHand(GameObject WeaponInHand)
    {
        handWeapon = WeaponInHand;
    }
}

