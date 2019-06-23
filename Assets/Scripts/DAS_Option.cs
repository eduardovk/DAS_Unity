//---------------------------------//
// Author: Eduardo Vicenzi Kuhn    //
// Date: 23/06/2019                //
// github.com/eduardovk            //
//---------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DAS_Option : MonoBehaviour
{
    public string phrase;
    [HideInInspector]
    public bool endDialog;
    [HideInInspector]
    public bool openDialogue;
    [HideInInspector]
    public bool executeAction;
    [HideInInspector]
    public DAS_Dialogue dialogue;
    [HideInInspector]
    public DAS_Action action;

}

[CustomEditor(typeof(DAS_Option))]
public class DAS_Option_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DAS_Option script = (DAS_Option)target;

        script.endDialog = EditorGUILayout.Toggle("End Dialogue", script.endDialog);
        if (!script.endDialog)
        {
            script.openDialogue = EditorGUILayout.Toggle("Open Dialogue", script.openDialogue);
            if (script.openDialogue)
            {
                script.dialogue = EditorGUILayout.ObjectField("Dialgoue", script.dialogue, typeof(DAS_Dialogue), true) as DAS_Dialogue;
            }

            script.executeAction = EditorGUILayout.Toggle("Execute Action", script.executeAction);
            if (script.executeAction)
            {
                script.action = EditorGUILayout.ObjectField("Action", script.action, typeof(DAS_Action), true) as DAS_Action;
            }
        }
        
    }
}
