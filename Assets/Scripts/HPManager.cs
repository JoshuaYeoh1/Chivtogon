using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    Player player;
    Enemy enemy;
    //[HideInInspector] public OverheadParry ovPa;
    //public Animator anim;
    public bool iframe;
    public float hp, hpmax, iframeTime=.5f;

    void Awake()
    {
        if(tag=="Player")
            player=GetComponent<Player>();
        else
            enemy=GetComponent<Enemy>();

        //ovPa=GetComponent<OverheadParry>();

        hp=hpmax;
    }    

    public void hit()
    {
        if(!iframe)
        {
            StartCoroutine(iframing());
            
            hp--;

            if(hp>0)
            {
                if(tag=="Player")
                    player.hit();
                else
                    enemy.hit();
            }
            else
            {
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
}
