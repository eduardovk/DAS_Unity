//---------------------------------//
// Author: Eduardo Vicenzi Kuhn    //
// Date: 23/06/2019                //
// github.com/eduardovk            //
//---------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAS_DialogueBox : MonoBehaviour
{
    [HideInInspector]
    public DAS_DialogueSystem currentDialogueSystem;
    public string showUpAnimTrigger = "riseup";

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
    public GameObject option1CenterBox;
    public Text option1CenterText;
    public GameObject option2CenterBox;
    public Text option2CenterText;

    // Single-Option Variables
    public GameObject singleOptionBox;
    public Text singleOptionText;

    // Sound Variables
    public bool hasSound = false;
    public AudioSource dialogueBoxAudioSource;
    public AudioSource typingAudioSource;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void nextPhrase()
    {
        currentDialogueSystem.nextPhrase();
    }

    public void optionClick(int optionIndex)
    {
        currentDialogueSystem.executeOption(optionIndex);
    }

    public void showUpAnimation()
    {
        animator.SetTrigger(showUpAnimTrigger);
    }

    public void playDialogueBoxSound()
    {
        if (hasSound && dialogueBoxAudioSource)
        {
            dialogueBoxAudioSource.Play();
        }
    }

    public void playTypeSound()
    {
        if (hasSound && typingAudioSource)
        {
            typingAudioSource.loop = true;
            typingAudioSource.Play();
        }
    }

    public void stopTypeSound()
    {
        if (hasSound && typingAudioSource)
        {
            typingAudioSource.loop = false;
        }
    }



}
