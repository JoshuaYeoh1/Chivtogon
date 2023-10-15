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
        ovPa.interrupt();

        anim.SetTrigger("hit");
    }
}
