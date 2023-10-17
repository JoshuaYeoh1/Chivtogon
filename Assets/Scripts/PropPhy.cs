using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPhy : MonoBehaviour
{
    bool canPlaySfx;
    public AudioClip[] sfxWoodPhy;

    void Start()
    {
        Invoke("canPlayNow", 5);
    }

    void canPlayNow()
    {
        canPlaySfx=true;
    }

    void OnCollisionEnter(Collision other)
    {
        if(canPlaySfx)
        Singleton.instance.playSFX(sfxWoodPhy,transform);
    }
}
