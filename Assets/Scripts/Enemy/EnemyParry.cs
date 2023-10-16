using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParry : MonoBehaviour
{
    Enemy enemy;
    Player player;
    HPManager hp;
    [HideInInspector] public OverheadParry ovPa;

    public bool facedByPlayer;
    public bool canParry=true;
    public float parryChance=.5f, feintToParryTime=.1f;

    void Start()
    {
        enemy=GetComponent<Enemy>();
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        hp=GetComponent<HPManager>();
        ovPa=GetComponent<OverheadParry>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==6 && other.tag=="FaceBox") //touch player facebox
        {
            facedByPlayer=true;
        }

        if(other.gameObject.layer==7 && facedByPlayer && !enemy.dead) //touch player weapon
        {
            if(ovPa.parrying)
            {
                ovPa.cancelParry();

                player.ovPa.interrupt();
            }
            else
            {
                hp.hit();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer==6) //exit player facebox
        {
            facedByPlayer=false;
        }
    }

    void Update()
    {
        if(facedByPlayer && player.ovPa.windingUp && canParry && !enemy.dead && Singleton.instance.playerAlive)
        {
            canParry=false;
            parry();
        }
    }

    void parry()
    {
        if(!enemy.dead)
        {
            StartCoroutine(parrying());
            StartCoroutine(parryCooldown());
        }
    }

    IEnumerator parrying()
    {
        if(Random.Range(0f,1f) <= parryChance && !enemy.dead)
        {
            yield return new WaitForSeconds(player.ovPa.windUpTime * Random.Range(.45f,.9f));

            if(ovPa.windingUp)
            {
                StartCoroutine(feintToParry());
            }
            else
            {
                ovPa.parry();
            }
        }
    }

    IEnumerator feintToParry()
    {
        ovPa.cancelOverhead();

        yield return new WaitForSeconds(feintToParryTime);

        ovPa.parry();
    }

    IEnumerator parryCooldown()
    {
        yield return new WaitForSeconds(player.ovPa.windUpTime);

        canParry=true;
    }
}
