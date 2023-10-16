using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letterbox : MonoBehaviour
{
    public RectTransform[] blackboxes;

    void Awake()
    {
        zeroScale();
    }

    public void animIn(float time)
    {
        zeroScale();
        tweenScale(1, time);
    }
    public void animOut(float time)
    {
        tweenScale(1, 0);
        tweenScale(0, time);
    }

    void tweenScale(float y, float time)
    {
        LeanTween.value(blackboxes[0].localScale.y, y, time).setEaseInOutSine().setOnUpdate(TweenUpdate);
    }
    void TweenUpdate(float value)
    {
        for(int i=0;i<blackboxes.Length;i++)
        {
            blackboxes[i].localScale = new Vector2(1,value);
        }
    }

    void zeroScale()
    {
        for(int i=0;i<blackboxes.Length;i++)
        {
            blackboxes[i].localScale = new Vector2(1,0);
        }
    }
}
