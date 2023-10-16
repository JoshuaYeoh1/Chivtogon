using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdvance : MonoBehaviour
{
    Enemy enemy;
    //Player player;
    EnemyStrafe strafe;
    public Animator anim;

    Vector3 startPos;
    public bool reached;
    public float goalZMin=1.2f, goalZMax=1.8f, travelTimeMin=4, travelTimeMax=5;
    
    void Start()
    {
        enemy=GetComponent<Enemy>();
        //player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        strafe=GetComponent<EnemyStrafe>();

        startPos = transform.localPosition;
        advance();
    }

    public void advance()
    {
        if(!enemy.dead && Singleton.instance.playerAlive)
        {
            advanceRt = StartCoroutine(advancing());
        }
    }

    int advanceLt=0;
    Coroutine advanceRt;

    IEnumerator advancing()
    {
        float goalZ = Random.Range(goalZMin,goalZMax);
        float travelTime = Random.Range(travelTimeMin,travelTimeMax);

        float time = travelTime*(transform.localPosition.z-goalZ)/(startPos.z-goalZ);

        tweenMove(goalZ, time);

        anim.SetBool("advancing", true);

        anim.SetBool("mirror", Random.Range(1,3)==1);

        yield return new WaitForSeconds(time);

        reachedPlayer();
    }

    void tweenMove(float destination, float time)
    {
        advanceLt = LeanTween.value(transform.localPosition.z, destination, time).setOnUpdate(updateZ).id;
    }
    void updateZ(float value)
    {
        transform.localPosition = new Vector3(0,0,value);
    }

    void reachedPlayer()
    {
        reached=true;

        if(strafe)
        strafe.canStrafe=false;

        anim.SetBool("advancing", false);
    }

    public void pauseAdvance()
    {
        LeanTween.cancel(advanceLt);

        if(advanceRt!=null)
        StopCoroutine(advanceRt);
        
        anim.SetBool("advancing", false);
    }  
}
