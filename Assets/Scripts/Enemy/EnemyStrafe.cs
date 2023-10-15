using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrafe : MonoBehaviour
{
    EnemyAdvance advance;
    GameObject pivot;
    public Animator anim;

    public bool canStrafe=true;
    public float strafeTimeMin=.5f, strafeTimeMax=1, strafeIntervalMax=3;
    float[] surroundAngles={0,45,90,135,180,-45,-90,-135};

    void Awake()
    {
        advance=GetComponent<EnemyAdvance>();
        pivot = transform.parent.gameObject;
        
        pivot.transform.localEulerAngles = new Vector3(pivot.transform.localEulerAngles.x, surroundAngles[Random.Range(0,surroundAngles.Length)], transform.localEulerAngles.z);

        StartCoroutine(strafing());
    }

    IEnumerator strafing()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(strafeTimeMin, strafeIntervalMax));

            if(canStrafe && !advance.reached)
            {
                strafe();
            }
        }
    }

    public void strafe()
    {
        strafeRt = StartCoroutine(strafeee());
    }

    Coroutine strafeRt;

    IEnumerator strafeee()
    {
        advance.stopAdvance();

        canStrafe=false;

        anim.SetTrigger("strafe");

        float time = Random.Range(strafeTimeMin, strafeTimeMax);
        
        LeanTween.rotateY(pivot, pivot.transform.localEulerAngles.y + 45*randomDir(), time).setEaseInOutSine();

        yield return new WaitForSeconds(time);

        if(!advance.reached)
        {
            canStrafe=true;
            advance.advance();
        }
    }

    public void stopStrafe()
    {
        if(strafeRt!=null)
        StopCoroutine(strafeRt);
    }

    int randomDir()
    {
        if(Random.Range(1,3)==1)
            return 1;
        else
            return -1;
    }
}
