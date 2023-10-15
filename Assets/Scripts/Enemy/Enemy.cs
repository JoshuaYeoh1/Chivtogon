using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public OverheadParry ovPa;

    void Awake()
    {
        ovPa=GetComponent<OverheadParry>();
    }   
}
