
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Контроллер здоровья
/// </summary>
public class HealthController : NetworkBehaviour, IDamagable
{
    public UnityEngine.Events.UnityAction<int> OnHit = null;
    public UnityEngine.Events.UnityAction OnDead = null;
    public UnityEngine.Events.UnityAction<int> OnHealthChanged = null;

    [SyncVar]
    [SerializeField]
    private int maxHealth;

    [SyncVar(hook="OnHealthChangedHook")]
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
    public bool TakeHit(HitData hitData)
    {
        {
            Debug.Log(name + " hit damage " + hitData.Damage);
            currentHealth = currentHealth - (int)hitData.Damage;
            if (currentHealth < 0)
            {

                currentHealth = 0;

                OnDead?.Invoke();

                return true;
            }
            if (OnHit != null)
                OnHit(currentHealth);
        }

        return false;
    }

    private void OnHealthChangedHook(int health)
    {
        currentHealth = health;
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void Revive()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void Awake()
    {
        Revive();
    }   
}
