using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnimatorController : MonoBehaviour
{
    // Start is called before the first frame update   
    
    private Animator avatarAnimator;
    private NetworkAnimator networkAnimator;

    public void InitAnimator()
    {
        if (avatarAnimator == null)
        {
            avatarAnimator = gameObject.GetComponentInChildren<Animator>();
            networkAnimator = gameObject.GetComponentInChildren<NetworkAnimator>();
        }        
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
