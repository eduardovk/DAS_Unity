//---------------------------------//
// Author: Eduardo Vicenzi Kuhn    //
// Date: 23/06/2019                //
// github.com/eduardovk            //
//---------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAS_DialogueSystem : MonoBehaviour
{
    public DAS_DialogueController dialogueController;
    public DAS_DialogueBox dialogueBox;
    
    public DAS_Dialogue[] dialogues;
    public float phraseSpeed = 0.015f;
    public Sprite photo;
    public string title;
    public bool random = false;

    [Header("Optional")]
    public DAS_Action actionWhenStart;
    public DAS_Action actionWhenFinish;

    [HideInInspector]
    public int currentDialogueIndex = 0;
    [HideInInspector]
    public int currentPhraseIndex = 0;
    [HideInInspector]
    public bool active = false;

    private IEnumerator currentCoroutine;
    private string currentText = "";
    private bool clicable = true;

    public void startDialogue()
    {
        if (!active && !dialogueController.dialogueInCourse)
        {
            active = true;
            dialogueController.dialogueInCourse = true;
            dialogueBox.currentDialogueSystem = this;
            int diagIndex = random ? Random.Range(0, dialogues.Length) : 0;
            currentDialogueIndex = diagIndex;
            currentPhraseIndex = 0;
            buildDialogueBox(dialogueBox, dialogues[diagIndex], 0);
            dialogues[diagIndex].playSound();
            dialogueBox.playDialogueBoxSound();
            dialogueBox.showUpAnimation();
            // If there is a DAS_Action set to execute when the dialog starts
            if (actionWhenStart)
            {
                actionWhenStart.execute();
            }
        }
    }

    public void endDialogue()
    {
        active = false;
        dialogueController.dialogueInCourse = false;
        dialogueBox.dialogueWithOptions.SetActive(false);
        dialogueBox.dialogueNoOptions.SetActive(false);
        disableOptions(dialogueBox);
        dialogueBox.photo.enabled = false;
        dialogueBox.titleNoPhoto.enabled = false;
        dialogueBox.titlePhoto.enabled = false;
        dialogueBox.phraseNoPhoto.enabled = false;
        dialogueBox.phrasePhoto.enabled = false;
        // If this is the last dialogue and there is a DAS_Action set to execute when it finishes
        if (dialogues[currentDialogueIndex].lastOne && actionWhenFinish)
        {
            actionWhenFinish.execute();
        }
    }

    public void nextPhrase()
    {
        // If there is still phrases in the current dialogue, call the next phrase
        if(dialogues[currentDialogueIndex].phrases.Length > (currentPhraseIndex + 1))
        {
            Debug.Log("next phrase");
            currentPhraseIndex++;
            buildDialogueBox(dialogueBox, dialogues[currentDialogueIndex], currentPhraseIndex);
        }
        // If there is no phrase left in the current dialogue
        else
        {
            // If the current dialogue is the last one, or if its the last on the array
            if (dialogues[currentDialogueIndex].lastOne || dialogues.Length <= currentDialogueIndex)
            {
                endDialogue();
            }
            // If its not the last dialogue, call the next one
            else
            {
                nextDialogue();
            }
        }
    }

    // Animate the letters to a typing effect
    IEnumerator animatePhrase(Text textContainer, string text, float speed)
    {
        clicable = false;
        currentText = "";
        if (dialogues[currentDialogueIndex].playTypingSound)
        {
            dialogueBox.playTypeSound();
        }
        for (int i = 0; i < text.Length; i++)
        {
            currentText = text.Substring(0, i+1);
            textContainer.text = currentText;
            
            yield return new WaitForSeconds(speed);
        }
        if (dialogues[currentDialogueIndex].playTypingSound)
        {
            dialogueBox.stopTypeSound();
        }
        clicable = true;
    }

    public void nextDialogue()
    {
        // If current dialogue jumps to another dialogue out of order
        if (dialogues[currentDialogueIndex].jumpToDialogue)
        {
            callSpecificDialogue(dialogues[currentDialogueIndex].targetDialogue);
            dialogueBox.showUpAnimation();
        }
        // Else check if there is another dialogue in the queue
        else
        {
            if (dialogues.Length > (currentDialogueIndex + 1))
            {
                currentDialogueIndex++;
                currentPhraseIndex = 0;
                buildDialogueBox(dialogueBox, dialogues[currentDialogueIndex], currentPhraseIndex);
                dialogues[currentDialogueIndex].playSound();
                dialogueBox.playDialogueBoxSound();
                dialogueBox.showUpAnimation();
            }
        }
    }

    // A dialogue can call a specific dialogue that is not in sequence
    public void callSpecificDialogue(DAS_Dialogue dialogue)
    {
        active = true;
        dialogueController.dialogueInCourse = true;
        int diagIndex = 0;
        // Search the index number for the provided dialogue
        for (int i = 0; i < dialogues.Length; i++)
        {
            if (dialogues[i] == dialogue)
            {
                diagIndex = i;
            }
        }
        currentPhraseIndex = 0;
        currentDialogueIndex = diagIndex;
        buildDialogueBox(dialogueBox, dialogues[currentDialogueIndex], currentPhraseIndex);
        dialogues[currentDialogueIndex].playSound();
        dialogueBox.playDialogueBoxSound();
        dialogueBox.showUpAnimation();
    }

    public void buildDialogueBox(DAS_DialogueBox dialogueBox, DAS_Dialogue dialogue, int phraseIndex = 0)
    {
        active = true;
        dialogueController.dialogueInCourse = true;
        disableOptions(dialogueBox);
        // If current dialogue has options, activate the bigger box
        if (dialogue.options.Length > 0)
        {
            dialogueBox.dialogueWithOptions.SetActive(true);
            dialogueBox.dialogueNoOptions.SetActive(false);
            // If single option, enable single option box
            if(dialogue.options.Length == 1)
            {
                dialogueBox.singleOptionBox.SetActive(true);
                dialogueBox.singleOptionText.text = dialogue.options[0].phrase;  
            }
            // If two options, enable center option boxes
            else if(dialogue.options.Length == 2)
            {
                dialogueBox.option1CenterBox.SetActive(true);
                dialogueBox.option2CenterBox.SetActive(true);
                dialogueBox.option1CenterText.text = dialogue.options[0].phrase;
                dialogueBox.option2CenterText.text = dialogue.options[1].phrase;
            }
            // If more than two options, enable corresponding boxes
            else
            {
                dialogueBox.option1Box.SetActive(true);
                dialogueBox.option2Box.SetActive(true);
                dialogueBox.option3Box.SetActive(true);
                dialogueBox.option1Text.text = dialogue.options[0].phrase;               
                dialogueBox.option2Text.text = dialogue.options[1].phrase;
                dialogueBox.option3Text.text = dialogue.options[2].phrase;
                if (dialogue.options.Length > 3)
                {
                    dialogueBox.option4Box.SetActive(true);
                    dialogueBox.option4Text.text = dialogue.options[3].phrase;
                }
            }
        }
        // If there is no option, active smaller box
        else
        {
            dialogueBox.dialogueWithOptions.SetActive(false);
            dialogueBox.dialogueNoOptions.SetActive(true);
        }

        // If current dialogue inherits style, use photo and title of this
        if (dialogue.inheritStyle)
        {
            assignPhotoAndTexts(dialogueBox, this.photo, this.title, dialogue.phrases[phraseIndex]);
        }
        else // If it doesnt inherit, use photo and title of current dialogue
        {
            assignPhotoAndTexts(dialogueBox, dialogue.photo, dialogue.title, dialogue.phrases[phraseIndex]);
        }
    }

    public void assignPhotoAndTexts(DAS_DialogueBox diagBox, Sprite photo, string title, string phrase)
    {
        // If there is a coroutine running the typing animation, stop it
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        if (photo)
        {
            diagBox.photo.sprite = photo;
            diagBox.photo.enabled = true;
            diagBox.titlePhoto.enabled = true;
            diagBox.titleNoPhoto.enabled = false;
            diagBox.titlePhoto.text = title;
            diagBox.phrasePhoto.enabled = true;
            diagBox.phraseNoPhoto.enabled = false;
            // If dialogue animatePhrases is checked, start coroutine with animation process
            if (dialogues[currentDialogueIndex].animatePhrases)
            {
                currentCoroutine = animatePhrase(diagBox.phrasePhoto, phrase, phraseSpeed);
                StartCoroutine(currentCoroutine);
            }
            // Else, just display the plain text
            else
            {
                diagBox.phrasePhoto.text = phrase;
            }
        }
        else
        {
            diagBox.photo.enabled = false;
            diagBox.titlePhoto.enabled = false;
            diagBox.titleNoPhoto.enabled = true;
            diagBox.titleNoPhoto.text = title;
            diagBox.phrasePhoto.enabled = false;
            diagBox.phraseNoPhoto.enabled = true;
            // If dialogue animatePhrases is checked, start coroutine with animation process
            if (dialogues[currentDialogueIndex].animatePhrases)
            {
                currentCoroutine = animatePhrase(diagBox.phraseNoPhoto, phrase, phraseSpeed);
                StartCoroutine(currentCoroutine);
            }
            // Else, just display the plain text
            else
            {
                diagBox.phraseNoPhoto.text = phrase;
            }
        }
    }

    // Disable all option boxes
    public void disableOptions(DAS_DialogueBox diagBox)
    {
        diagBox.option1Box.SetActive(false);
        diagBox.option2Box.SetActive(false);
        diagBox.option3Box.SetActive(false);
        diagBox.option4Box.SetActive(false);
        diagBox.option1CenterBox.SetActive(false);
        diagBox.option2CenterBox.SetActive(false);
        diagBox.singleOptionBox.SetActive(false);
    }

    public void executeOption(int optionIndex)
    {
        // If endDialog is checked in the DAS_Option, finish the current dialog
        if (dialogues[currentDialogueIndex].options[optionIndex].endDialog)
        {
            endDialogue();
            if (actionWhenFinish)
            {
                actionWhenFinish.execute();
            }
        }
        else
        {
            // If there is a DAS_Action, finish the dialog but keep active = true untill the action finish its execution
            if (dialogues[currentDialogueIndex].options[optionIndex].executeAction)
            {
                endDialogue();
                active = true;
                dialogueController.dialogueInCourse = true;
                dialogues[currentDialogueIndex].options[optionIndex].action.execute();
            }
            // If openDialogue is checked, call the specific dialogue provided
            if (dialogues[currentDialogueIndex].options[optionIndex].openDialogue)
            {
                callSpecificDialogue(dialogues[currentDialogueIndex].options[optionIndex].dialogue);
            }
        }
        
        
    }
   
}
