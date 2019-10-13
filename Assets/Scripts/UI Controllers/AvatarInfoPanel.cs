using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Панель информации игрока
/// </summary>
public class AvatarInfoPanel : MonoBehaviour
{
     
    public static UnityAction<string> SetInfoPlayer;
    public static UnityAction<WeaponData> SetInfoWeapon;

    [SerializeField]
    private Text textPlayerName;

    [SerializeField]
    private Text textGunInfo;

    [SerializeField]
    private WeaponData weaponData;
      
    private void Awake()
    {
        SetInfoPlayer += SetInfo;
        SetInfoWeapon += SetWeaponData;

        transform.parent.GetComponent<BaseWindowMenu>().OnOpenMenu += OnInfo;
    }   
       

    private void OnInfo()
    {
        string result = "";
        result = string.Format("Name {0}.\nDamage: {1}.\nRange: {2}.\nSector:{3}.",
                 weaponData.NameWeapon, weaponData.DamageWeapon, weaponData.RangeAttack, weaponData.AttackSector);
        textGunInfo.text = result;
        Debug.Log("OnIfno");
    }

    private void SetInfo(string playerName)
    {
        textPlayerName.text = playerName;
    }

    private void SetWeaponData(WeaponData _weaponData)
    {
        weaponData = _weaponData;
    }

}
