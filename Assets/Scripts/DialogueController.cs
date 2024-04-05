using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class DialogueController : MonoBehaviour
{
    [SerializeField] private DialogueNode _startingDialogueNode;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DialogueSystem.Instance.StartDialogue(_startingDialogueNode);
        }
    }
}
