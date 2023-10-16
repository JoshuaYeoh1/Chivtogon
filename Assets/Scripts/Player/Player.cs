using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    CapsuleCollider coll;
    [HideInInspector] public PlayerTurn turn;
    [HideInInspector] public PlayerAttack attack;
    [HideInInspector] public PlayerParry parry;
    HPManager hp;
    [HideInInspector] public OverheadParry ovPa;
    public Animator anim;
    public GameObject trigger, weapon;
    WiggleRotate camshake;
    public Transform firstperson, secondperson;
    public VisualEffect vfxBlood, vfxSpark;
    InOutAnim dmgvig, black;
    Volume hurtvolume;
    Letterbox letterbox;

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
        black = GameObject.FindGameObjectWithTag("black").GetComponent<InOutAnim>();
        hurtvolume = GameObject.FindGameObjectWithTag("hurtvolume").GetComponent<Volume>();
        letterbox = GameObject.FindGameObjectWithTag("letterbox").GetComponent<Letterbox>();
    }

    void Start()
    {
        Singleton.instance.awakePlayer();

        Singleton.instance.player = gameObject;

        Singleton.instance.playerAlive=true;

        Singleton.instance.playerKills=0;

        StartCoroutine(intro());        
    }

    IEnumerator intro()
    {
        Singleton.instance.controlsEnabled=false;

        if(Random.Range(1,3)==1) firstPersonMode(0); else secondPersonMode(0);

        black.animIn(0);

        letterbox.animIn(0);

        yield return new WaitForSeconds(.5f);

        black.animOut(1);

        yield return new WaitForSeconds(2);

        letterbox.animOut(.5f);

        yield return new WaitForSeconds(.5f);

        Singleton.instance.controlsEnabled=true;

        thirdPersonMode(2);
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

        anim.SetTrigger("death player");

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

        StartCoroutine(outro());
    }

    IEnumerator outro()
    {
        Singleton.instance.controlsEnabled=false;

        yield return new WaitForSeconds(2.5f);

        //letterbox.animIn(.5f);

        black.animIn(1);

        yield return new WaitForSeconds(1.5f);

        dmgvig.animOut(0);

        tweenhurtvolume(0,0);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name); 
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

    void secondPersonMode(float time)
    {
        Singleton.instance.cam.transform.parent = secondperson;

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
