using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door;
    public float defLocalZ, animTime=.5f;
    int doorLt=0, count=0;

    public AudioClip[] sfxOpen, sfxClose;

    void Awake()
    {
        defLocalZ = door.transform.localEulerAngles.z;
    }

    void OnTriggerEnter(Collider other)
    {
        if(count==0)
        {
            LeanTween.cancel(doorLt);
            doorLt = LeanTween.rotateLocal(door, new Vector3(door.transform.localEulerAngles.x,door.transform.localEulerAngles.y,defLocalZ-90), animTime).setEaseInOutSine().id;

            Singleton.instance.playSFX(sfxOpen,transform);
        }
        count++;
    }
    void OnTriggerExit(Collider other)
    {
        count--;
        if(count==0)
        {
            LeanTween.cancel(doorLt);
            doorLt = LeanTween.rotateLocal(door, new Vector3(door.transform.localEulerAngles.x,door.transform.localEulerAngles.y,defLocalZ+90), animTime*2).setEaseInOutSine().id;

            Singleton.instance.playSFX(sfxClose,transform);
        }
    }
}
