using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// менеджер локаций
/// </summary>
public class EnviromentManager : NetworkBehaviour
{
    [SerializeField]
    private LocationData[] locationDatas;

    [SyncVar]
    private int index;

    public override void OnStartServer()
    {
        int index = Random.Range(0, locationDatas.Length);
    }

    private void Start()
    {
        if(isClient)
        {
            Instantiate(locationDatas[index].LocationPrefab, transform);
        }
    }
}
