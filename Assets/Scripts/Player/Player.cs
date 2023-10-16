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
    WiggleRotate camshake;
    public Transform firstperson;

    void Awake()
    {
        coll=GetComponent<CapsuleCollider>();
        turn=GetComponent<PlayerTurn>();
        attack=GetComponent<PlayerAttack>();
        parry=GetComponent<PlayerParry>();
        hp=GetComponent<HPManager>();
        ovPa=GetComponent<OverheadParry>();
        camshake = GameObject.FindGameObjectWithTag("camshake").GetComponent<WiggleRotate>();

        Singleton.instance.player = gameObject;

        Singleton.instance.playerAlive=true;

        Singleton.instance.playerKills=0;
    }

    void Start()
    {
        StartCoroutine(intro());        
    }

    IEnumerator intro()
    {
        firstPersonMode(0);

        yield return new WaitForSeconds(2);

        thirdPersonMode(1);
    }

    public void hit()
    {
        ovPa.interrupt();

        anim.SetTrigger("hit");

        camshake.shake();
    }

    public void die()
    {
        ovPa.interrupt();

        anim.SetTrigger("death player");

        anim.SetBool("mirror", Random.Range(1,3)==1);

        camshake.shake();

        firstPersonMode(1);

        Singleton.instance.playerAlive=false;

        coll.enabled=false;
        trigger.SetActive(false);
        weapon.SetActive(false);
    }

    void thirdPersonMode(float time)
    {
        Singleton.instance.cam.transform.parent = camshake.transform;

        LeanTween.moveLocal(Singleton.instance.cam.gameObject, Vector3.zero, time).setEaseInOutSine();
        LeanTween.rotateLocal(Singleton.instance.cam.gameObject, Vector3.zero, time).setEaseInOutSine();
    }

    void firstPersonMode(float time)
    {
        Singleton.instance.cam.transform.parent = firstperson;

        LeanTween.moveLocal(Singleton.instance.cam.gameObject, Vector3.zero, time).setEaseInOutSine();
        LeanTween.rotateLocal(Singleton.instance.cam.gameObject, Vector3.zero, time).setEaseInOutSine();
    }
}
