using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAS_DialogueSystem : MonoBehaviour
{
    public DAS_DialogueBox dialogueBox;
    public DAS_Dialogue[] dialogues;
    public Sprite photo;
    public string title;
    public bool random = false;

    [HideInInspector]
    public int currentDialogueIndex = 0;
    [HideInInspector]
    public int currentPhraseIndex = 0;
    

    public void startDialogue()
    {
        dialogueBox.currentDialogueSystem = this;
        int diagIndex = random ? Random.Range(0, dialogues.Length) : 0;
        buildDialogueBox(dialogueBox, dialogues[diagIndex], 0);
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
                Debug.Log("TODO exit diag");
                // TODO exit dialog
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
        Debug.Log("next diag");
        if(dialogues.Length > (currentDialogueIndex + 1))
        {
            currentDialogueIndex++;
            currentPhraseIndex = 0;
            buildDialogueBox(dialogueBox, dialogues[currentDialogueIndex], currentPhraseIndex);
        }
    }

    public void buildDialogueBox(DAS_DialogueBox dialogueBox, DAS_Dialogue dialogue, int phraseIndex = 0)
    {
        // If current dialogue has options, activate the bigger box
        if (dialogue.options.Length > 0)
        {
            dialogueBox.dialogueWithOptions.SetActive(true);
            dialogueBox.dialogueNoOptions.SetActive(false);
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
            diagBox.photo.sprite = this.photo;
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
   
}
