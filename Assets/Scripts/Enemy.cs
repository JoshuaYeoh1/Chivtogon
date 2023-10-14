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
    public float strafeIntervalMin=.35f, strafeIntervalMax=3, strafeTimeMin=.5f, strafeTimeMax=1;
    Coroutine strafeRt;

    IEnumerator strafing()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(strafeIntervalMin, strafeIntervalMax));

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
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer==9 && !reached)
        {
            triggering=false;

            advance();
        }
    }

    void Update()
    {
        if(triggering && !_other && !reached)
        {
            triggering=false;
            
            advance();
        }
    }
}
