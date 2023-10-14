using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadParry : MonoBehaviour
{
    [Header("Overhead")]
    public GameObject weapon;
    public bool canOverhead=true, windingUp;
    public float windUpTime=.5f, swingCooldown=.5f;
    Coroutine overheadRt;

    public void overhead()
    {
        overheadRt = StartCoroutine(overheading());
    }

    public IEnumerator overheading()
    {
        weapon.SetActive(true);

        canOverhead=canParry=false;

        windingUp=true;

        yield return new WaitForSeconds(windUpTime);

        windingUp=false;

        LeanTween.rotateX(weapon, 0, 0);

        LeanTween.rotateX(weapon, weapon.transform.localEulerAngles.x+90, .1f).setEaseOutExpo();

        yield return new WaitForSeconds(swingCooldown);

        LeanTween.rotateX(weapon, 0, 0);

        weapon.SetActive(false);

        canOverhead=canParry=true;
    }

    public void cancelOverhead()
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
    public float parryTime=.1f, protectTime=.4f, unparryTime=.1f;
    Coroutine parryRt;

    public void parry()
    {
        parryRt = StartCoroutine(parryyy());
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

        shield.SetActive(false);

        canOverhead=canParry=true;

        parrying=false;
    }
}
