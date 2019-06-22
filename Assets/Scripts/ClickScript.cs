using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    public DAS_DialogueSystem dialogueSystem;

    private void OnMouseDown()
    {
        dialogueSystem.startDialogue();
    }
}
