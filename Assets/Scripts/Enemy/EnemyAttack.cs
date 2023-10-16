using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Enemy enemy;
    EnemyAdvance advance;
    [HideInInspector] public OverheadParry ovPa;

    public float atkIntervalMin=.5f, atkIntervalMax=2, checkParryChance=.05f;
    public float feintChance=.2f, feintIntervalMin=.2f, feintIntervalMax=.45f;

    void Start()
    {
        enemy=GetComponent<Enemy>();
        advance=GetComponent<EnemyAdvance>();
        ovPa=GetComponent<OverheadParry>();

        StartCoroutine(attacking());
    }
    
    IEnumerator attacking()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(atkIntervalMin,atkIntervalMax));

            if(advance.reached && !enemy.dead && Singleton.instance.playerAlive)
            {
                randomMove();
            }
        }
    }

    void randomMove()
    {
        if(Random.Range(0f,1f) <= checkParryChance)
        {
            ovPa.parry();
        }
        else
        {
            ovPa.overhead();

            if(Random.Range(0f,1f) <= feintChance)
            {
                StartCoroutine(feinting());
            }
        }
    }

    IEnumerator feinting()
    {
        yield return new WaitForSeconds(Random.Range(feintIntervalMin,feintIntervalMax));

        ovPa.parry();
    }
}
