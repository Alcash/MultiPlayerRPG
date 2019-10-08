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

    [SyncVar]
    private float turnAmount;
    [SyncVar]
    private float forwardAmount;

    private PersonInfo personInfo;

    [SerializeField]
    private WeaponData defaultWeapon;

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


        avatarAnimator.SetBool("Aiming", shoot == false && direction != Vector2.zero);

        if (shoot && direction != Vector2.zero)
        {
            avatarAnimator.SetTrigger("Shoot");
            Debug.Log("Aimming shoot");
        }

        if (shoot == false && direction != Vector2.zero)
        {

            avatarAnimator.SetBool("Aiming", shoot == false && direction != Vector2.zero);

            Vector3 vector = new Vector3(direction.x, 0, direction.y);

            vector = transform.InverseTransformDirection(vector);
            vector = Vector3.ProjectOnPlane(vector, Vector3.up);

            turnAmount = Mathf.Atan2(vector.x, vector.z);

           // turnAmount -= Mathf.Atan2(transform.forward.x, transform.forward.z);

            Debug.Log("direction " + vector);
            Debug.Log("turnAmount " + turnAmount);
            transform.Rotate(0, turnAmount * movingTurnSpeed * Time.deltaTime, 0);           

            avatarAnimator.SetFloat("Turn", turnAmount, 0.1f, Time.fixedDeltaTime);
            Debug.Log("Aimming");
        }

        if (shoot && direction == Vector2.zero)
        {
            avatarAnimator.SetTrigger("Shoot");
            Debug.Log("Shoot near");
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

        if(transform.childCount > 0)
        {
            avatarPerson = transform.GetChild(0).gameObject;
        }

        if (avatarAnimator == null)
        {
            avatarAnimator = avatarPerson.GetComponent<Animator>();
        }

        personInfo = avatarPerson.GetComponent<PersonInfo>();

        GameObject weapon = Instantiate(WeaponManager.GetWeaponData(defaultWeapon.NameWeapon).ModelPrefab, personInfo.SocketWeapon.transform);
    }  
}
