//---------------------------------//
// Author: Eduardo Vicenzi Kuhn    //
// Date: 23/06/2019                //
// github.com/eduardovk            //
//---------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DAS_Dialogue : MonoBehaviour
{
    public bool lastOne;

    public string[] phrases;

    public DAS_Option[] options;

    [HideInInspector]
    public bool animatePhrases = true;
    [HideInInspector]
    public bool playTypingSound = true;

    [HideInInspector]
    public bool inheritStyle = true;
    [HideInInspector]
    public string title;
    [HideInInspector]
    public Sprite photo;

    [HideInInspector]
    public bool jumpToDialogue;
    [HideInInspector]
    public DAS_Dialogue targetDialogue;

    [HideInInspector]
    public bool playSpecificSound;
    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
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

[CustomEditor(typeof(DAS_Dialogue))]
public class DAS_Dialogue_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DAS_Dialogue script = (DAS_Dialogue)target;

        script.inheritStyle = EditorGUILayout.Toggle("Inherit Style", script.inheritStyle);
        if (!script.inheritStyle)
        {
            script.title = EditorGUILayout.TextField("Title", script.title);
            script.photo = EditorGUILayout.ObjectField("Photo", script.photo, typeof(Sprite), true) as Sprite;
        }

        script.playSpecificSound = EditorGUILayout.Toggle("Play Specific Sound", script.playSpecificSound);
        if (script.playSpecificSound)
        {
            script.audioSource = EditorGUILayout.ObjectField("Audio Source", script.audioSource, typeof(AudioSource), true) as AudioSource;
            script.specificSound = EditorGUILayout.ObjectField("Specific Sound", script.specificSound, typeof(AudioClip), true) as AudioClip;
        }

        script.animatePhrases = EditorGUILayout.Toggle("Animate Phrases", script.animatePhrases);
        if (script.animatePhrases)
        {
            script.playTypingSound = EditorGUILayout.Toggle("Play Typing Sound", script.playTypingSound);
        }


        script.jumpToDialogue = EditorGUILayout.Toggle("Jump To Dialogue", script.jumpToDialogue);
        if (script.jumpToDialogue)
        {
            script.targetDialogue = EditorGUILayout.ObjectField("Target Dialgoue", script.targetDialogue, typeof(DAS_Dialogue), true) as DAS_Dialogue;
        }

    }
}