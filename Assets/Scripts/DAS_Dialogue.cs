using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DAS_Dialogue : MonoBehaviour
{

    public string[] phrases;
    public bool animatePhrases = true;
    public bool playTypingSound = true;
    public DAS_Option[] options;
    public bool lastOne;
    public bool inheritStyle = true;
    
    public string title;
    public Sprite photo;

    public bool jumpToDialogue;
    public DAS_Dialogue targetDialogue;

    public bool playSpecificSound = false;
    public AudioSource audioSource;
    public AudioClip specificSound;

    public void playSound()
    {
        if (playSpecificSound && audioSource && specificSound)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(specificSound);
        }
    }
   

}

