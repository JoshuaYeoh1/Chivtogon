using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryBox : MonoBehaviour
{
    public List<GameObject> parryableTargets = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==8) //touch enemy
        {
            parryableTargets.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer==8 && parryableTargets.Contains(other.gameObject)) //exit enemy
        {
            parryableTargets.Remove(other.gameObject);
        }
    }
}
