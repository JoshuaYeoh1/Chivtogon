using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [HideInInspector] public GameObject parent;
    EnemyAdvance advance;
    EnemyStrafe strafe;
    
    void Awake()
    {
        parent=transform.parent.gameObject;
        advance=parent.GetComponent<EnemyAdvance>();
        strafe=parent.GetComponent<EnemyStrafe>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            if(!advance.reached)
            {
                advance.stopAdvance();

                strafe.canStrafe=false;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag=="Enemy")
        {
            if(!advance.reached)
            {
                advance.advance();

                strafe.canStrafe=true;
            }
        }
    }
}
