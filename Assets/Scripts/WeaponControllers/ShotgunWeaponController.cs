using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер оружия
/// </summary>
public class ShotgunWeaponController : BaseWeaponController
{  
    public override void Shoot()
    {
        fxEffect?.SetActive(true);

        List<IDamageble> enemyList = new List<IDamageble>();
        Collider[] EnemyCollider = Physics.OverlapSphere(transform.position, weaponData.RangeAttack, LayerMask.GetMask("Avatar"));
        for (int i = 0; i < EnemyCollider.Length; i++)
        {
            var enemy = EnemyCollider[i].GetComponent<IDamageble>();

            var vector3ToEnemy = EnemyCollider[i].transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, vector3ToEnemy);
            if (angle < weaponData.AttackSector / 2 && enemy != null)
            {
                enemyList.Add(enemy);
            }
        }
    }
}
