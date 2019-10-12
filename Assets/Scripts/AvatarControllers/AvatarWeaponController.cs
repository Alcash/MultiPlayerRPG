using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Контроллер оружия аватара
/// </summary>
public class AvatarWeaponController : NetworkBehaviour
{
    [SerializeField]
    private WeaponData defaultWeapon;    
    private BaseWeaponController weaponController;  
       

    public void Shoot()
    {
        //weaponController?.Shoot();
        CmdNetShoot();
    }

    [Command]
    private void CmdNetShoot()
    {
        weaponController?.Shoot();
    }

    internal void Init(PersonInfo personInfo)
    {
        GameObject weapon = Instantiate(WeaponManager.GetWeaponData(defaultWeapon.NameWeapon).ModelPrefab, personInfo.SocketWeapon.transform);

        weaponController = weapon.GetComponent<BaseWeaponController>();
        weaponController.InitWeapon(defaultWeapon, this);

       // weaponController.OnHit()
    }
}
