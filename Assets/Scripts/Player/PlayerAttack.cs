using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    OverheadParry ovPa;

    void Awake()
    {
        ovPa = GetComponent<OverheadParry>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            attack();
        }
    }

    public void attack()
    {
        if(Singleton.instance.playerAlive && Singleton.instance.controlsEnabled && Singleton.instance.doneTutorial1)
        {
            ovPa.overhead();

            Singleton.instance.swipeDownCount++;
        }
    }
}
