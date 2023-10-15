using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineUp : MonoBehaviour
{
    EnemyAdvance advance;
    Collider _other;

    bool triggering;

    void Awake()
    {
        advance=GetComponent<EnemyAdvance>();
    }
    
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer==9) //touch fellow enemy's back trigger box
        {
            triggering=true;
            _other = other;

            advance.stopAdvance();
            LeanTween.moveLocalZ(gameObject, transform.localPosition.z+.01f, 0);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer==9 && !advance.reached) //exit fellow enemy's back trigger box
        {
            triggering=false;

            advance.advance();
        }
    }
    void Update()
    {
        if(triggering && !_other && !advance.reached) //exit back trigger box if fellow enemy was deleted
        {
            triggering=false;

            advance.advance();
        }
    }   
}
