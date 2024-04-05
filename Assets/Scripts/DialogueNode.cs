using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Scriptable Objects/Dialogue Node")]
internal class DialogueNode : ScriptableObject
{
    internal string Message => _message;
    [SerializeField] [TextArea(3, 5)]
    private string _message;

    internal ResponseNode[] Responses => _responses;
    [SerializeField] [Tooltip("Do not put over 3 elements or else the game will break (the system is built assuming 3 or less choices)")]
    private ResponseNode[] _responses;
}

[System.Serializable]
internal struct ResponseNode
{
    internal readonly string ResponseMessage => _responseMessage;
    [SerializeField] private string _responseMessage;

    internal readonly DialogueNode NextDialogue => _nextDialogue;
    [SerializeField] private DialogueNode _nextDialogue;
}
