using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideoutController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        IHidable hidable = other.GetComponent<IHidable>();
        if(hidable != null)
        {
            hidable.HideObject();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IHidable hidable = other.GetComponent<IHidable>();
        if (hidable != null)
        {
            hidable.UnHideObject();
        }
    }
}
