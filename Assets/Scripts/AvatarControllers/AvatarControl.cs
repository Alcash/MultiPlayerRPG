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

        animatorController.SetFloat("Forward", forwardAmount, 0.1f, Time.fixedDeltaTime);
        animatorController.SetFloat("Turn", turnAmount, 0.1f, Time.fixedDeltaTime);

    }

    /// <summary>
    /// Установка атаки
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="shoot"></param>
    public void SetShoot(Vector2 direction, bool shoot)
    {
        animatorController.SetBool("Aiming", shoot == false && direction != Vector2.zero);

        if (shoot && direction != Vector2.zero)
        {
            animatorController.SetTrigger("Shoot");
            avatarWeaponController.Shoot();            
        }

        if (shoot == false && direction != Vector2.zero)
        {

            animatorController.SetBool("Aiming", shoot == false && direction != Vector2.zero);

            Vector3 vector = new Vector3(direction.x, 0, direction.y);

            vector = transform.InverseTransformDirection(vector);
            vector = Vector3.ProjectOnPlane(vector, Vector3.up);

            turnAmount = Mathf.Atan2(vector.x, vector.z);

            transform.Rotate(0, turnAmount * movingTurnSpeed * Time.deltaTime, 0);

            animatorController.SetFloat("Turn", turnAmount, 0.1f, Time.fixedDeltaTime);
        }

        if (shoot && direction == Vector2.zero)
        {
            animatorController.SetTrigger("Shoot");
            avatarWeaponController.Shoot();          
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
        OnAvatarSpawn(avatarPerson);
    }

    private void Awake()
    {
        avatarRigidbody = GetComponent<Rigidbody>();
        animatorController = GetComponent<AnimatorController>();
        avatarWeaponController = GetComponent<AvatarWeaponController>();
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
        avatarCollider.enabled = false;
        OnDefeat?.Invoke();
    }

    /// <summary>
    /// Возрождение
    /// </summary>
    public void Revive()
    {
        animatorController.SetBool("Death", false);      

        avatarCollider.enabled = true;
    }
}
