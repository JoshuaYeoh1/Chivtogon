using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject parent;
    //[HideInInspector] public Enemy enemy;
    [HideInInspector] public OverheadParry ovPa;

    void Awake()
    {
        //enemy = parent.GetComponent<Enemy>();
        ovPa = parent.GetComponent<OverheadParry>();
    }
}
