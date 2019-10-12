using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные о попадании
/// </summary>
public struct HitData
{   
    private float damage;

    private AvatarWeaponController owner;

    public float Damage
    {
        get
        {
            return damage;
        }
    }

    public HitData(float _damage, AvatarWeaponController _owner)
    {
        damage = _damage;
        owner = _owner;
    }
}
