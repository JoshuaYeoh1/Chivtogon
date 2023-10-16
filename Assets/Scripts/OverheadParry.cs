using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadParry : MonoBehaviour
{
    public Animator anim;

    [Header("Overhead")]
    public GameObject weaponHitbox;
    public bool canOverhead=true, windingUp, interrupted;
    public float windUpTime=.5f, swingCooldown=.5f, interruptTime=1;
    Coroutine overheadRt;

    [Header("Parry")]
    public bool canParry=true;
    public bool parrying;
    public float parryTime=0, protectTime=.4f, unparryTimeSlow=.4f;
    Coroutine parryRt;

    void Awake()
    {
        weaponHitbox.SetActive(false);
    }

    public void overhead()
    {
        if(canOverhead && !interrupted)
        overheadRt = StartCoroutine(overheading());
    }

    public IEnumerator overheading()
    {
        canOverhead=canParry=false;

        windingUp=true;

        anim.SetBool("swinging", true);
        anim.SetTrigger("swing");

        yield return new WaitForSeconds(windUpTime);

        windingUp=false;

        weaponHitbox.SetActive(true);

        yield return new WaitForSeconds(swingCooldown*.25f);

        weaponHitbox.SetActive(false);

        yield return new WaitForSeconds(swingCooldown*.75f);

        anim.SetBool("swinging", false);

        canOverhead=canParry=true;
    }

    public void cancelOverhead()
    {
        if(overheadRt!=null)
        StopCoroutine(overheadRt);

        weaponHitbox.SetActive(false);

        canOverhead=canParry=true;

        windingUp=false;

        anim.SetBool("swinging", false);
    }

    public void interrupt()
    {
        StartCoroutine(interrupting());
        cancelOverhead();
        cancelParry();
    }
    IEnumerator interrupting()
    {
        interrupted=true;

        yield return new WaitForSeconds(interruptTime);

        interrupted=false;
    }

    public void parry()
    {
        if(windingUp)
        {
            cancelOverhead();
        }
        else if(canParry)
        {
            parryRt = StartCoroutine(parryyy());
        }
    }

    public IEnumerator parryyy()
    {
        canOverhead=canParry=false;

        yield return new WaitForSeconds(parryTime);

        parrying=true;

        anim.SetTrigger("parry");

        anim.SetBool("parrying", true);

        anim.SetBool("mirror", Random.Range(1,3)==1);

        yield return new WaitForSeconds(protectTime);

        parrying=false;

        anim.SetBool("slowunparry", true);

        anim.SetBool("parrying", false);

        yield return new WaitForSeconds(unparryTimeSlow);

        cancelParry();
    }

    public void cancelParry()
    {
        if(parryRt!=null)
        StopCoroutine(parryRt);        

        canOverhead=canParry=true;

        parrying=false;

        anim.SetBool("slowunparry", false);

        anim.SetBool("parrying", false);
    }
}













//junk stuff

//public GameObject shield;
//shield.SetActive(false);
//LeanTween.rotateX(weaponHitbox, 0, 0);
//LeanTween.rotateX(weaponHitbox, weaponHitbox.transform.localEulerAngles.x+90, 0).setEaseInOutSine();
//shield.SetActive(true);
//LeanTween.rotateX(shield, 0, 0);
//LeanTween.rotateX(shield, weaponHitbox.transform.localEulerAngles.x-90, parryTime).setEaseOutExpo();
//LeanTween.rotateX(shield, 0, unparryTime).setEaseOutExpo();
//shield.SetActive(false);

// public bool animLayer;
// Coroutine animLayerRt;

// void disableAnimLayer()
// {
//     if(animLayerRt!=null)
//     StopCoroutine(animLayerRt);

//     animLayerRt = StartCoroutine(disablingAnimLayer());
// }

// IEnumerator disablingAnimLayer()
// {
//     yield return new WaitForSeconds(.4f);

//     animLayer=false;
// }