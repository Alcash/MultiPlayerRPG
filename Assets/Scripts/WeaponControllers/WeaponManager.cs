using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// менеджер оружия
/// </summary>
public class WeaponManager : MonoBehaviour
{

    [SerializeField]
    private WeaponData[] weaponDatas;


    private static Dictionary<string, WeaponData> personWeapon;

    private void Awake()
    {
        personWeapon = new Dictionary<string, WeaponData>();

        foreach (var weapon in weaponDatas)
        {
            personWeapon.Add(weapon.NameWeapon, weapon);
        }
    }

    public static WeaponData GetWeaponData(string weaponName)
    {
        if ((weaponName == "" || personWeapon.ContainsKey(weaponName) == false) && personWeapon.Count > 0)
        {
            List<string> keys = new List<string>(personWeapon.Keys);
            weaponName = keys[0];
        }

        if (personWeapon.ContainsKey(weaponName))
        {
            return personWeapon[weaponName];
        }
        else
        {
            return null;
        }
    }
}
