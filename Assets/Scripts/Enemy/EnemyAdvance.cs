using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdvance : MonoBehaviour
{
    Player player;
    EnemyStrafe strafe;
    public Animator anim;

    Vector3 startPos;
    public bool reached;
    public float goalZMin=1.2f, goalZMax=1.8f, travelTimeMin=4, travelTimeMax=5;
    
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

        //advanceLt = LeanTween.moveLocalZ(gameObject, goalZ, time).id;
        advanceLt = LeanTween.value(transform.localPosition.z, goalZ, time).setOnUpdate(updateZ).id;

        anim.SetBool("advancing", true);

        yield return new WaitForSeconds(time);

        reached=true;

        if(strafe)
        strafe.canStrafe=false;

        anim.SetBool("advancing", false);
    }

    void updateZ(float value)
    {
        transform.localPosition = new Vector3(0,0,value);
    }

    public void pauseAdvance()
    {
        LeanTween.cancel(advanceLt);

        if(advanceRt!=null)
        StopCoroutine(advanceRt);
        
        anim.SetBool("advancing", false);
    }  

    public void stopAdvance()
    {
        LeanTween.cancel(advanceLt);

        if(advanceRt!=null)
        StopCoroutine(advanceRt);
        
        if(strafe)
        strafe.stopStrafe();

        anim.SetBool("advancing", false);
    }
}
