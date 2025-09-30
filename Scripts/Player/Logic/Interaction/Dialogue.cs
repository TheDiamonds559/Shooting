using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Mousse/Dialogue")]
public class Dialogue : ScriptableObject
{
    [field:SerializeField] public DialogueData[] Data { get; private set; }
    [Serializable]
    public class DialogueData
    {
        [field:SerializeField] public string Name {  get; private set; }
        [field:SerializeField] public string Text { get; private set; }
        [field:SerializeField] public string[] Choices { get; private set; }
    }
}
