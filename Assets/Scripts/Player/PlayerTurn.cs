using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public Animator anim;

    public bool canTurn=true;
    public float turnTime=.2f;

    void Update()
    {
        if(canTurn)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                turnRt = StartCoroutine(turn(-1));
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                turnRt = StartCoroutine(turn(1));
            }
        }
    }

    Coroutine turnRt;

    IEnumerator turn(int dir)
    {
        canTurn=false;

        if(dir<0)
            anim.SetTrigger("turnleft");
        else
            anim.SetTrigger("turnright");

        LeanTween.rotateY(gameObject, transform.eulerAngles.y + dir*45, turnTime).setEaseInOutSine();

        yield return new WaitForSeconds(turnTime);

        canTurn=true;
    }
}
