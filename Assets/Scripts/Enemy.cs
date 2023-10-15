using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject pivot;
    Vector3 startPos;
    float[] surroundAngles={0,45,90,135,180,-45,-90,-135};

    [Header("Advancing")]
    public bool reached;
    public float goalZMin=1.75f, goalZMax=2.25f, travelTimeMin=4, travelTimeMax=6;
    int advanceLt=0;
    Coroutine advanceRt;

    void Awake()
    {
        pivot = transform.parent.gameObject;
        startPos = transform.localPosition;

        pivot.transform.localEulerAngles = new Vector3(pivot.transform.localEulerAngles.x, surroundAngles[Random.Range(0,surroundAngles.Length)], transform.localEulerAngles.z);
        
        advance();
        StartCoroutine(strafing());
    }

    public void advance()
    {
        advanceRt = StartCoroutine(advancing());
    }

    IEnumerator advancing()
    {
        float goalZ = Random.Range(goalZMin,goalZMax);
        float travelTime = Random.Range(travelTimeMin,travelTimeMax);

        float time = travelTime*(transform.localPosition.z-goalZ)/(startPos.z-goalZ);

        advanceLt = LeanTween.moveLocalZ(gameObject, goalZ, time).id;

        yield return new WaitForSeconds(time);

        reached=true;

        canStrafe=false;
    }

    public void stopAdvance()
    {
        LeanTween.cancel(advanceLt);

        if(advanceRt!=null)
        StopCoroutine(advanceRt);
        
        if(strafeRt!=null)
        StopCoroutine(strafeRt);
    }

    [Header("Strafing")]
    public bool canStrafe=true;
    public float strafeTimeMin=.5f, strafeTimeMax=1, strafeIntervalMax=3;
    Coroutine strafeRt;

    IEnumerator strafing()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(strafeTimeMin, strafeIntervalMax));

            if(canStrafe && !reached)
            {
                strafe();
            }
        }
    }

    public void strafe()
    {
        strafeRt = StartCoroutine(strafeee());
    }

    IEnumerator strafeee()
    {
        stopAdvance();

        canStrafe=false;

        int dir;

        if(Random.Range(1,3)==1)
            dir=1;
        else
            dir=-1;

        float time = Random.Range(strafeTimeMin, strafeTimeMax);
        
        LeanTween.rotateY(pivot, pivot.transform.localEulerAngles.y + 45*dir, time).setEaseInOutSine();

        yield return new WaitForSeconds(time);

        if(!reached)
        {
            canStrafe=true;
            advance();
        }
    }

    Collider _other;
    bool triggering;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer==9)
        {
            triggering=true;
            _other = other;

            stopAdvance();
            LeanTween.moveLocalZ(gameObject, transform.localPosition.z+.01f, 0);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==6)
        {
            facedByPlayer=true;
        }

        if(other.gameObject.layer==7)
        {
            if(ovPa.parrying)
            {
                ovPa.cancelParry();

                player.ovPa.interrupt();
            }
            else
            {
                hit();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer==9 && !reached)
        {
            triggering=false;

            advance();
        }

        if(other.gameObject.layer==6)
        {
            facedByPlayer=false;
        }
    }

    void LateUpdate()
    {
        if(triggering && !_other && !reached)
        {
            triggering=false;

            advance();
        }
    }

    Player player;
    OverheadParry ovPa;

    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ovPa=GetComponent<OverheadParry>();

        StartCoroutine(attacking());
    }

    [Header("Attack")]
    public float atkIntervalMin=.5f;
    public float atkIntervalMax=2, checkParryChance=.05f;
    public float feintChance=.3f, feintIntervalMin=.2f, feintIntervalMax=.45f;

    IEnumerator attacking()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(atkIntervalMin,atkIntervalMax));

            if(reached)
            {
                if(Random.Range(0f,1f) <= checkParryChance)
                {
                    ovPa.parry();
                }
                else
                {
                    ovPa.overhead();

                    if(Random.Range(0f,1f) <= feintChance)
                    {
                        yield return new WaitForSeconds(Random.Range(feintIntervalMin,feintIntervalMax));

                        ovPa.parry();
                    }
                }
            }
        }
    }

    [Header("Defend")]
    public bool facedByPlayer;
    public bool canParry=true;
    public float parryChance=.5f, parryIntervalMin=.3f, parryIntervalMax=.5f, feintToParryTime=.1f;

    void Update()
    {
        if(facedByPlayer && player.ovPa.windingUp && canParry)
        {
            canParry=false;
            StartCoroutine(parrying());
            StartCoroutine(parryCooldown());
        }
    }

    IEnumerator parrying()
    {
        if(Random.Range(0f,1f) <= parryChance)
        {
            yield return new WaitForSeconds(Random.Range(parryIntervalMin,parryIntervalMax));

            if(!ovPa.windingUp)
            {
                ovPa.parry();
            }
            else
            {
                ovPa.cancelOverhead();

                yield return new WaitForSeconds(feintToParryTime);

                ovPa.parry();
            }

            yield return new WaitForSeconds(Random.Range(parryIntervalMin,parryIntervalMax));
        }
    }

    IEnumerator parryCooldown()
    {
        yield return new WaitForSeconds(player.ovPa.windUpTime);

        canParry=true;
    }

    public void hit()
    {
        ovPa.interrupt();
    }
}
