using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine;
using System;

/// <summary>
/// Сетевой дебаг
/// </summary>
public class NetworkDebug : NetworkBehaviour
{
    private static NetworkDebug instance;

    public static UnityAction<string> Log;
    public static UnityAction<string> LogError;

    public void Awake()
    {
        base.OnStartLocalPlayer();

        if (instance == null)
        {
            instance = this;          
        }
        else
        {
            Destroy(gameObject);
        }
        

       Log += LogEditor;
       LogError += LogErrorEditor;
    }

    private void OnDisable()
    {
        Log -= LogEditor;
        LogError -= LogErrorEditor;
    }

    [ClientRpc]
    private void RpcLog(string message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }

    [ClientRpc]
    private  void RpcLogError(string message)
    {
#if UNITY_EDITOR
        Debug.LogError(message);
#endif
    }

    private void LogEditor(string message)
    {
        RpcLog(message);

    }
    private  void LogErrorEditor(string message)
    {

        RpcLogError(message);

    }
}
