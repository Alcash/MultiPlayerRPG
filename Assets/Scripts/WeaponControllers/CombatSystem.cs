using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Боевая система
/// </summary>
public class CombatSystem : MonoBehaviour
{
    public static void CalculateDamage(IDamagable reciever, HitData hitData)
    {
      
        bool objectWasKilled = reciever.TakeHit(hitData);

        if(objectWasKilled)
        {
            hitData.Owner.OnKilledObject?.Invoke();
        }
    }   
}
