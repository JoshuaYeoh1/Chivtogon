using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [HideInInspector] public GameObject target;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==8) //touch enemy
        {
            target = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer==8 && other==target.gameObject) //exit enemy
        {
            target = null;
        }
    }
}
