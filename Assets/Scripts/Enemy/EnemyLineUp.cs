using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyLineUp : MonoBehaviour
{
    Enemy enemy;
    EnemyAdvance advance;
    EnemyStrafe strafe;
    Collider _other;

    bool triggering;

    void Start()
    {
        enemy=GetComponent<Enemy>();
        advance=GetComponent<EnemyAdvance>();
        strafe=GetComponent<EnemyStrafe>();
    }
    
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer==9 && !advance.reached && !enemy.dead) //touch fellow enemy's back trigger box
        {
            triggering=true;
            _other = other;

            pauseMove();
            pushBack();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer==9 && !advance.reached) //exit fellow enemy's back trigger box
        {
            triggering=false;

            resumeMoveRt = StartCoroutine(resumeMoveDelay(Random.Range(1f,3f)));
        }
    }
    void Update()
    {
        if(triggering && !_other && !advance.reached) //exit back trigger box if fellow enemy was deleted
        {
            triggering=false;

            resumeMoveRt = StartCoroutine(resumeMoveDelay(Random.Range(1f,3f)));
        }
    }

    void pushBack()
    {
        LeanTween.moveLocalZ(gameObject, transform.localPosition.z+.01f, 0);
    }

    void pauseMove()
    {
        if(resumeMoveRt!=null)
        StopCoroutine(resumeMoveRt);

        advance.pauseAdvance();
        strafe.canStrafe=false;
        strafe.stopStrafe();
    }

    Coroutine resumeMoveRt;

    IEnumerator resumeMoveDelay(float time)
    {
        yield return new WaitForSeconds(time);

        if(Singleton.instance.playerAlive && !enemy.dead)
        {
            advance.advance();

            yield return new WaitForSeconds(time);

            if(!advance.reached)
            {
                strafe.canStrafe=true;
                strafe.strafe();
            }
        }
    }
}
