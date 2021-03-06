﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Контроллер дробовика
/// </summary>
public class ShotgunWeaponController : BaseWeaponController
{  
    public override void Shoot()
    {
        fxEffect?.SetActive(true);

        List<IDamagable> enemyList = new List<IDamagable>();
        Collider[] enemyCollider = Physics.OverlapSphere(transform.position, weaponData.RangeAttack, LayerMask.GetMask("Avatar"));
        for (int i = 0; i < enemyCollider.Length; i++)
        {
            if(enemyCollider[i].gameObject == avatarOwner.gameObject)
            {
                continue;
            }
            var enemy = enemyCollider[i].GetComponent<IDamagable>();

            RaycastHit raycastHit;

            Physics.Raycast(transform.position, enemyCollider[i].transform.position - transform.position, out raycastHit);           
            if (raycastHit.collider == enemyCollider[i])
            {
                var vector3ToEnemy = enemyCollider[i].transform.position - transform.position;
                var angle = Vector3.Angle(transform.forward, vector3ToEnemy);
                if (angle < weaponData.AttackSector / 2 && enemy != null)
                {

                    enemyList.Add(enemy);

                    CombatSystem.CalculateDamage(enemy, hitData);
                }
            }
        }
    }
}
