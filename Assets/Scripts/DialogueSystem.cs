using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class DialogueSystem : MonoBehaviour
{
    internal static DialogueSystem Instance { get; private set; }

    [SerializeField] private GameObject _dialogueCollection;
    [SerializeField] private Text _dialogueText;
    [SerializeField] private Transform _buttonGrid;

    private ResponseButton[] _responseButtons;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _responseButtons = new ResponseButton[_buttonGrid.childCount];
        for (int i = 0; i < _responseButtons.Length; i++)
        {
            Transform child = _buttonGrid.transform.GetChild(i);
            _responseButtons[i] = new ResponseButton(child.GetComponent<Button>(), child.GetComponentInChildren<Text>());
        }
    }

    public void StartDialogue(DialogueNode startingNode)
    {
        if (startingNode == null)
        {
            _dialogueCollection.gameObject.SetActive(false);
            return;
        }

        if (!_dialogueCollection.gameObject.activeSelf) _dialogueCollection.gameObject.SetActive(true);

        _dialogueText.text = startingNode.Message;
        for (int i = 0; i < _responseButtons.Length; i++)
        {
            _responseButtons[i].Button.onClick.RemoveAllListeners();
            if (i < startingNode.Responses.Length)
            {
                if (!_responseButtons[i].Button.gameObject.activeSelf) _responseButtons[i].Button.gameObject.SetActive(true);

                _responseButtons[i].ButtonText.text = startingNode.Responses[i].ResponseMessage;
                int localIndex = i;
                _responseButtons[i].Button.onClick.AddListener(() => StartDialogue(startingNode.Responses[localIndex].NextDialogue));
                continue;
            }
            _responseButtons[i].Button.gameObject.SetActive(false);
        }
    }
}

internal struct ResponseButton
{
    internal readonly Button Button;
    internal readonly Text ButtonText;

    internal ResponseButton(Button button, Text buttonText)
    {
        Button = button;
        ButtonText = buttonText;
    }
}
