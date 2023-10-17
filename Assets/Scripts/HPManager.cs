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

    [HideInInspector] public string enemyWeaponType;

    public AudioClip[] sfxHurtBlunt, sfxHurtAxe, sfxHurtBlade;
    public AudioClip[] sfxDieBlunt, sfxDieAxe, sfxDieBlade;
    public AudioClip[] sfxSubwoofer;

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

                switch(enemyWeaponType)
                {
                    case "axe": Singleton.instance.playSFX(sfxHurtAxe,transform); break;
                    case "blunt": Singleton.instance.playSFX(sfxHurtBlunt,transform); break;
                    case "blade": Singleton.instance.playSFX(sfxHurtBlade,transform); break;
                    default: Singleton.instance.playSFX(sfxHurtAxe,transform); break;
                }

                if(tag=="Player")
                {
                    if(hp>hpmax*.5f) player.voice.hurtLow(player.voicetype);
                    else if(hp<=hpmax*.5f && hp>hpmax*.25f) player.voice.HurtMed(player.voicetype);
                    else if(hp<=hpmax*.25f) player.voice.hurtHigh(player.voicetype);
                }
                else
                {
                    if(Random.Range(1,4)==1) enemy.voice.hurtLow(enemy.voicetype);
                    else if(Random.Range(1,3)==1) enemy.voice.HurtMed(enemy.voicetype);
                    else enemy.voice.hurtHigh(enemy.voicetype);
                }
            }
            else
            {
                hp=0;

                updateBarFill();

                switch(enemyWeaponType)
                {
                    case "axe": Singleton.instance.playSFX(sfxDieAxe,transform); break;
                    case "blunt": Singleton.instance.playSFX(sfxDieBlunt,transform); break;
                    case "blade": Singleton.instance.playSFX(sfxDieBlade,transform); break;
                    default: Singleton.instance.playSFX(sfxDieAxe,transform); break;
                }

                if(tag=="Player")
                    player.voice.death(player.voicetype);
                else
                    enemy.voice.death(enemy.voicetype);

                die();
            }

            if(tag=="Player") Singleton.instance.playSFX(sfxSubwoofer,transform);
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
