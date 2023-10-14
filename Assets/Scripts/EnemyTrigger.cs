using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    Enemy enemy;
    
    void Awake()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            if(!enemy.reached)
            {
                enemy.stopAdvance();

                enemy.canStrafe=false;

                Debug.Log("touched");
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag=="Enemy")
        {
            if(!enemy.reached)
            {
                enemy.advance();

                enemy.canStrafe=true;
            }
        }
    }
}
