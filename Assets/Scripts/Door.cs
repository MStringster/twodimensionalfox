using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] int doorID;
    Animator anim;
    Collider coll;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {

            if(other.transform.GetComponent<Key>().KeyID == doorID)
            {
                Destroy(other.gameObject);
            anim.enabled = true;
            coll.enabled = false;
               // Destroy(this.gameObject);
            }
       
    }
}
