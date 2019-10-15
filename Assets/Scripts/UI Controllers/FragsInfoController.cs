using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Контроллер информации о убийствах
/// </summary>
public class FragsInfoController : MonoBehaviour
{
    public static UnityAction<int> OnFragCount;

    [SerializeField]
    private Text textFrags;

    private void Awake()
    {
        OnFragCount += UpdateUI;
    }

    private void UpdateUI(int value)
    {
        textFrags.text = value.ToString();
    }
}
