using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingWeapon : WeaponBase
{
    [SerializeField] float orbitRadius = 2;
    [SerializeField] float orbitSpeed = 1;

    public override void UpdateWeapon()
    {
        OrbiterMovement();
    }

    void OrbiterMovement()
    {
        Vector3 orbitPos = new Vector3(Mathf.Cos(Time.time * orbitSpeed), Mathf.Sin(Time.time * orbitSpeed), 0) * orbitRadius;
        transform.localPosition = orbitPos;

    }
}
