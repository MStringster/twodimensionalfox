using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : InteractableObject
{
    PlayerControl player;
    private PlayerTextBox playertxtbox;
    [SerializeField] GameObject particleSystem;

    public void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        playertxtbox = player.GetComponentInChildren<PlayerTextBox>();

    }

    private void Update()
    {
        if(!player.Is2DMode)
        {
            particleSystem.SetActive(true);
        }
    }
    public override void Interact()
    {
        if(!player.Is2DMode)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            playertxtbox.ShowText("Requires Crystal Energy");
        }
    }

}
