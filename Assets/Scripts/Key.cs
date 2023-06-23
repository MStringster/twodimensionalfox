using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableObject
{
    [SerializeField] int keyID = 0;
    public int KeyID => keyID;

    [SerializeField] float floatSpeed = 5f;
    [SerializeField] float floatHeight = 2f;

    Vector3 startPos;

    bool pickedUp = false;

    public override void Interact()
    {
        Debug.Log("Key Interacted With");
        transform.parent = FindObjectOfType<PlayerControl>().transform;
        transform.position = transform.parent.position + transform.parent.transform.right * 2f;
        transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        pickedUp = true;
    }

    private void Start()
    {
        startPos = transform.position; 
    }
    public void Update()
    {
        if (!pickedUp)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            float verticalOffset = newY - transform.position.y;
            transform.Translate(0f, verticalOffset, 0f);
        }

    }
}
 