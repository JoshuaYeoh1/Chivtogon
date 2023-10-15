using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadParry : MonoBehaviour
{
    [Header("Overhead")]
    public GameObject weapon;
    public bool canOverhead=true, windingUp;
    public float windUpTime=.5f, swingCooldown=.5f, interruptTime=.75f;
    Coroutine overheadRt;

    public void overhead()
    {
        if(canOverhead)
        overheadRt = StartCoroutine(overheading());
    }

    public IEnumerator overheading()
    {
        weapon.SetActive(true);

        LeanTween.rotateX(weapon, 0, 0);

        canOverhead=canParry=false;

        windingUp=true;

        yield return new WaitForSeconds(windUpTime);

        windingUp=false;

        LeanTween.rotateX(weapon, weapon.transform.localEulerAngles.x+90, 0).setEaseInOutSine();

        yield return new WaitForSeconds(swingCooldown);

        LeanTween.rotateX(weapon, 0, 0);

        weapon.SetActive(false);

        canOverhead=canParry=true;
    }

    public void cancelOverhead()
    {
        if(overheadRt!=null)
        StopCoroutine(overheadRt);

        LeanTween.rotateX(weapon, 0, 0);

        weapon.SetActive(false);

        canOverhead=canParry=true;

        windingUp=false;
    }

    public void interrupt()
    {
        cancelOverhead();
        cancelParry();

        StartCoroutine(interrupted());
    }
    IEnumerator interrupted()
    {
        canOverhead=false;

        yield return new WaitForSeconds(interruptTime);

        canOverhead=true;
    }
    
    [Header("Parry")]
    public GameObject shield;
    public bool canParry=true, parrying;
    public float parryTime=0, protectTime=.4f, unparryTime=.5f;
    Coroutine parryRt;

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

        shield.SetActive(true);

        LeanTween.rotateX(shield, 0, 0);

        LeanTween.rotateX(shield, weapon.transform.localEulerAngles.x-90, parryTime).setEaseOutExpo();

        yield return new WaitForSeconds(parryTime);

        parrying=true;

        yield return new WaitForSeconds(protectTime);

        parrying=false;

        LeanTween.rotateX(shield, 0, unparryTime).setEaseOutExpo();

        yield return new WaitForSeconds(unparryTime);

        shield.SetActive(false);

        canOverhead=canParry=true;
    }

    public void cancelParry()
    {
        if(parryRt!=null)
        StopCoroutine(parryRt);

        LeanTween.rotateX(shield, 0, 0);

        shield.SetActive(false);

        canOverhead=canParry=true;

        parrying=false;
    }
}
