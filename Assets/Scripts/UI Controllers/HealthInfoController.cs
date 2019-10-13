using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Контроллер информации о здоровье аватара
/// </summary>
[RequireComponent(typeof(Slider))]
public class HealthInfoController : MonoBehaviour
{
    public static UnityAction<HealthController> OnSetHealth;
    private Slider sliderHealth;
    private HealthController healthController;

    private void Awake()
    {
        sliderHealth = GetComponent<Slider>();

        OnSetHealth += SetHealthController;
    }

    private void SetHealthController(HealthController _healthController)
    {
        healthController = _healthController;

        healthController.OnHit += SetNewValue;
    }

    private void SetNewValue(int value)
    {
        sliderHealth.value = (float)value / healthController.MaxHealth;
    }
}
