using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour
{
    Image img;
    SpriteRenderer sr;
    Vector2 defscale;
    Sprite defSprite;

    public Sprite hoverSprite;
    public float animTime=.3f;

    //public AudioClip[] snd_click, snd_hover;

    void Awake()
    {
        img = GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();
        defscale = transform.localScale;

        if(sr==null)
            defSprite = img.sprite;
        else if(img==null)
            defSprite = sr.sprite;
    }

    public void OnMouseEnter()
    {
        if(sr==null)
            img.sprite=hoverSprite;
        else if(img==null)
            sr.sprite=hoverSprite;
        
        LeanTween.scale(gameObject, new Vector2(defscale.x*1.2f,defscale.y*1.2f), animTime).setEaseOutExpo();

        //Singleton.instance.playSFX(snd_hover,transform,false);
    }

    public void OnMouseExit()
    {
        if(sr==null)
            img.sprite=defSprite;
        else if(img==null)
            sr.sprite=defSprite;

        LeanTween.scale(gameObject, defscale, animTime).setEaseOutBack();
    }

    public void OnMouseDown()
    {
        LeanTween.scale(gameObject, new Vector2(defscale.x*.9f,defscale.y*.9f), animTime/2).setEaseOutExpo();

        LeanTween.scale(gameObject, new Vector2(defscale.x*1.2f,defscale.y*1.2f), animTime/2).setDelay(animTime/2).setEaseOutBack();

        //Singleton.instance.playSFX(snd_click,transform,false);
    }
}
