using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CapsuleCollider coll;
    PlayerTurn turn;
    PlayerAttack attack;
    PlayerParry parry;
    HPManager hp;
    [HideInInspector] public OverheadParry ovPa;
    public Animator anim;
    public GameObject trigger, weapon;

    void Awake()
    {
        coll=GetComponent<CapsuleCollider>();
        turn=GetComponent<PlayerTurn>();
        attack=GetComponent<PlayerAttack>();
        parry=GetComponent<PlayerParry>();
        hp=GetComponent<HPManager>();
        ovPa=GetComponent<OverheadParry>();

        Singleton.instance.player = gameObject;

        Singleton.instance.playerAlive=true;

        Singleton.instance.playerKills=0;
    }

    public void hit()
    {
        ovPa.interrupt();

        anim.SetTrigger("hit");
    }

    public void die()
    {
        ovPa.interrupt();

        anim.SetTrigger("death player");

        anim.SetBool("mirror", Random.Range(1,3)==1);

        Singleton.instance.playerAlive=false;

        coll.enabled=false;
        trigger.SetActive(false);
        weapon.SetActive(false);
    }
}
