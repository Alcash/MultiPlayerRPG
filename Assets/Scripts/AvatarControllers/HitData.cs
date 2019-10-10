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

    public HitData(float _damage)
    {
        damage = _damage;
    }
}
