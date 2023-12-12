using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Respawn : MonoBehaviour
{
    public List<CharacterController> characterList = new List<CharacterController>();

    public List<GameObject> spawnPos = new List<GameObject>();

    //public NameCharacter EnemyName;

    public const string RespawnTag = "Respawn";

    // Update is called once per frame
    void Start()
    {
        InvokeRepeating(RespawnTag, 0, 0.05f);
    }

    public void ReSpawn()
    {
        if (GameManager.Instance.characterCount <= 0)
        {
            return;
        }

        for (int i = 0; i < spawnPos.Count; i++)
        {
            if (characterList[i].gameObject.activeInHierarchy == false)
            {
                characterList[i].transform.position = spawnPos[Random.Range(Random.Range(0, 10), i)].transform.position;

                GameManager.Instance.characterCount -= 1;

                Spawn(characterList[i]);
            }
        }
    }

    public void Spawn(CharacterController character)
    {
        character.gameObject.SetActive(true);

        character.isDead = false;

        character.enabled = true;

        //character.Score = 0;

        //character.ScoreText.text = character.Score.ToString();

        character.Name.text = "" + (NameCharacter)Random.Range(0, 9);

        character.agent.enabled = true;

        character._collider.enabled = true;

        character.rb.detectCollisions = true;

        if (!GameManager.Instance._listCharacter.Contains(character.GetComponent<CharacterManager>()))
        {
            GameManager.Instance._listCharacter.Add(character.GetComponent<CharacterManager>());
        }
        character.ChangeState(new PatrolState());
    }
}
