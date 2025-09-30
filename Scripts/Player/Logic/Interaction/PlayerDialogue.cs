using System;
using UnityEngine;
using JustJJ.DataStructures;

public class PlayerDialogue : PlayerComponent
{
    [SerializeField] private bool _lockPlayerInDialogue = true;

    public Action<DialogueNode> NewDialogueE;
    public Action<DialogueNode> DialogueChoiceE;
    public Action EndDialogueE;

    private DialogueGraph _currentDialogue;

    private bool _isInDialogue = false;

    private PlayerInput _input;

    public override void InitialiseComponent(PlayerManager playerManager)
    {
        base.InitialiseComponent(playerManager);
        _input = playerManager.Input;
    }

    protected override void AddEvents()
    {
        _playerManager.DialogueE += DialogueChecking;
    }

    protected override void RemoveEvents()
    {
        _playerManager.DialogueE -= DialogueChecking;
    }

    private void DialogueChecking()
    {
        if (!_isInDialogue) return;
        if (_input.SkipDialogue)
        {
            if (_currentDialogue.IsMultipleChoice)
            {
                DialogueChoiceE?.Invoke(_currentDialogue.CurrentDialogue);
                _isInDialogue=false;
                _playerManager.SetCursorLocked(false);
            }
            else
            {
                if (_currentDialogue.ProgressDialogue() == null)
                {
                    _isInDialogue = false;
                    _playerManager.SetPlayerLockState(false);
                    EndDialogueE?.Invoke();
                }
                else
                    NewDialogueE?.Invoke(_currentDialogue.CurrentDialogue);
            }
        }
    }

    public void ProgressCurrentDialogue(string next)
    {
        _playerManager.SetCursorLocked(true);
        _currentDialogue.ProgressDialogue(next);
        NewDialogueE?.Invoke(_currentDialogue.CurrentDialogue);
        _isInDialogue = true;
    }

    public void CallNewDialogue(DialogueGraph dialogue)
    {
        _playerManager.SetPlayerLockState(true);
        _currentDialogue = dialogue;
        _isInDialogue = true;
        NewDialogueE?.Invoke(_currentDialogue.StartDialogue());
    }
}
