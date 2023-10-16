using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

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
    public VisualEffect vfxBlood, vfxSpark;
    InOutAnim dmgvig;
    Volume hurtvolume;

    void Awake()
    {
        coll=GetComponent<CapsuleCollider>();
        turn=GetComponent<PlayerTurn>();
        attack=GetComponent<PlayerAttack>();
        parry=GetComponent<PlayerParry>();
        hp=GetComponent<HPManager>();
        ovPa=GetComponent<OverheadParry>();
        camshake = GameObject.FindGameObjectWithTag("camshake").GetComponent<WiggleRotate>();
        dmgvig = GameObject.FindGameObjectWithTag("dmgvig").GetComponent<InOutAnim>();
        hurtvolume = GameObject.FindGameObjectWithTag("hurtvolume").GetComponent<Volume>();
    }

    void Start()
    {
        Singleton.instance.player = gameObject;

        Singleton.instance.playerAlive=true;

        Singleton.instance.playerKills=0;

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

        vfxBlood.Play();

        cancelRtLt();

        dmgvigrt = StartCoroutine(dmgviging());
    }

    public void die()
    {
        ovPa.interrupt();

        anim.SetTrigger("death player");

        anim.SetBool("mirror", Random.Range(1,3)==1);

        camshake.shake();

        vfxBlood.Play();

        cancelRtLt();

        dmgvig.animIn(.1f);

        tweenhurtvolume(.3f,.1f);

        firstPersonMode(.5f);

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

    void cancelRtLt()
    {
        if(dmgvigrt!=null) StopCoroutine(dmgvigrt);

        LeanTween.cancel(hurtvolumelt);
    }

    Coroutine dmgvigrt;

    IEnumerator dmgviging()
    {
        dmgvig.animIn(.1f);

        tweenhurtvolume(.3f,.1f);

        yield return new WaitForSeconds(.1f);

        dmgvig.animOut(Random.Range(.5f,1));

        tweenhurtvolume(0,Random.Range(.5f,1));
    }

    int hurtvolumelt=0;

    void tweenhurtvolume(float val, float time)
    {
        hurtvolumelt = LeanTween.value(hurtvolume.weight, val, time).setOnUpdate(updateHurtVolume).id;
    }
    void updateHurtVolume(float value)
    {
        hurtvolume.weight = value;
    }

}
