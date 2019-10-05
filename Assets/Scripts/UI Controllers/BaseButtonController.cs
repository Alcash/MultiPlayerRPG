using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Базовый класс кнопки
/// </summary>
[RequireComponent(typeof(Button))]
public abstract class BaseButtonController : MonoBehaviour
{
    private Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    protected abstract void ButtonClicked();
}
