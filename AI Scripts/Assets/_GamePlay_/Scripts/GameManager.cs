using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton Pattern
    public static GameManager Ins;
    private void Awake()
    {
        Ins = this;
    }


}
