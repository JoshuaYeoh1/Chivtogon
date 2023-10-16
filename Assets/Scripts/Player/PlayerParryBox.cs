using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryBox : MonoBehaviour
{
    public List<GameObject> parryableTargets = new List<GameObject>();

    void Awake()
    {
        StartCoroutine(slowUpdate());
    }

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

    IEnumerator slowUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(.1f);

            removeDeadFromList();
        }
    }

    void removeDeadFromList()
    {
        for(int i=0;i<parryableTargets.Count;i++)
        {
            if(parryableTargets[i].GetComponent<Enemy>().dead)
            {
                parryableTargets.Remove(parryableTargets[i]);
            }
        }
    }
}
