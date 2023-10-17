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

    public SkinRandomizerScript weaponSkin;
    [HideInInspector] public string weaponType;
    public AudioClip[] sfxSwingBlunt, sfxSwingAxe, sfxSwingBlade;

    public AudioClip[] sfxWindUp;
    public void playWindup()
    {
        Singleton.instance.playSFX(sfxWindUp,transform);
    }

    void Start()
    {
        if(weaponSkin.skin==0) weaponType="axe";
        else if(weaponSkin.skin==1) weaponType="blunt";
        else if(weaponSkin.skin==2) weaponType="blade";
        else weaponType="axe";
    }

    public void playSwing()
    {
        switch(weaponType)
        {
            case "axe": Singleton.instance.playSFX(sfxSwingAxe,transform); break;
            case "blunt": Singleton.instance.playSFX(sfxSwingBlunt,transform); break;
            case "blade": Singleton.instance.playSFX(sfxSwingBlade,transform); break;
            default: Singleton.instance.playSFX(sfxSwingAxe,transform); break;
        }
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
