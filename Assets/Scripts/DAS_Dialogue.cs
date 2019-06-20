using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DAS_Dialogue : MonoBehaviour
{

    public string[] phrases;
    public DAS_Option[] options;
    public bool lastOne;
    public bool inheritStyle = true;
    
    public string title;
    public Sprite photo;

    public bool jumpToDialogue;
    public DAS_Dialogue targetDialogue;

   

}

//[CustomEditor(typeof(DAS_Dialogue))]
//public class DAS_DialogueEditor : Editor
//{
//    override public void OnInspectorGUI()
//    {
//        var dialogueScript = target as DAS_Dialogue;
//        dialogueScript.inheritStyle = GUILayout.Toggle(dialogueScript.inheritStyle, "Inherit Sytle");
//        if (!dialogueScript.inheritStyle)
//        {
//            dialogueScript.title = EditorGUILayout.TextField("Title");
//        }
//    }
//}
