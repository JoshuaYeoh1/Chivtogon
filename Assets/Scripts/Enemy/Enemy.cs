using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CapsuleCollider coll;
    EnemyAdvance advance;
    EnemyStrafe strafe;
    EnemyLineUp lineup;
    EnemyAttack attack;
    EnemyParry parry;
    HPManager hp;
    [HideInInspector] public OverheadParry ovPa;
    public Animator anim;
    public GameObject trigger, weapon;

    public bool dead;

    void Start()
    {
        coll=GetComponent<CapsuleCollider>();
        advance=GetComponent<EnemyAdvance>();
        strafe=GetComponent<EnemyStrafe>();
        lineup=GetComponent<EnemyLineUp>();
        attack=GetComponent<EnemyAttack>();
        parry=GetComponent<EnemyParry>();
        hp=GetComponent<HPManager>();
        ovPa=GetComponent<OverheadParry>();
    }

    public void hit()
    {
        ovPa.interrupt();

        anim.SetTrigger("hit");

        anim.SetBool("mirror", Random.Range(1,3)==1);
    }

    public void die()
    {
        ovPa.interrupt();

        anim.SetTrigger("death enemy");

        anim.SetBool("mirror", Random.Range(1,3)==1);

        dead=true;

        coll.enabled=false;
        trigger.SetActive(false);
        weapon.SetActive(false);

        StartCoroutine(sinkAnim());

        Singleton.instance.playerKills++;
        Singleton.instance.enemiesAlive--;
    }

    IEnumerator sinkAnim()
    {
        yield return new WaitForSeconds(3f);

        LeanTween.moveLocalY(gameObject, transform.localPosition.y-1, 1).setEaseInOutSine();

        yield return new WaitForSeconds(1);

        Destroy(gameObject.transform.parent.gameObject);
    }
}
