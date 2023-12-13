using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Weapon
{
    private Transform CandyAmmoTransform;
    private void Start()
    {
        CandyAmmoTransform = this.transform;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        positionTarget.y = 0.92f;

        Quaternion lookTarget = Quaternion.LookRotation(fixedDirectToCharacter);

        Vector3 rotation = Quaternion.Lerp(CandyAmmoTransform.rotation, lookTarget, Time.deltaTime * 30f).eulerAngles;

        CandyAmmoTransform.Translate(fixedDirectToCharacter * Time.deltaTime * speed, Space.World);

        CandyAmmoTransform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }
}
