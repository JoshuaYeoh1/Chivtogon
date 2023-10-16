using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    HPManager hp;
    OverheadParry ovPa;
    public PlayerParryBox ppb;

    void Awake()
    {
        hp = GetComponent<HPManager>();
        ovPa = GetComponent<OverheadParry>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && Singleton.instance.playerAlive && Singleton.instance.controlsEnabled)
        {
            ovPa.parry();
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
            }
            else
            {
                hp.hit();
            }
        }
    }
}
