using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Информация о локации
/// </summary>
[CreateAssetMenu(fileName = "LocationData", menuName = "ScriptableObject/Location Data", order = 51)]
public class LocationData : MonoBehaviour
{
    [SerializeField]
    private GameObject locationPrefab;

    /// <summary>
    /// Префаб аватара
    /// </summary>
    public GameObject LocationPrefab
    {
        get
        {
            return locationPrefab;
        }
    }

    [SerializeField]
    private GameObject locationName;

    /// <summary>
    /// Префаб аватара
    /// </summary>
    public GameObject LocationName
    {
        get
        {
            return locationName;
        }
    }
}
