using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCandyThrow : MonoBehaviour
{
    private static PoolCandyThrow instance;

    public static PoolCandyThrow Instance { get { return instance; } }

    public GameObject ThrowPrefab;

    public List<GameObject> Throw = new List<GameObject>();

    public int ThrowAmount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnThrow();
    }

    public void SpawnThrow()
    {
        for (int i = 0; i < ThrowAmount; i++)
        {
            GameObject throwSpawn = Instantiate(ThrowPrefab);

            throwSpawn.SetActive(false);

            throwSpawn.transform.SetParent(transform);

            Throw.Add(throwSpawn);
        }
    }

    public GameObject GetPooledThrow()
    {
        for(int i = 0; i < Throw.Count; i++)
        {
            if (!Throw[i].activeInHierarchy)
            {
                return Throw[i];
            }
        }

        return null;
    }
}
