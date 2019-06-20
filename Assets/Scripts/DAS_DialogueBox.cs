using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAS_DialogueBox : MonoBehaviour
{
    public DAS_DialogueSystem currentDialogueSystem;

    // DialoguePanel
    public GameObject dialogueNoOptions;
    public GameObject dialogueWithOptions;

    // Dialogue variables
    public Text titleNoPhoto;
    public Text phraseNoPhoto;

    // Dialogue with photo variables
    public Image photo;
    public Text titlePhoto;
    public Text phrasePhoto;

    // Options Variables
    public GameObject option1Box;
    public Text option1Text;
    public GameObject option2Box;
    public Text option2Text;
    public GameObject option3Box;
    public Text option3Text;
    public GameObject option4Box;
    public Text option4Text;

    // Single-Option Variables
    public GameObject singleOptionBox;
    public Text singleOptionText;

    public void nextPhrase()
    {
        currentDialogueSystem.nextPhrase();
    }

    public void optionClick(int optionIndex)
    {
        currentDialogueSystem.executeOption(optionIndex);
    }



}
