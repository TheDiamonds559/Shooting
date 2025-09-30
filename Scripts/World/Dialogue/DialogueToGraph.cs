using System.Collections.Generic;
using System.Linq;

namespace JustJJ.DataStructures
{
    public static class DialogueToGraph
    {
        public static DialogueGraph DialogueScriptToGraph(Dialogue dialogue)
        {
            DialogueGraph graph = new DialogueGraph();
            List<DialogueNode> nodes = new List<DialogueNode>();

            foreach (var d in dialogue.Data)
            {
                DialogueNode node = new DialogueNode(d.Name, d.Text);

                nodes.Add(node);
            }

            for (int i = 0;  i < nodes.Count; i++)
            {
                foreach(var n in dialogue.Data[i].Choices)
                {
                    if (n == null) continue;
                    nodes[i].Choices.Add(nodes.Where(e=>e.Name==n).FirstOrDefault());
                }

                graph.AddNode(nodes[i]);
            }

            return graph;
        }
    }
}
