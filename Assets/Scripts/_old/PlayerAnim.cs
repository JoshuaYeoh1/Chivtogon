using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;
    public Player player;
    public OverheadParry ovPa;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // anim.SetBool("turningleft", player.turningleft);
        // anim.SetBool("turningright", player.turningright);

        // if(player.hitAnim)
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

        // if(player.hurtLayer)
        //     anim.SetLayerWeight(2,1);
        // else
        //     anim.SetLayerWeight(2,0);
    }
}
