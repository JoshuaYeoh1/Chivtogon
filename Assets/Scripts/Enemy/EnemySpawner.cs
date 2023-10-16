using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    float spawnTimeMin=2, spawnTimeMax=4;
    public int maxEnemies=1, increaseEnemiesAfterNKills=10;

    void Start()
    {
        Singleton.instance.enemiesAlive=0;

        spawningRt = StartCoroutine(spawning());
    }

    Coroutine spawningRt;

    IEnumerator spawning()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(spawnTimeMin,spawnTimeMax));
            
            difficultyCheck();
            spawn();
        }
    }

    void difficultyCheck()
    {
        if(Singleton.instance.playerKills%increaseEnemiesAfterNKills==0 && Singleton.instance.playerKills/increaseEnemiesAfterNKills>0)
        maxEnemies = Singleton.instance.playerKills/increaseEnemiesAfterNKills;
    }
    
    void spawn()
    {
        if(Singleton.instance.enemiesAlive<maxEnemies && Singleton.instance.playerAlive)
        {
            Instantiate(enemy, Singleton.instance.player.transform.position, Quaternion.identity);

            Singleton.instance.enemiesAlive++;
        }
    }
}
