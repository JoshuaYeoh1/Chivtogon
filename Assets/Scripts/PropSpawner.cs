using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public GameObject[] props;
    public float maxDistance=4.5f, yOffset=.05f;
    public int minCount=8, maxCount=16;

    void Awake()
    {
        for(int i=0;i<Random.Range(minCount,maxCount+1);i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-maxDistance, maxDistance), yOffset, Random.Range(-maxDistance, maxDistance));
            
            Instantiate(props[Random.Range(0,props.Length)], spawnPos, Quaternion.identity);
        }
    }
}
