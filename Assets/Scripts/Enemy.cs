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

    [Header("Strafing")]
    public bool canStrafe=true;
    public float strafeIntervalMin=.5f, strafeIntervalMax=3, strafeTime=.5f;

    void Awake()
    {
        pivot = transform.parent.gameObject;
        startPos = transform.localPosition;

        pivot.transform.localEulerAngles = new Vector3(pivot.transform.localEulerAngles.x, surroundAngles[Random.Range(0,surroundAngles.Length)], transform.localEulerAngles.z);
        
        advance();
        StartCoroutine(strafing());
    }

    void advance()
    {
        StartCoroutine(advancing());
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

        Destroy(pivot);
    }

    void stopAdvance()
    {
        LeanTween.cancel(advanceLt);

        if(advanceRt!=null)
        StopCoroutine(advanceRt);
    }

    IEnumerator strafing()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(strafeIntervalMin, strafeIntervalMax));

            if(canStrafe && !reached)
            {
                StartCoroutine(strafeee());
            }
        }
    }

    public void strafe()
    {
        StartCoroutine(strafeee());
    }

    IEnumerator strafeee()
    {
        stopAdvance();

        int dir;

        if(Random.Range(1,3)==1)
            dir=1;
        else
            dir=-1;

        LeanTween.rotateY(pivot, pivot.transform.localEulerAngles.y + 45*dir, strafeTime).setEaseInOutSine();

        yield return new WaitForSeconds(strafeTime);

        advance();
    }
}
