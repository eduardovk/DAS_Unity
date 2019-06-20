using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAS_DialogueSystem : MonoBehaviour
{
    public DAS_DialogueController dialogueController;
    public DAS_DialogueBox dialogueBox;
    public DAS_Dialogue[] dialogues;
    public Sprite photo;
    public string title;
    public bool random = false;

    [HideInInspector]
    public int currentDialogueIndex = 0;
    [HideInInspector]
    public int currentPhraseIndex = 0;
    [HideInInspector]
    public bool active = false;

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
            dialogueBox.showUpAnimation();
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
                dialogueBox.showUpAnimation();
            }
        }
    }

    public void callSpecificDialogue(DAS_Dialogue dialogue)
    {
        active = true;
        dialogueController.dialogueInCourse = true;
        int diagIndex = 0;
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
                dialogueBox.option2Box.SetActive(true);
                dialogueBox.option3Box.SetActive(true);
                dialogueBox.option2Text.text = dialogue.options[0].phrase;
                dialogueBox.option3Text.text = dialogue.options[1].phrase;
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
        if (photo)
        {
            diagBox.photo.sprite = photo;
            diagBox.photo.enabled = true;
            diagBox.titlePhoto.enabled = true;
            diagBox.titleNoPhoto.enabled = false;
            diagBox.titlePhoto.text = title;
            diagBox.phrasePhoto.enabled = true;
            diagBox.phraseNoPhoto.enabled = false;
            diagBox.phrasePhoto.text = phrase;
        }
        else
        {
            diagBox.photo.enabled = false;
            diagBox.titlePhoto.enabled = false;
            diagBox.titleNoPhoto.enabled = true;
            diagBox.titleNoPhoto.text = title;
            diagBox.phrasePhoto.enabled = false;
            diagBox.phraseNoPhoto.enabled = true;
            diagBox.phraseNoPhoto.text = phrase;
        }
    }

    public void disableOptions(DAS_DialogueBox diagBox)
    {
        diagBox.option1Box.SetActive(false);
        diagBox.option2Box.SetActive(false);
        diagBox.option3Box.SetActive(false);
        diagBox.option4Box.SetActive(false);
        diagBox.singleOptionBox.SetActive(false);
    }

    public void executeOption(int optionIndex)
    {
        if (dialogues[currentDialogueIndex].options[optionIndex].executeAction)
        {
            endDialogue();
            active = true;
            dialogueController.dialogueInCourse = true;
            dialogues[currentDialogueIndex].options[optionIndex].action.execute();
        }

        if (dialogues[currentDialogueIndex].options[optionIndex].openDialogue)
        {
            callSpecificDialogue(dialogues[currentDialogueIndex].options[optionIndex].dialogue);
        }
        
    }
   
}
