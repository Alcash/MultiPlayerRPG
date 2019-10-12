
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Контроллер здоровья
/// </summary>
public class HealthController : NetworkBehaviour, IDamagable
{
    public UnityEngine.Events.UnityAction<int> OnHit = null;
    public UnityEngine.Events.UnityAction OnDead = null;

    [SyncVar]
    [SerializeField]
    private int maxHealth;

    [SyncVar(hook="OnHealthChanged")]
    private int currentHealth;  
    
    /// <summary>
    /// Макисмальное здоровье
    /// </summary>
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
        {
            Debug.Log(name + " hit damage " + hitData.Damage);
            currentHealth = currentHealth - (int)hitData.Damage;
            if (currentHealth < 0)
            {

                currentHealth = 0;

                OnDead?.Invoke();
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
