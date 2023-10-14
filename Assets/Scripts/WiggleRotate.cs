using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleRotate : MonoBehaviour
{
    public float frequency, magnitude, duration;
    float angleX, angleY;
    public bool wiggle;
    float seed;

    void Start()
    {
        seed = Random.value;
    }

    void LateUpdate()
    {
        if(wiggle)
        {
            angleX = Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1;
            angleY = Mathf.PerlinNoise(seed+1, Time.time * frequency) * 2 - 1;

            transform.localEulerAngles = new Vector3(angleX,angleY,0) * magnitude;
        }
    }

    public void shake()
    {
        StartCoroutine(shaker());
    }

    IEnumerator shaker()
    {
        transform.localEulerAngles = Vector3.zero;

        wiggle=true;

        yield return new WaitForSeconds(duration);

        wiggle=false;

        transform.localEulerAngles = Vector3.zero;
    }
}
