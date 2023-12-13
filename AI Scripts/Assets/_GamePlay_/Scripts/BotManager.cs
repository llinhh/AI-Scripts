using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    public static BotManager Ins;
    private void Awake()
    {
        Ins = this;
    }

    public List<Bot> ListBotOnMap = new List<Bot>();

    public void GetRandomWeapon(Bot bot)
    {
        int RandomWeapon = Random.Range(Random.Range(0, 10), GameManager.Ins.ListWeapon.Count);
        bot.WeaponInHand = GameManager.Ins.ListWeapon[RandomWeapon].gameObject;
    }
}
