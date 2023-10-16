using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    public float[] angles = {0,90,180,270};

    void Awake()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y+angles[Random.Range(0,angles.Length)], transform.localEulerAngles.z);
    }
}
