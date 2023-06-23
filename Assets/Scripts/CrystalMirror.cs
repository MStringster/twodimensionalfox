using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMirror : InteractableObject
{
    PlayerControl player;

    public void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    public override void Interact()
    {
        player.SwitchPerspective();
    }
}
