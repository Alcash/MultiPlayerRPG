using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Аниматок автара
/// </summary>
[RequireComponent(typeof(AvatarControl))]
public class AnimatorController : MonoBehaviour
{
    // Start is called before the first frame update   
    
    private Animator avatarAnimator;
    private NetworkAnimator networkAnimator;

    private void Awake()
    {
        AvatarControl avatarControl = GetComponent<AvatarControl>();
        InitAnimator(avatarControl.AvatarPerson);
        avatarControl.OnAvatarSpawn += InitAnimator;
        avatarControl.OnDefeat += OnDeath;
    }

    public void InitAnimator(GameObject avatar)
    {
        if (avatar != null)
        {
            avatarAnimator = avatar.GetComponent<Animator>();
            networkAnimator = avatar.GetComponent<NetworkAnimator>();
            networkAnimator.SetParameterAutoSend(3, true);
        }        
    }

    private void OnDeath()
    {
        SetBool("Death", true);
        SetTrigger("Dying");       
    }

    public void SetFloat(string key, float value,float dampTime, float deltaTime)
    {
        avatarAnimator.SetFloat(key, value, dampTime, deltaTime);
    }

    public void SetFloat(string key, float value)
    {
        avatarAnimator.SetFloat(key, value);
    }

    public void SetBool(string key, bool value )
    {
        avatarAnimator.SetBool(key, value);
    }

    public void SetTrigger(string key)
    {
        avatarAnimator.SetTrigger(key);
        networkAnimator.SetTrigger(key);
    }
}
