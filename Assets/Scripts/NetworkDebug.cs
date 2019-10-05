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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

    [Command]
    private void CmdLog(string message)
    {
        Debug.Log(message);
    }

    [Command]
    private  void CmdLogError(string message)
    {
        Debug.LogError(message);
    }
    
    private void LogEditor(string message)
    {
        CmdLog(message);

    }
    private  void LogErrorEditor(string message)
    {

        CmdLogError(message);

    }
}
