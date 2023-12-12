using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private static WeaponManager instance;
    public static WeaponManager Instance { get { return instance; } }

    public GameObject weaponPrefab;

    public List<GameObject> weapons = new List<GameObject>();

    public int weaponAmount;

    public float timer;

    public float speed;

    public Vector3 positionTarget;

    public Vector3 charOwnerPos;

    public Vector3 fixedDirectToCharacter;

    public CharacterManager characterOwner;

    public Vector3 posSpawnWeapon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnWeapon();
    }

    private void Update()
    {

        UpdateState();

        AutoDespawnIfOutOfRange();
    }

    public void SpawnWeapon()
    {
        for (int i = 0; i < weaponAmount; i++)
        {
            GameObject weaponSpawn = Instantiate(weaponPrefab);

            weaponSpawn.SetActive(false);

            weaponSpawn.transform.SetParent(transform);

            weapons.Add(weaponSpawn);
        }
    }
    public GameObject GetWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (!weapons[i].activeInHierarchy)
            {
                return weapons[i];
            }
        }

        return null;
    }

    public virtual void UpdateState()
    {

    }

    public void AutoDespawnIfOutOfRange()
    {

        if (Vector3.Distance(charOwnerPos, transform.position) > characterOwner.range)
        {
            gameObject.SetActive(false);
        }
    }
    public void SetOwnerPos(Vector3 _charOwnerPos)
    {
        charOwnerPos = _charOwnerPos;

        fixedDirectToCharacter = (positionTarget - charOwnerPos).normalized;
    }
    public void SetTargetPosition(Vector3 _targetPos)
    {
        positionTarget = _targetPos;
    }

    public void SetOwnerChar(CharacterManager _characterOwner)
    {
        characterOwner = _characterOwner;
    }
}
