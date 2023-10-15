using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdvance : MonoBehaviour
{
    Player player;
    EnemyStrafe strafe;
    public Animator anim;

    Vector3 startPos;
    public bool reached, triggering;
    public float goalZMin=1.75f, goalZMax=2.25f, travelTimeMin=4, travelTimeMax=6;
    
    void Awake()
    {
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        strafe=GetComponent<EnemyStrafe>();

        startPos = transform.localPosition;
        advance();
    }

    public void advance()
    {
        advanceRt = StartCoroutine(advancing());
    }

    int advanceLt=0;
    Coroutine advanceRt;

    IEnumerator advancing()
    {
        float goalZ = Random.Range(goalZMin,goalZMax);
        float travelTime = Random.Range(travelTimeMin,travelTimeMax);

        float time = travelTime*(transform.localPosition.z-goalZ)/(startPos.z-goalZ);

        advanceLt = LeanTween.moveLocalZ(gameObject, goalZ, time).id;

        anim.SetBool("advancing", true);

        yield return new WaitForSeconds(time);

        reached=true;

        strafe.canStrafe=false;

        anim.SetBool("advancing", false);
    }

    public void stopAdvance()
    {
        LeanTween.cancel(advanceLt);

        if(advanceRt!=null)
        StopCoroutine(advanceRt);
        
        strafe.stopStrafe();

        anim.SetBool("advancing", false);
    }

    Collider _other;
    
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer==9) //touch fellow enemy
        {
            triggering=true;
            _other = other;

            stopAdvance();
            LeanTween.moveLocalZ(gameObject, transform.localPosition.z+.01f, 0);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer==9 && !reached) //exit fellow enemy
        {
            triggering=false;

            advance();
        }
    }
    void Update()
    {
        if(triggering && !_other && !reached) //exit trigger if fellow enemy was deleted
        {
            triggering=false;

            advance();
        }
    }     
}
