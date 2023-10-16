using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        awakeAudio();
        awakePlayer();
    }

    void awakeAudio()
    {
        LoadVolume();

        musicSource.clip = music[(Random.Range(0,music.Length))];
        musicSource.Play();
        
        ambSource.clip = ambient[(Random.Range(0,ambient.Length))];
        ambSource.Play();
    }

    [Header("Audio Manager")]
    public AudioMixer mixer;
    public AudioSource SFXObject, musicSource, ambSource;
    public AudioClip[] music, ambient;

    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    public void playSFX(AudioClip[] clip, Transform spawnTransform, bool dynamics=true, bool randPitch=true, float volume=1)
    {   
        AudioSource source = Instantiate(SFXObject, spawnTransform.position, Quaternion.identity);
        
        source.clip = clip[Random.Range(0,clip.Length)];

        source.volume = volume;

        SFXObject sfxobj = source.GetComponent<SFXObject>();
        sfxobj.randPitch = randPitch;
        sfxobj.dynamics = dynamics;

        source.Play();

        Destroy(source.gameObject, source.clip.length);
    }

    void LoadVolume()
    {   
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MASTER, Mathf.Log10(masterVolume)*20);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume)*20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume)*20);
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////


    [Header("Player")]
    public GameObject player;
    [HideInInspector] public Camera cam;
    public bool playerAlive=true, controlsEnabled=true;
    public int playerKills;
    
    [Header("Enemies")]
    public int enemiesAlive;

    public void awakePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
