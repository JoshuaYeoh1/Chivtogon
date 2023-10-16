using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door;
    public float defLocalZ, animTime=.5f;
    int doorLt=0, count=0;

    void Awake()
    {
        defLocalZ = door.transform.localEulerAngles.z;
    }

    void OnTriggerEnter(Collider other)
    {
        if(count==0)
        {
            LeanTween.cancel(doorLt);
            doorLt = LeanTween.rotateLocal(door, new Vector3(door.transform.localEulerAngles.x,door.transform.localEulerAngles.y,-90), animTime).setEaseInOutSine().id;
        }
        count++;
    }
    void OnTriggerExit(Collider other)
    {
        count--;
        if(count==0)
        {
            LeanTween.cancel(doorLt);
            doorLt = LeanTween.rotateLocal(door, new Vector3(door.transform.localEulerAngles.x,door.transform.localEulerAngles.y,90), animTime).setEaseInOutSine().id;
        }
    }
}
