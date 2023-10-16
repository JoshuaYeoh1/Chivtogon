using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXAttach : MonoBehaviour
{
    public Transform attachment;

    void Update()
    {
        transform.position = attachment.position;
    }
}
