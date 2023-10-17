using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    Player player;
    Enemy enemy;
    //[HideInInspector] public OverheadParry ovPa;
    //public Animator anim;
    public bool iframe, regen;
    public float hp, hpmax, iframeTime=.5f, regenAmount=1, regenTime=1;

    public GameObject barFill;

    void Awake()
    {
        if(tag=="Player")
            player=GetComponent<Player>();
        else
            enemy=GetComponent<Enemy>();

        //ovPa=GetComponent<OverheadParry>();

        hp=hpmax;

        if(regen) StartCoroutine(hpregen());
    }    

    public void hit(float dmg)
    {
        if(!iframe)
        {
            if(hp>dmg)
            {
                hp-=dmg;

                updateBarFill();

                StartCoroutine(iframing());

                if(tag=="Player")
                    player.hit();
                else
                    enemy.hit();
            }
            else
            {
                hp=0;
                updateBarFill();
                die();
            }
        }
    }

    public void die()
    {
        gameObject.layer=0;

        if(tag=="Player")
            player.die();
        else
            enemy.die();
    }

    IEnumerator iframing()
    {
        iframe=true;

        yield return new WaitForSeconds(iframeTime);

        iframe=false;
    }
    
    IEnumerator hpregen()
    {
        while(true)
        {
            yield return new WaitForSeconds(regenTime);

            if(hp>0 && hp<=hpmax-regenAmount && regen)
            {   
                hp+=regenAmount;
                updateBarFill();
            }
        }
    }

    int barfillLt=0;

    public void updateBarFill()
    {
        if(barFill)
        {
            LeanTween.cancel(barfillLt);

            barfillLt = LeanTween.scaleX(barFill, hp/hpmax, .2f).setEaseOutSine().id;
        }
    }

    void Update()
    {
        if(hp>hpmax) hp=hpmax;
        else if(hp<0) hp=0;
    }
}
