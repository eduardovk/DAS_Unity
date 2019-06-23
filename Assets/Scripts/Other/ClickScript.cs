//---------------------------------//
// Author: Eduardo Vicenzi Kuhn    //
// Date: 23/06/2019                //
// github.com/eduardovk            //
//---------------------------------//
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
