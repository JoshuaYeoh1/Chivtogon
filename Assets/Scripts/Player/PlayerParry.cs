using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    Player player;
    HPManager hp;
    OverheadParry ovPa;
    public PlayerParryBox ppb;

    public AudioClip[] sfxParryBlunt, sfxParryAxe, sfxParryBlade;

    void Awake()
    {
        player = GetComponent<Player>();
        hp = GetComponent<HPManager>();
        ovPa = GetComponent<OverheadParry>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            parry();
        }
    }

    public void parry()
    {
        if(Singleton.instance.playerAlive && Singleton.instance.controlsEnabled && Singleton.instance.doneTutorial2)
        {
            ovPa.parry();

            Singleton.instance.swipeUpCount++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==10) //touch enemy weapon
        {
            if(ovPa.parrying && ppb.parryableTargets.Contains(other.GetComponent<EnemyWeapon>().parent))
            {
                ovPa.cancelParry();

                other.GetComponent<EnemyWeapon>().ovPa.interrupt();

                player.vfxSpark.Play();

                switch(player.weaponType)
                {
                    case "axe": Singleton.instance.playSFX(sfxParryAxe,transform); break;
                    case "blunt": Singleton.instance.playSFX(sfxParryBlunt,transform); break;
                    case "blade": Singleton.instance.playSFX(sfxParryBlade,transform); break;
                    default: Singleton.instance.playSFX(sfxParryAxe,transform); break;
                }
            }
            else
            {
                hp.enemyWeaponType = other.GetComponent<EnemyWeapon>().parent.GetComponent<Enemy>().weaponType;

                if(ppb.parryableTargets.Contains(other.GetComponent<EnemyWeapon>().parent))
                {
                    hp.hit(10);
                }
                else
                {
                    hp.hit(5,false);
                }
            }
        }
    }
}
