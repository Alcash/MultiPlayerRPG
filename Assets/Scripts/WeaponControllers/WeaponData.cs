using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="weaponData", menuName ="ScriptableObject/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string nameWeapon = "default";

    /// <summary>
    /// название оружия
    /// </summary>
    public string NameWeapon
    {
        get
        {
            return nameWeapon;
        }
    }

    [SerializeField]
    private GameObject modelPrefab;

    /// <summary>
    /// Модель оружия
    /// </summary>
    public GameObject ModelPrefab
    {
        get
        {
            return modelPrefab;
        }
    }

    [SerializeField]
    private float damageWeapon = 3;

    /// <summary>
    /// Урон стрельбы
    /// </summary>
    public float DamageWeapon
    {
        get
        {
            return damageWeapon;
        }
    }

    [SerializeField]
    private float rangeAttack = 3;

    /// <summary>
    /// дальность стрельбы
    /// </summary>
    public float RangeAttack
    {
        get
        {
            return rangeAttack;
        }
    }

   

    [SerializeField]
    private float attackSector = 90;

    /// <summary>
    /// Сектор атаки
    /// </summary>
    public float AttackSector
    {
        get
        {
            return attackSector;
        }
    }
    
    [SerializeField]
    private float attackSpeed = 1;
    /// <summary>
    /// скорость атаки
    /// </summary>
    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }
    }

   

    
}
