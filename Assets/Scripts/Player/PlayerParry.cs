using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    Player player;
    HPManager hp;
    OverheadParry ovPa;
    public PlayerParryBox ppb;

    void Awake()
    {
        player = GetComponent<Player>();
        hp = GetComponent<HPManager>();
        ovPa = GetComponent<OverheadParry>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && Singleton.instance.controlsEnabled)
        {
            parry();
        }
    }

    public void parry()
    {
        if(Singleton.instance.playerAlive)
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
            }
            else
            {
                hp.hit(10);
            }
        }
    }
}
