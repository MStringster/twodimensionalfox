using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTextBox : MonoBehaviour
{
    [SerializeField] TMP_Text textBox;

    [SerializeField] Vector3 offset;
    [SerializeField] float displayDuration = 3f;

    private float timer;
    private bool isDisplayed;

    private void Start()
    {
        textBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(isDisplayed)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                HideText();
            }
        }


    }

    private void LateUpdate()
    {
        textBox.transform.position = transform.position + offset;
    }

    public void ShowText(string text)
    {
       
        textBox.text = text;
        timer = displayDuration;
        isDisplayed = true;

        textBox.gameObject.SetActive(true);
    }

    public void HideText()
    {
        textBox.gameObject.SetActive(false);
        isDisplayed = false;
    }
}