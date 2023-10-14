using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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

        if(Input.GetKeyDown(KeyCode.S) && canOverhead)
        {
            overheadRt = StartCoroutine(overhead());
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            if(windingUp)
            {
                cancelOverhead();
            }
            else if(canParry)
            {
                parryRt = StartCoroutine(parry());
            }
        }
    }

    [Header("Turning")]
    public bool canTurn=true;
    public float turnDeg=45;
    public float turnTime=.5f, turnCooldown=0;
    Coroutine turnRt;

    IEnumerator turn(int dir)
    {
        canTurn=false;

        LeanTween.rotateY(gameObject, transform.eulerAngles.y + dir*turnDeg, turnTime).setEaseOutExpo();

        yield return new WaitForSeconds(turnTime);

        yield return new WaitForSeconds(turnCooldown);

        canTurn=true;
    }

    [Header("Overhead")]
    public GameObject weapon;
    public bool canOverhead=true, windingUp;
    public float windUpTime=.5f, swingCooldown=.5f;
    Coroutine overheadRt;

    IEnumerator overhead()
    {
        weapon.SetActive(true);

        canOverhead=canParry=false;

        windingUp=true;

        yield return new WaitForSeconds(windUpTime);

        windingUp=false;

        LeanTween.rotateX(weapon, 0, 0);

        LeanTween.rotateX(weapon, 90, .1f).setEaseOutExpo();

        yield return new WaitForSeconds(swingCooldown);

        LeanTween.rotateX(weapon, 0, 0);

        weapon.SetActive(false);

        canOverhead=canParry=true;
    }

    void cancelOverhead()
    {
        if(overheadRt!=null)
        StopCoroutine(overheadRt);

        weapon.SetActive(false);

        canOverhead=canParry=true;

        windingUp=false;
    }
    
    [Header("Parry")]
    public GameObject shield;
    public bool canParry=true, parrying;
    public float parryTime=.1f, protectTime=.3f, unparryTime=.5f;
    Coroutine parryRt;

    IEnumerator parry()
    {
        canOverhead=canParry=false;

        shield.SetActive(true);

        LeanTween.rotateX(shield, 0, 0);

        LeanTween.rotateX(shield, -90, parryTime).setEaseOutExpo();

        yield return new WaitForSeconds(parryTime);

        parrying=true;

        yield return new WaitForSeconds(protectTime);

        parrying=false;

        LeanTween.rotateX(shield, 0, unparryTime).setEaseOutExpo();

        yield return new WaitForSeconds(unparryTime);

        shield.SetActive(false);

        canOverhead=canParry=true;
    }

    void cancelParry()
    {
        if(parryRt!=null)
        StopCoroutine(parryRt);

        shield.SetActive(false);

        canOverhead=canParry=true;

        parrying=false;
    }
}
