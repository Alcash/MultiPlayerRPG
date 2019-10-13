using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Прячется полностью пока в убежище
/// </summary>
[RequireComponent(typeof(AvatarControl))]
public class HideSelf : MonoBehaviour, IHidable
{
    private GameObject avatar;

    public void HideObject()
    {
        avatar.SetActive(false);
    }

    public void UnHideObject()
    {
        avatar.SetActive(true);
    }

    private void Awake()
    {
        GetComponent<AvatarControl>().OnAvatarSpawn += Init;
    }

    private void Init(GameObject _avatar)
    {
        avatar = _avatar;
    }
}
