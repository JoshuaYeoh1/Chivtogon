using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    OverheadParry ovPa;

    void Awake()
    {
        ovPa=GetComponent<OverheadParry>();
    }

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

        if(Input.GetKeyDown(KeyCode.S) && ovPa.canOverhead)
        {
            ovPa.overhead();
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            if(ovPa.windingUp)
            {
                ovPa.cancelOverhead();
            }
            else if(ovPa.canParry)
            {
                ovPa.parry();
            }
        }
    }

    [Header("Turning")]
    public bool canTurn=true;
    public float turnTime=.2f;
    Coroutine turnRt;

    IEnumerator turn(int dir)
    {
        canTurn=false;

        LeanTween.rotateY(gameObject, transform.eulerAngles.y + dir*45, turnTime).setEaseOutExpo();

        yield return new WaitForSeconds(turnTime);

        canTurn=true;
    }
}
