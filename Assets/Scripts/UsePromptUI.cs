using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsePromptUI : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] float promptDistance = 3f;
    [SerializeField] string interactableTag = "Interactable";

    private TMP_Text promptText;

    void Start()
    {
        promptText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (playerObject != null)
        {
            Collider[] colliders = Physics.OverlapSphere(playerObject.transform.position, promptDistance);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(interactableTag))
                {
                    promptText.enabled = true;
                    return;
                }
            }
        }

        promptText.enabled = false;
    }
}