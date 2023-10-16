using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrafe : MonoBehaviour
{
    Enemy enemy;
    EnemyAdvance advance;
    GameObject parent;
    public Animator anim;

    public bool canStrafe=true;
    public float strafeTimeMin=.75f, strafeTimeMax=1, strafeIntervalMin=1, strafeIntervalMax=7;
    float[] surroundAngles={0,45,90,135,180,-45,-90,-135};

    void Start()
    {
        enemy=GetComponent<Enemy>();
        advance=GetComponent<EnemyAdvance>();
        parent=transform.parent.gameObject;

        LeanTween.rotateY(parent, surroundAngles[Random.Range(0,surroundAngles.Length)], 0);

        StartCoroutine(strafing());
    }

    IEnumerator strafing()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(strafeIntervalMin, strafeIntervalMax));

            if(canStrafe && !advance.reached && !enemy.dead && Singleton.instance.playerAlive)
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
        canStrafe=false;

        anim.SetTrigger("strafe");

        anim.SetBool("mirror", Random.Range(1,3)==1);

        advance.pauseAdvance();
        
        float time = Random.Range(strafeTimeMin,strafeTimeMax);

        LeanTween.rotateY(parent, parent.transform.localEulerAngles.y + 45*randomDir(), time).setEaseInOutSine();

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
