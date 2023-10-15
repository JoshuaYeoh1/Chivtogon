using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    Animator anim;
    public Enemy enemy;
    public OverheadParry ovPa;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // anim.SetBool("advancing", enemy.advancingAnim);
        // anim.SetBool("strafing", enemy.strafingAnim);

        // if(enemy.hitAnim)
        // anim.SetTrigger("hit");
        
        // anim.SetBool("swinging", ovPa.swinging);
        // anim.SetBool("parrying", ovPa.parrying);

        // layerWeight();
    }

    void layerWeight()
    {
        // if(ovPa.animLayer)
        //     anim.SetLayerWeight(1,1);
        // else
        //     anim.SetLayerWeight(1,0);

        // if(enemy.hurtLayer)
        //     anim.SetLayerWeight(2,1);
        // else
        //     anim.SetLayerWeight(2,0);
    }
}
