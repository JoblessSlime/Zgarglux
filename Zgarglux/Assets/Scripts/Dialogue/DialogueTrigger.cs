using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool playerInRange = false;
    public GameObject pressEUI; // UI prompt (e.g., "Press E to talk")

    void Start()
    {
        if (pressEUI != null)
            pressEUI.SetActive(false); // Ensure the UI is hidden at the start
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pressEUI != null)
                pressEUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressEUI != null)
                pressEUI.SetActive(false);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        if (pressEUI != null)
            pressEUI.SetActive(false); // Hide "Press E" UI when dialogue starts
    }
}