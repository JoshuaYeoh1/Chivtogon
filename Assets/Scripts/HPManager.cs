using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [HideInInspector] public OverheadParry ovPa;
    public Animator anim;

    public float hp, hpmax;

    void Awake()
    {
        ovPa=GetComponent<OverheadParry>();
    }    

    public void hit()
    {
        hp--;

        if(hp>0)
        {
            ovPa.interrupt();

            anim.SetTrigger("hit");
        }
        else
        {
            die();
        }
    }

    public void die()
    {
        gameObject.layer=0;

        if(tag=="Enemy")
        {
            anim.SetTrigger("death enemy");

            StartCoroutine(sinkAnim());
        }
        else
            anim.SetTrigger("death player");
    }

    IEnumerator sinkAnim()
    {
        yield return new WaitForSeconds(3f);

        LeanTween.moveLocalY(gameObject, transform.localPosition.y-1, 1).setEaseInOutSine();

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
