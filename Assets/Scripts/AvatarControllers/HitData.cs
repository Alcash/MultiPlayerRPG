using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Данные о попадании
/// </summary>
public struct HitData
{   
    private float damage;

   

    public float Damage
    {
        get
        {
            return damage;
        }
    }

    private AvatarWeaponController owner;

    public AvatarWeaponController Owner
    {
        get
        {
            return owner;
        }
    }

    public HitData(float _damage, AvatarWeaponController _owner)
    {
        damage = _damage;
        owner = _owner;
    }    
}
