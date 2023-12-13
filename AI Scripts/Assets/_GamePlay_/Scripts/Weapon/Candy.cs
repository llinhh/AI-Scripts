using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Weapon
{
    private Transform CandyBulletTf;
    private void Start()
    {
        CandyBulletTf = this.transform;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        positionTarget.y = 0.92f;

        Quaternion lookTarget = Quaternion.LookRotation(fixedDirectToCharacter);

        Vector3 rotation = Quaternion.Lerp(CandyBulletTf.rotation, lookTarget, Time.deltaTime * 30f).eulerAngles;

        CandyBulletTf.Translate(fixedDirectToCharacter * Time.deltaTime * speed, Space.World);

        CandyBulletTf.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }
}
