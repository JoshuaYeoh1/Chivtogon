using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public Animator anim;

    public bool canTurn=true;
    public float turnTime=.2f;

    public AudioClip[] sfxWindUp;

    void Update()
    {
        if(Singleton.instance.controlsEnabled)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                turnleft();
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                turnright();
            }
        }
    }

    public void turnleft()
    {
        if(canTurn && Singleton.instance.playerAlive)
        {
            turnRt = StartCoroutine(turn(-1));

            Singleton.instance.swipeLeftCount++;
        }
    }

    public void turnright()
    {
        if(canTurn && Singleton.instance.playerAlive)
        {
            turnRt = StartCoroutine(turn(1));

            Singleton.instance.swipeRightCount++;
        }
    }

    Coroutine turnRt;

    IEnumerator turn(int dir)
    {
        canTurn=false;

        Singleton.instance.playSFX(sfxWindUp,transform);

        if(dir<0)
            anim.SetTrigger("turnleft");
        else
            anim.SetTrigger("turnright");

        LeanTween.rotateY(gameObject, transform.eulerAngles.y + dir*45, turnTime).setEaseInOutSine();

        yield return new WaitForSeconds(turnTime);

        canTurn=true;
    }
}
