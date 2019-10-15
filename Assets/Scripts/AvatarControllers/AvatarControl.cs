using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

/// <summary>
/// Контроллер действий аватара
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AvatarWeaponController))]
[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(Collider))]
public class AvatarControl : NetworkBehaviour
{
    public UnityAction<GameObject> OnAvatarSpawn;
    public UnityAction OnDefeat;
    private GameObject avatarPerson;
    private Rigidbody avatarRigidbody;
    private Collider avatarCollider;

    private AnimatorController animatorController;
    private AvatarWeaponController avatarWeaponController;
    private HealthController avatarHealthController;


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
    private int fragCount = 0;

    private Vector2 aimDirection;
    private bool isShoot;
    private bool isAiming;
    private Vector3 moveDirection;

    /// <summary>
    /// персонаж аватара
    /// </summary>
    public GameObject AvatarPerson
    {
        get
        {
            return avatarPerson;
        }
    }
  

    /// <summary>
    /// Установка направления движения
    /// </summary>
    /// <param name="move"></param>
    public void SetMovement(Vector2 move)
    {
        moveDirection = new Vector3(move.x, 0, move.y);
    }

    /// <summary>
    /// Установка атаки
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="shoot"></param>
    public void SetShoot(Vector2 direction, bool shoot)
    {
        aimDirection = direction;

        isShoot = shoot;       
    }

    private void FixedUpdate()
    {
        Aiming();

        Moving();

        Rotating();
    }

    private void Aiming()
    {       
        isAiming = false;
        if (isShoot && aimDirection != Vector2.zero)
        {
            animatorController.SetTrigger("Shoot");
            avatarWeaponController.Shoot();
        }

        if (isShoot == false && aimDirection != Vector2.zero)
        {
            animatorController.SetBool("Aiming", isShoot == false && aimDirection != Vector2.zero);
            isAiming = true;
        }

        if (isShoot && aimDirection == Vector2.zero)
        {
            animatorController.SetTrigger("Shoot");
            avatarWeaponController.Shoot();
        }        
    }

    private void Moving()
    {
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        moveDirection = transform.InverseTransformDirection(moveDirection);
        moveDirection = Vector3.ProjectOnPlane(moveDirection, Vector3.up);

        forwardAmount = 0;
        if (moveDirection.magnitude > 0.01f)
        {
            forwardAmount = moveDirection.magnitude;
        }

        avatarRigidbody.velocity = transform.forward * forwardAmount * moveSpeedMultiplier;

        animatorController.SetFloat("Forward", forwardAmount, 0.1f, Time.fixedDeltaTime);        
    }

    private void Rotating()
    {
        if(isAiming)
        {
            Vector3 vector = new Vector3(aimDirection.x, 0, aimDirection.y);

            vector = transform.InverseTransformDirection(vector);
            vector = Vector3.ProjectOnPlane(vector, Vector3.up);
            turnAmount = Mathf.Atan2(vector.x, vector.z);
            transform.Rotate(0, turnAmount * movingTurnSpeed * Time.deltaTime, 0);
        }
        else 
        if (moveDirection.magnitude > 0.01)
        {
            turnAmount = Mathf.Atan2(-moveDirection.x, -moveDirection.z);
            transform.Rotate(0, turnAmount * movingTurnSpeed * Time.deltaTime, 0);  
        }      

        animatorController.SetFloat("Turn", turnAmount, 0.1f, Time.fixedDeltaTime);
    }

    /// <summary>
    /// Указание Обличия аватара
    /// </summary>
    /// <param name="person"></param>
    public void SetAvatarPerson(GameObject person)
    {
        avatarPerson = person;
        OnAvatarSpawn(avatarPerson);
    }

    private void Awake()
    {
        avatarRigidbody = GetComponent<Rigidbody>();
        animatorController = GetComponent<AnimatorController>();
        avatarWeaponController = GetComponent<AvatarWeaponController>();
        avatarWeaponController.OnKilledObject += KillObject;
        avatarCollider = GetComponent<Collider>();
        avatarHealthController = GetComponent<HealthController>();
        avatarHealthController.OnDead += OnDeath;
    }

    private void Start()
    {             
        if (transform.childCount > 0 && avatarPerson == null)
        {
            avatarPerson = transform.GetChild(0).gameObject;
            OnAvatarSpawn(avatarPerson);
        }

        personInfo = avatarPerson.GetComponent<PersonInfo>();
        avatarWeaponController.Init(personInfo);

        OnAvatarSpawn(avatarPerson);

        if (hasAuthority)
        {
            HealthInfoController.OnSetHealth(avatarHealthController);
        }

    }

    public override void OnStartAuthority()
    {
        HealthInfoController.OnSetHealth(avatarHealthController);
    }

    private void OnDeath()
    {
        RpcDeath();
    }

    [ClientRpc]
    private void RpcDeath()
    {
        avatarCollider.enabled = false;
        Debug.Log("OnDeath");
        OnDefeat?.Invoke();
    }

    /// <summary>
    /// Возрождение
    /// </summary>
    //[ClientRpc]
    public void RpcRevive(Vector3 position)
    {
        transform.position = position;
        animatorController.SetBool("Death", false);
        avatarHealthController.Revive();
        avatarCollider.enabled = true;
    }
   
    private void KillObject()
    {
        RpcKillObject();
    }

    [ClientRpc]
    private void RpcKillObject()
    {
        if (hasAuthority)
        {
            fragCount++;
            FragsInfoController.OnFragCount(fragCount);
        }
    }
}
