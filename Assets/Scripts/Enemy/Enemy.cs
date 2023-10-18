using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
    CapsuleCollider coll;
    EnemyAdvance advance;
    EnemyStrafe strafe;
    EnemyLineUp lineup;
    EnemyAttack attack;
    EnemyParry parry;
    HPManager hp, playerhp;
    [HideInInspector] public OverheadParry ovPa;
    public Animator anim;
    public GameObject trigger, weapon;
    public VisualEffect vfxBlood, vfxSpark;

    public bool facedByPlayer, dead, inDoorway;

    public SkinRandomizerScript weaponSkin;
    [HideInInspector] public string weaponType;

    [HideInInspector] public CharacterVoice voice;
    [HideInInspector] public string voicetype;

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
        playerhp=GameObject.FindGameObjectWithTag("Player").GetComponent<HPManager>();

        checkWeaponType();

        voice=GetComponent<CharacterVoice>();

        voicetype = voice.enemyVoice[Random.Range(0,voice.enemyVoice.Length)];
    }

    void checkWeaponType()
    {
        if(weaponSkin.skin==0) weaponType="axe";
        else if(weaponSkin.skin==1) weaponType="blunt";
        else if(weaponSkin.skin==2) weaponType="blade";
        else weaponType="axe";
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==6 && other.tag=="FaceBox") //touch player facebox
        {
            facedByPlayer=true;
        }

        if(other.gameObject.layer==11) //touch door trigger
        {
            inDoorway=true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer==6  && other.tag=="FaceBox") //exit player facebox
        {
            facedByPlayer=false;
        }

        if(other.gameObject.layer==11) //exit door trigger
        {
            inDoorway=false;
        }
    }

    public void hit(bool interrupt=true)
    {
        if(interrupt) ovPa.interrupt();

        anim.SetTrigger("hit");

        vfxBlood.Play();
    }

    public void die()
    {
        ovPa.interrupt();

        if(Random.Range(1,3)==1) anim.SetTrigger("death enemy"); else anim.SetTrigger("death player");

        anim.SetBool("mirror", Random.Range(1,3)==1);

        vfxBlood.Play();

        dead=true;

        coll.enabled=false;
        trigger.SetActive(false);
        weapon.SetActive(false);

        StartCoroutine(sinkAnim());

        playerhp.hp += Mathf.RoundToInt((playerhp.hpmax-playerhp.hp)*.5f);
        playerhp.updateBarFill();

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
