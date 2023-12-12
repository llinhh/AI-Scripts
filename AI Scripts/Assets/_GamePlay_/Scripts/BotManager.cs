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

    
}
