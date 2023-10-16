using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleLight : MonoBehaviour
{
    Light mylight;
    public float frequency, magnitude, duration;
    public float defintensity, intensity;
    public bool wiggle=true;
    float seed;

    void Awake()
    {
        mylight = GetComponent<Light>();

        defintensity = mylight.intensity;

        seed = Random.value;
    }

    void LateUpdate()
    {
        if(wiggle)
        {
            intensity = (Mathf.PerlinNoise(seed,Time.time*frequency)*2-1)*magnitude;

            mylight.intensity = intensity;
        }
    }

    Coroutine rt1;

    public void shake()
    {
        if(rt1!=null)
        StopCoroutine(rt1);
        
        rt1 = StartCoroutine(shaker());
    }

    IEnumerator shaker()
    {
        mylight.intensity = defintensity;

        wiggle=true;

        yield return new WaitForSeconds(duration);

        wiggle=false;

        mylight.intensity = defintensity;
    }
}
