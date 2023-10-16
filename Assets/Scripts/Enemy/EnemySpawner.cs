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
        if(Singleton.instance.playerKills<increaseEnemiesAfterNKills) maxEnemies=1;
        else if(Singleton.instance.playerKills>=increaseEnemiesAfterNKills && Singleton.instance.playerKills<increaseEnemiesAfterNKills*2) maxEnemies=2;
        else if(Singleton.instance.playerKills>=increaseEnemiesAfterNKills*2 && Singleton.instance.playerKills<increaseEnemiesAfterNKills*3) maxEnemies=3;
        else if(Singleton.instance.playerKills>=increaseEnemiesAfterNKills*3 && Singleton.instance.playerKills<increaseEnemiesAfterNKills*4) maxEnemies=4;
        else if(Singleton.instance.playerKills>=increaseEnemiesAfterNKills*4 && Singleton.instance.playerKills<increaseEnemiesAfterNKills*5) maxEnemies=5;
        else if(Singleton.instance.playerKills>=increaseEnemiesAfterNKills*5 && Singleton.instance.playerKills<increaseEnemiesAfterNKills*6) maxEnemies=6;
        else if(Singleton.instance.playerKills>=increaseEnemiesAfterNKills*6 && Singleton.instance.playerKills<increaseEnemiesAfterNKills*7) maxEnemies=7;
        else if(Singleton.instance.playerKills>=increaseEnemiesAfterNKills*7 && Singleton.instance.playerKills<increaseEnemiesAfterNKills*8) maxEnemies=8;
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
