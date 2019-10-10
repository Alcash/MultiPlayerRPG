using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthController : NetworkBehaviour, IDamagable
{
    public UnityEngine.Events.UnityAction<int> OnHit = null;

    [SyncVar]
    [SerializeField]
    private int maxHealth;

    [SyncVar(hook="OnHealthChanged")]
    private int currentHealth;  
    
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    /// <summary>
    /// Получение воздействия
    /// </summary>
    /// <param name="hitData"></param>
    public void TakeHit(HitData hitData)
    {
        
       
        //if (isServer == false)
        {
            Debug.Log(name + " hit damage " + hitData.Damage);
            currentHealth = currentHealth - (int)hitData.Damage;
            if (currentHealth < 0)
            {

                currentHealth = 0;
            }
            if (OnHit != null)
                OnHit(currentHealth);
        }
    }  

    private void OnHealthChanged(int health)
    {
        currentHealth = health;
    }
    private void Awake()
    {
        currentHealth = maxHealth;
    }   
}
