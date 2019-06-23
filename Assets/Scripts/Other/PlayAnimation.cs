//---------------------------------//
// Author: Eduardo Vicenzi Kuhn    //
// Date: 23/06/2019                //
// github.com/eduardovk            //
//---------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : DAS_Action
{
    public DAS_Dialogue dialogue;
    public DAS_DialogueSystem dialogueSystem;
    public GameObject effect;
    public Animator animator;

    public override void execute()
    {
        // in this example, there is still dialogues after the animation
        // dialoguesystem.active = true will keep the dialogue active,
        // so the user dont open the dialogue again while animation is playing
        dialogueSystem.active = true;
        dialogueSystem.dialogueController.dialogueInCourse = true;
        effect.SetActive(true);
        GetComponent<AudioSource>().Play();
        animator.SetTrigger("jump");
        Invoke("callNextDialogue", 1f);
    }

    private void callNextDialogue()
    {
        effect.SetActive(false);
        dialogueSystem.callSpecificDialogue(dialogue);
    }

}
