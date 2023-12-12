using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<CharacterManager> _listCharacter = new List<CharacterManager>();

    public bool isGameActive;

    public bool isWin;

    public bool isLose;

    [HideInInspector] public const string prefsCoinsTag = "Coins";

    public int Coins;

    public int characterCount;

    public int TotalAlive;

    public GameObject Hammer;

    public GameObject Candy;

    public GameObject Knife;

    public int LevelID;

    private void Awake()
    {
        InitializeSingleton();

        isGameActive = false;

        /*LevelID = 1;

        LevelID = PlayerPrefs.GetInt("LevelID");

        Coins = PlayerPrefs.GetInt("Coins");

        GUIManager.Instance.CoinsText.text = PlayerPrefs.GetInt("Coins").ToString();*/
    }
    private void Update()
    {
        /*GUIManager.Instance.CoinsText.text = PlayerPrefs.GetInt("Coins").ToString();

        if (Coins <= 0)
        {
            Coins = 0;

            GUIManager.Instance.CoinsText.text = Coins.ToString();
        }*/

        if (isGameActive == true)
        {
            Hammer.SetActive(true);
            Candy.SetActive(true);
            Knife.SetActive(true);
        }
    }

    private void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void AddCharacter(CharacterManager Character)
    {
        _listCharacter.Add(Character);
    }

    /*public void AddCoins(int CoinsToAdd)
    {
        Coins += CoinsToAdd;

        PlayerPrefs.SetInt("Coins", Coins);

        GUIManager.Instance.CoinsText.text = Coins.ToString();
    }*/

    /*public void LoadLevel()
    {
        LevelID++;

        if (LevelID > 2)
        {
            LevelID = 1;
        }

        PlayerPrefs.SetInt("LevelID", LevelID);

        PlayerPrefs.Save();

        SceneManager.LoadScene("Level" + LevelID);
    }*/
}
