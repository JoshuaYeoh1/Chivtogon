using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;
using TMPro;

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
    EnemySpawner spawner;

    GameObject tutorial1, tutorial2, tutorial3, tutorial4;
    bool doTutorial;

    InOutAnim hpbar, killcounter;
    TextMeshProUGUI killcounttext;

    public SkinRandomizerScript weaponSkin;
    [HideInInspector] public string weaponType;

    [HideInInspector] public CharacterVoice voice;
    [HideInInspector] public string voicetype;

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
        spawner = GameObject.FindGameObjectWithTag("spawner").GetComponent<EnemySpawner>();

        tutorial1 = GameObject.FindGameObjectWithTag("tutorial1");
        tutorial2 = GameObject.FindGameObjectWithTag("tutorial2");
        tutorial3 = GameObject.FindGameObjectWithTag("tutorial3");
        tutorial4 = GameObject.FindGameObjectWithTag("tutorial4");
        tutorial1.SetActive(false);
        tutorial2.SetActive(false);
        tutorial3.SetActive(false);
        tutorial4.SetActive(false);

        hpbar = GameObject.FindGameObjectWithTag("hpbar").GetComponent<InOutAnim>();
        killcounter = GameObject.FindGameObjectWithTag("killcounter").GetComponent<InOutAnim>();
        killcounttext = GameObject.FindGameObjectWithTag("killcounttext").GetComponent<TextMeshProUGUI>();

        voice=GetComponent<CharacterVoice>();
    }

    void Start()
    {
        Singleton.instance.awakePlayer();

        Singleton.instance.player = gameObject;

        Singleton.instance.playerAlive=true;

        Singleton.instance.playerKills=0;

        checkWeaponType();

        voicetype = voice.playerVoice[Random.Range(0,voice.playerVoice.Length)];

        StartCoroutine(intro());
    }

    void checkWeaponType()
    {
        if(weaponSkin.skin==0) weaponType="axe";
        else if(weaponSkin.skin==1) weaponType="blunt";
        else if(weaponSkin.skin==2) weaponType="blade";
        else weaponType="axe";
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

        thirdPersonMode(2);

        Singleton.instance.controlsEnabled=true;

        //if(Application.platform!=RuntimePlatform.WindowsPlayer && Singleton.instance.swipeDownCount==0 && Singleton.instance.swipeRightCount==0 && Singleton.instance.swipeLeftCount==0 && Singleton.instance.swipeUpCount==0)
        if(Singleton.instance.swipeDownCount==0 && Singleton.instance.swipeRightCount==0 && Singleton.instance.swipeLeftCount==0 && Singleton.instance.swipeUpCount==0)
        {
            doTutorial=true;

            tutorial1.SetActive(true);
            tutorial1.transform.localScale = Vector3.zero;
            LeanTween.scale(tutorial1, Vector3.one, .25f).setEaseOutBack();
        }
        else
        {
            startTheRound();
        }   
    }

    void Update()
    {
        if(doTutorial)
        {
            if(Singleton.instance.swipeLeftCount>0 && Singleton.instance.swipeRightCount>0 && !Singleton.instance.doneTutorial1)
            {
                Singleton.instance.doneTutorial1=true;

                LeanTween.scale(tutorial1, Vector3.zero, .25f).setEaseInBack();
                Destroy(tutorial1, .25f);

                tutorial2.SetActive(true);
                tutorial2.transform.localScale = Vector3.zero;
                LeanTween.scale(tutorial2, Vector3.one, .25f).setDelay(.25f).setEaseOutBack();
            }

            if(Singleton.instance.swipeDownCount>0 && Singleton.instance.doneTutorial1 && !Singleton.instance.doneTutorial2)
            {
                Singleton.instance.doneTutorial2=true;

                LeanTween.scale(tutorial2, Vector3.zero, .25f).setEaseInBack();
                Destroy(tutorial2, .25f);

                tutorial3.SetActive(true);
                tutorial3.transform.localScale = Vector3.zero;
                LeanTween.scale(tutorial3, Vector3.one, .25f).setDelay(.25f).setEaseOutBack();
            }
            
            if(Singleton.instance.swipeUpCount>0 && Singleton.instance.doneTutorial1 && Singleton.instance.doneTutorial2 && !Singleton.instance.doneTutorial3)
            {
                Singleton.instance.doneTutorial3=true;

                LeanTween.scale(tutorial3, Vector3.zero, .25f).setEaseInBack();
                Destroy(tutorial3, .25f);

                StartCoroutine(showTutorial4());
            }
        }

        killcounttext.text = Singleton.instance.playerKills.ToString();
    }

    IEnumerator showTutorial4()
    {
        tutorial4.SetActive(true);
        tutorial4.transform.localScale = Vector3.zero;
        LeanTween.scale(tutorial4, Vector3.one, .25f).setDelay(.25f).setEaseOutBack();

        yield return new WaitForSeconds(6);

        LeanTween.scale(tutorial4, Vector3.zero, .25f).setEaseInBack();
        Destroy(tutorial4, .25f);

        yield return new WaitForSeconds(.25f);

        doTutorial=false;

        startTheRound();
    }

    void startTheRound()
    {
        spawner.startSpawning();
        
        hpbar.animIn(.2f);
        killcounter.animIn(.2f);
    }

    public void hit(bool interrupt=true)
    {
        if(interrupt) ovPa.interrupt();

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

        hpbar.animOut(.2f);
        killcounter.animOut(.2f);

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
