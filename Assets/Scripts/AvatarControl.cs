using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

/// <summary>
/// Контроллер действий аватара
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class AvatarControl : NetworkBehaviour
{   
    private GameObject avatarPerson;
    private Rigidbody avatarRigidbody;

    [SerializeField]
    private Animator avatarAnimator;

    [SerializeField]
    private float movingTurnSpeed = 360;
    [SerializeField]
    private float stationaryTurnSpeed = 180; 
    [SerializeField]
    private float moveSpeedMultiplier = 3f;

    private float turnAmount;
     float forwardAmount;

    /// <summary>
    /// Установка направления движения
    /// </summary>
    /// <param name="move"></param>
    public void SetMovement(Vector2 move)
    {
        Vector3 move3d = new Vector3(move.x, 0, move.y);

        if (move3d.magnitude > 1f)
        {
            move3d.Normalize();
        }

        move3d = transform.InverseTransformDirection(move3d);
        move3d = Vector3.ProjectOnPlane(move3d, Vector3.up);

        forwardAmount = 0;
       

        if (move3d.magnitude > 0.01)
        {       
            turnAmount = Mathf.Atan2(-move3d.x, -move3d.z);    
            transform.Rotate(0, turnAmount * movingTurnSpeed * Time.deltaTime, 0);

            forwardAmount = move3d.magnitude;
        }

        avatarRigidbody.velocity = transform.forward * forwardAmount * moveSpeedMultiplier;

        if (avatarAnimator)
        {
            avatarAnimator.SetFloat("Forward", forwardAmount, 0.1f, Time.fixedDeltaTime);
            avatarAnimator.SetFloat("Turn", turnAmount, 0.1f, Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Установка атаки
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="shoot"></param>
    public void SetShoot(Vector2 direction, bool shoot)
    {
        if(shoot && direction == Vector2.zero)
        {
            Debug.Log("near Shoot");
        }

        if (shoot && direction != Vector2.zero)
        {
            Debug.Log("direction Shoot");
        }

        if (shoot == false && direction != Vector2.zero)
        {
            Debug.Log("aiming");
        }

        if (shoot == false && direction == Vector2.zero)
        {
           
        }
    }
    
    /// <summary>
    /// Указание Обличия аватара
    /// </summary>
    /// <param name="person"></param>
    public void SetAvatarPerson(GameObject person)
    {
        avatarPerson = person;        
        if (avatarAnimator == null)
        {
            avatarAnimator = avatarPerson.GetComponent<Animator>();
        }
    }   

    private void Start()
    {       
        avatarRigidbody = GetComponent<Rigidbody>();  
    }

    

    /* [Command]
     private void CmdSpawnPerson(string avatarPersonName)
     {
         spawnedPlayerPerson = Instantiate(PlayerInfo.CurrentAvatarData.AvatarPrefab, transform);

         NetworkServer.SpawnWithClientAuthority(spawnedPlayerPerson);
     }*/

    private void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        // float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
       
    }

    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (Time.fixedDeltaTime > 0 && avatarAnimator != null)
        {
            Vector3 v = (avatarAnimator.deltaPosition * moveSpeedMultiplier) / Time.fixedDeltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = avatarRigidbody.velocity.y;
            avatarRigidbody.velocity = v;
        }
    }
}
