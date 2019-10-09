using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxDisabling : MonoBehaviour
{
    [SerializeField]
    private float delayEffect  = 0.2f;

    private void OnEnable()
    {
        Invoke("Disable", delayEffect);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
