using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    bool objectHeld = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!objectHeld)
        {
            if(other.transform.tag == "Key")
            {
                other.transform.parent = transform;
                objectHeld = true;
            }
        }
    }
}
