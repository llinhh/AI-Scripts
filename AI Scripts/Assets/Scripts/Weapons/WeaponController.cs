using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : WeaponManager
{
    Transform weaponTransform;
    private void Start()
    {
        weaponTransform = this.transform;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        positionTarget.y = 0.92f;

        Quaternion lookTarget = Quaternion.LookRotation(fixedDirectToCharacter);

        Vector3 rotation = Quaternion.Lerp(weaponTransform.rotation, lookTarget, Time.deltaTime * 30f).eulerAngles;

        weaponTransform.Translate(fixedDirectToCharacter * Time.deltaTime * speed, Space.World);

        weaponTransform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}