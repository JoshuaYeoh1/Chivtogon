using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    OverheadParry ovPa;

    void Awake()
    {
        ovPa = GetComponent<OverheadParry>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            ovPa.overhead();
        }
    }
}
