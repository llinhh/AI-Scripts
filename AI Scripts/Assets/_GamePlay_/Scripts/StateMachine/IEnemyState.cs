using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Enter(Bot bot);

    void Execute();

    void Exit();
}