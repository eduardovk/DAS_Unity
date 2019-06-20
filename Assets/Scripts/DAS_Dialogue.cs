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

