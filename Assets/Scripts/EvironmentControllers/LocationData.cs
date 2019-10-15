using UnityEngine;

/// <summary>
/// Информация о локации
/// </summary>
[CreateAssetMenu(fileName = "LocationData", menuName = "ScriptableObject/Location Data", order = 51)]
public class LocationData : ScriptableObject
{
    [SerializeField]
    private GameObject locationPrefab;

    /// <summary>
    /// Префаб локации
    /// </summary>
    public GameObject LocationPrefab
    {
        get
        {
            return locationPrefab;
        }
    }

    [SerializeField]
    private string locationName;

    /// <summary>
    /// название локации
    /// </summary>
    public string LocationName
    {
        get
        {
            return locationName;
        }
    }
}
