using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterState
{
    void Enter(CharacterController character);

    void Execute();

    void Exit();

}