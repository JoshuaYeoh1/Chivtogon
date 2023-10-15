using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadParry : MonoBehaviour
{
    public Animator anim;

    [Header("Overhead")]
    public GameObject weaponHitbox;
    public bool canOverhead=true, windingUp;
    public float windUpTime=.5f, swingCooldown=.5f, interruptTime=.75f;
    Coroutine overheadRt;

    [Header("Parry")]
    //public GameObject shield;
    public bool canParry=true;
    public bool parrying;
    public float parryTime=0, protectTime=.4f, unparryTime=.4f;
    Coroutine parryRt;

    void Awake()
    {
        weaponHitbox.SetActive(false);
        //shield.SetActive(false);
    }

    public void overhead()
    {
        if(canOverhead)
        overheadRt = StartCoroutine(overheading());
    }

    public IEnumerator overheading()
    {
        //LeanTween.rotateX(weaponHitbox, 0, 0);

        canOverhead=canParry=false;

        windingUp=true;

        anim.SetBool("swinging", true);

        yield return new WaitForSeconds(windUpTime);

        windingUp=false;

        weaponHitbox.SetActive(true);

        yield return new WaitForSeconds(swingCooldown*.25f);

        weaponHitbox.SetActive(false);

        //LeanTween.rotateX(weaponHitbox, weaponHitbox.transform.localEulerAngles.x+90, 0).setEaseInOutSine();

        yield return new WaitForSeconds(swingCooldown*.75f);

        //LeanTween.rotateX(weaponHitbox, 0, 0);

        anim.SetBool("swinging", false);

        canOverhead=canParry=true;
    }

    public void cancelOverhead()
    {
        if(overheadRt!=null)
        StopCoroutine(overheadRt);

        //LeanTween.rotateX(weaponHitbox, 0, 0);

        weaponHitbox.SetActive(false);

        canOverhead=canParry=true;

        windingUp=false;

        anim.SetBool("swinging", false);
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

        //shield.SetActive(true);
        //LeanTween.rotateX(shield, 0, 0);
        //LeanTween.rotateX(shield, weaponHitbox.transform.localEulerAngles.x-90, parryTime).setEaseOutExpo();

        yield return new WaitForSeconds(parryTime);

        parrying=true;

        anim.SetBool("parrying", true);

        yield return new WaitForSeconds(protectTime);

        parrying=false;

        anim.SetBool("parrying", false);

        //LeanTween.rotateX(shield, 0, unparryTime).setEaseOutExpo();

        yield return new WaitForSeconds(unparryTime);

        //shield.SetActive(false);

        canOverhead=canParry=true;
    }

    public void cancelParry()
    {
        if(parryRt!=null)
        StopCoroutine(parryRt);

        // LeanTween.rotateX(shield, 0, 0);
        //shield.SetActive(false);

        canOverhead=canParry=true;

        parrying=false;

        anim.SetBool("parrying", false);
    }

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
}
