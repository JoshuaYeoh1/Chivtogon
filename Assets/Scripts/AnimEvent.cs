using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public AudioClip[] footstep;

    public void playFootstep()
    {
        Singleton.instance.playSFX(footstep,transform);
    }

    // public OverheadParry ovPa;

    // public void enableWeaponHitbox()
    // {
    //     ovPa.enableWeaponHitbox();
    // }
    // public void disableWeaponHitbox()
    // {
    //     ovPa.disableWeaponHitbox();
    // }
}
