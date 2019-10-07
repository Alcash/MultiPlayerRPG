using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Информация о аватаре
/// </summary>
[CreateAssetMenu(fileName = "AvatarData", menuName = "ScriptableObject/Avatar Data", order = 51)]
public class AvatarData : ScriptableObject
{
    [SerializeField]
    private GameObject avatarPrefab;

    /// <summary>
    /// Префаб аватара
    /// </summary>
    public GameObject AvatarPrefab
    {
        get
        {
            return avatarPrefab;
        }
    }
}
