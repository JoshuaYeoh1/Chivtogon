using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTimeMin=1, spawnTimeMax=4;
    Coroutine rt1;

    void Awake()
    {
        rt1 = StartCoroutine(spawning());
    }

    IEnumerator spawning()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(spawnTimeMin,spawnTimeMax));

            Instantiate(enemy, Singleton.instance.playerPos, Quaternion.identity);
        }
    }

    public void stopSpawning()
    {
        StopCoroutine(rt1);
    }
}
