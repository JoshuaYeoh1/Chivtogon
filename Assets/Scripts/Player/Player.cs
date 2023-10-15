using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public OverheadParry ovPa;

    void Awake()
    {
        ovPa=GetComponent<OverheadParry>();
    }    
}
