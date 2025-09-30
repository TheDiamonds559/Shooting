using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JustJJ.DataStructures
{
    public class DialogueGraph
    {
        private List<DialogueNode> _nodes;
        public DialogueNode CurrentDialogue { get; private set; }
        
        public DialogueGraph()
        {
            _nodes = new List<DialogueNode>();
        }

        public bool IsMultipleChoice => CurrentDialogue?.Choices.Count > 1;

        public void AddNode(DialogueNode node)
        {
            _nodes.Add(node);
        }

        public DialogueNode StartDialogue()
        {
            CurrentDialogue = _nodes[0];
            return _nodes[0];
        }

        /// <summary>
        /// Assumes no multiple choice
        /// </summary>
        /// <returns></returns>
        public DialogueNode ProgressDialogue()
        {
            if (CurrentDialogue?.Choices.Count == 0)
            {
                return null;
            }
            return CurrentDialogue = CurrentDialogue.Choices[0];
        }

        public DialogueNode ProgressDialogue(string name)
        {
            return CurrentDialogue = CurrentDialogue.Choose(name);
        }
    }

    public class DialogueNode
    {
        public string Name { get; private set; }
        public string Text { get; private set; }
        public List<DialogueNode> Choices { get; private set; }

        public DialogueNode(string name, string text)
        {
            Name=name;
            Text=text;
            Choices= new List<DialogueNode>();
        }

        public DialogueNode Choose(string name)
        {
            return Choices.Where(c =>  c.Name==name).FirstOrDefault();
        }

        public void PrintChoices()
        {
            foreach (DialogueNode node in Choices)
            {
                if (node == null) Debug.Log("The fuck");
                else Debug.Log(node.Name);
            }
        }
    }
}
