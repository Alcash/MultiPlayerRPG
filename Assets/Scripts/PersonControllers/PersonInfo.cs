using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject socketWeapon;
   
    public GameObject SocketWeapon
    {
        get
        {
            return socketWeapon;
        }
    }
}
