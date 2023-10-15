using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParry : MonoBehaviour
{
    Player player;
    HPManager hp;
    [HideInInspector] public OverheadParry ovPa;

    public bool facedByPlayer;
    public bool canParry=true;
    public float parryChance=.5f, parryIntervalMin=.3f, parryIntervalMax=.5f, feintToParryTime=.1f;

    void Awake()
    {
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        hp=GetComponent<HPManager>();
        ovPa=GetComponent<OverheadParry>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==6) //touch player trigger
        {
            facedByPlayer=true;
        }

        if(other.gameObject.layer==7 && facedByPlayer) //touch player weapon
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
        if(other.gameObject.layer==6) //exit player trigger
        {
            facedByPlayer=false;
        }
    }

    void Update()
    {
        if(facedByPlayer && player.ovPa.windingUp && canParry)
        {
            canParry=false;
            parry();
        }
    }

    void parry()
    {
        StartCoroutine(parrying());
        StartCoroutine(parryCooldown());
    }

    IEnumerator parrying()
    {
        if(Random.Range(0f,1f) <= parryChance)
        {
            yield return new WaitForSeconds(Random.Range(parryIntervalMin,parryIntervalMax));

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
