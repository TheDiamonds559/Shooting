using JustJJ.DataStructures;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PlayerUIDialogueBox : PlayerUIComponent
{
    [SerializeField] private float _textDelay = .01f;
    [SerializeField] private float _textClearDelay = 2.0f;

    private TMP_Text _dialogueBox;
    private PlayerDialogue _dialogue;

    private IEnumerator _currentDialogue;
    private IEnumerator _secondaryDialogue;

    private Dialogue _currentDialogueShowing;

    public override void InitialiseComponent(PlayerUIManager playerUIManager)
    {
        _dialogue = playerUIManager.PlayerManager.GetPlayerComponent<PlayerDialogue>();
        base.InitialiseComponent(playerUIManager);
        _dialogueBox = GetComponent<TMP_Text>();
        ClearDialogue();
    }

    protected override void AddEvents()
    {
        _dialogue.NewDialogueE += UpdateDialogueLine;
        _dialogue.EndDialogueE += ClearDialogue;
    }

    protected override void RemoveEvents()
    {
        _dialogue.NewDialogueE -= UpdateDialogueLine;
        _dialogue.EndDialogueE -= ClearDialogue;
    }

    private void UpdateDialogueLine(DialogueNode dialogue)
    {
        string line = dialogue.Text;
        if (_secondaryDialogue == null)
        {
            _secondaryDialogue = TextEffect(line);
            StartCoroutine(_secondaryDialogue);
        }
        else
        {
            StopCoroutine(_secondaryDialogue);
            _secondaryDialogue = TextEffect(line);
            StartCoroutine(_secondaryDialogue);
        }
    }

    //private IEnumerator DialogueEffect()
    //{
    //    ClearDialogue();
    //    while (!_currentDialogueShowing.IsAtEnd())
    //    {
    //        if (_secondaryDialogue != null)
    //            StopCoroutine(_secondaryDialogue);
    //        _secondaryDialogue = TextEffect(_currentDialogueShowing.GetCurrentDialogue());
    //        StartCoroutine(_secondaryDialogue);
    //        yield return new WaitForSeconds(_textDelay * _currentDialogueShowing.GetCurrentDialogue().Length + _textClearDelay);
    //        _currentDialogueShowing.Progress();
    //    }
    //    _currentDialogue = null;
    //    _dialogue.StopDialogue();
    //}

    private IEnumerator TextEffect(string text)
    {
        string fullText = "";
        foreach (char c in text)
        {
            fullText += c;
            _dialogueBox.text = fullText;
            yield return new WaitForSeconds(_textDelay);
        }
        _secondaryDialogue=null;
    }

    public void ClearDialogue()
    {
        _dialogueBox.text = "";
    }


}
