using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public DAS_DialogueSystem diagSystemCarlito;
    public DAS_DialogueSystem diagSystemSandy;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            diagSystemCarlito.startDialogue();
        }else if (Input.GetKeyDown(KeyCode.G))
        {
            diagSystemSandy.startDialogue();
        }
    }
}
