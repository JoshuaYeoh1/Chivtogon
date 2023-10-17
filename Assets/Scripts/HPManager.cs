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

    void Awake()
    {
        if(tag=="Player")
            player=GetComponent<Player>();
        else
            enemy=GetComponent<Enemy>();

        //ovPa=GetComponent<OverheadParry>();

        hp=hpmax;

        StartCoroutine(hpregen());
    }    

    public void hit(float dmg)
    {
        if(!iframe)
        {
            hp-=dmg;

            if(hp>dmg)
            {
                StartCoroutine(iframing());

                if(tag=="Player")
                    player.hit();
                else
                    enemy.hit();
            }
            else
            {
                hp=0;
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
            if(hp>0 && hp<=hpmax-regenAmount && regen)
            {   
                yield return new WaitForSeconds(regenTime);
                hp+=regenAmount;
            }
        }
    }
}
