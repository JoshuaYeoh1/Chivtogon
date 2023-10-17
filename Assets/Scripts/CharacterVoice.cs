using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVoice : MonoBehaviour
{
    AudioSource voice;

    public string[] playerVoice={"pappas", "snook"}, enemyVoice={"justin", "bob"};

    public AudioClip[] pappasGruntHigh, pappasGruntLow, pappasHurtLow, pappasHurtMed, pappasHurtHigh, pappasGurgle;
    public AudioClip[] snookGruntHigh, snookGruntLow, snookHurtLow, snookHurtMed, snookHurtHigh, snookGurgle;
    public AudioClip[] justinGruntHigh, justinGruntLow, justinHurtLow, justinHurtMed, justinHurtHigh, justinGurgle;
    public AudioClip[] bobGruntHigh, bobGruntLow, bobHurtLow, bobHurtMed, bobHurtHigh, bobGurgle;

    void Awake()
    {
        voice = GetComponent<AudioSource>();
    }

    public void say(AudioClip[] clip)
    {
        voice.clip = clip[Random.Range(0,clip.Length)];
        voice.Play();
    }

    public void gruntHigh(string voicetype)
    {
        switch(voicetype)
        {
            case "pappas": say(pappasGruntHigh); break;
            case "snook": say(snookGruntHigh); break;
            case "justin": say(justinGruntHigh); break;
            case "bob": say(bobGruntHigh); break;
        }
    }
    
    public void gruntLow(string voicetype)
    {
        switch(voicetype)
        {
            case "pappas": say(pappasGruntLow); break;
            case "snook": say(snookGruntLow); break;
            case "justin": say(justinGruntLow); break;
            case "bob": say(bobGruntLow); break;
        }
    }
    
    public void hurtLow(string voicetype)
    {
        switch(voicetype)
        {
            case "pappas": say(pappasHurtLow); break;
            case "snook": say(snookHurtLow); break;
            case "justin": say(justinHurtLow); break;
            case "bob": say(bobHurtLow); break;
        }
    }
    
    public void HurtMed(string voicetype)
    {
        switch(voicetype)
        {
            case "pappas": say(pappasHurtMed); break;
            case "snook": say(snookHurtMed); break;
            case "justin": say(justinHurtMed); break;
            case "bob": say(bobHurtMed); break;
        }
    }
    
    public void hurtHigh(string voicetype)
    {
        switch(voicetype)
        {
            case "pappas": say(pappasHurtHigh); break;
            case "snook": say(snookHurtHigh); break;
            case "justin": say(justinHurtHigh); break;
            case "bob": say(bobHurtHigh); break;
        }
    }
    
    public void death(string voicetype)
    {
        StartCoroutine(gurgling(voicetype));
    }
    IEnumerator gurgling(string voicetype)
    {
        switch(voicetype)
        {
            case "pappas":
            {
                say(pappasHurtHigh);
                yield return new WaitForSeconds(voice.clip.length);
                say(pappasGurgle);
                break;
            }
            case "snook":
            {
                say(snookHurtHigh);
                yield return new WaitForSeconds(voice.clip.length);
                say(snookGurgle);
                break;
            }
            case "justin":
            {
                say(justinHurtHigh);
                yield return new WaitForSeconds(voice.clip.length);
                say(justinGurgle);
                break;
            }
            case "bob":
            {
                say(bobHurtHigh);
                yield return new WaitForSeconds(voice.clip.length);
                say(bobGurgle);
                break;
            }
        }
    }
}
