using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class SinglesChain
    {
        private SinglesChainNode root;

        public int Digit { get; private set; }

        private SinglesChain(int digit, SinglesChainNode root)
        {
            Digit = digit;
            this.root = root;

            var toAdd = new List<SinglesChainNode>();
            var toAddColours = new List<int>();
            toAdd.Add(root);
            toAddColours.Add(0);
            while(toAdd.Count > 0)
            {
                var current = toAdd[0];
                var currentColour = toAddColours[0];
                toAdd.Remove(current);
                toAddColours.RemoveAt(0);
                current.Colour = currentColour;
                foreach(var next in current.Connections)
                {
                    if(next.Colour == -1)
                    {
                        toAdd.Add(next);
                        toAddColours.Add((currentColour + 1) % 2);
                    }
                }
            }
        }

        public List<SinglesChainNode> Get()
        {
            var l = new List<SinglesChainNode>();
            var toAdd = new HashSet<SinglesChainNode>();
            toAdd.Add(root);
            while (toAdd.Count > 0)
            {
                var current = toAdd.First();
                toAdd.Remove(current);
                l.Add(current);
                foreach(var next in current.Connections)
                {
                    if (!l.Contains(next))
                        toAdd.Add(next);
                }
            }
            return l;
        }

        public static List<SinglesChain> Build(Solver solver, int digit)
        {
            int rootIndex = 0;
            digit -= 1;
            HashSet<SinglesChainNode> elements = new HashSet<SinglesChainNode>();
            List<SinglesChainNode> roots = new List<SinglesChainNode>();
            for(var i = 0; i < 9; i++)
            {
                if(solver.RowPossibilities[i][digit].Count == 2)
                {
                    var n1 = new SinglesChainNode() { Colour = -1, Location = new Location(i, solver.RowPossibilities[i][digit].First()) };
                    var n2 = new SinglesChainNode() { Colour = -1, Location = new Location(i, solver.RowPossibilities[i][digit].Last()) };
                    n1.Connections.Add(n2);
                    n2.Connections.Add(n1);
                    elements.Add(n1);
                    elements.Add(n2);
                    n1.ChainId = n2.ChainId = rootIndex;
                    rootIndex++;
                    roots.Add(n1);
                }
            }
            for (var i = 0; i < 9; i++)
            {
                if (solver.ColumnPossibilities[i][digit].Count == 2)
                {
                    var n1 = new SinglesChainNode() { Colour = -1, Location = new Location(solver.ColumnPossibilities[i][digit].First(), i) };
                    var n2 = new SinglesChainNode() { Colour = -1, Location = new Location(solver.ColumnPossibilities[i][digit].Last(), i) };
                    foreach(var element in elements)
                    {
                        if(n1.Location.Equals(element.Location))
                        {
                            n1 = element;
                        }
                        if(n2.Location.Equals(element.Location))
                        {
                            n2 = element;
                        }
                    }

                    if(n1.ChainId != n2.ChainId)
                    {
                        if(n1.ChainId == -1)
                        {
                            n1.ChainId = n2.ChainId;
                        }
                        else if(n2.ChainId == -1)
                        {
                            n2.ChainId = n1.ChainId;
                        }
                        else
                        {
                            foreach(var root in roots)
                            {
                                if(root.ChainId == n2.ChainId)
                                {
                                    roots.Remove(root);
                                    break;
                                }
                            }
                            n2.UpdateChainId(n1.ChainId);
                        }
                    }
                    n1.Connections.Add(n2);
                    n2.Connections.Add(n1);

                    elements.Add(n1);
                    elements.Add(n2);
                    if (n1.ChainId == -1)
                    {
                        n1.ChainId = n2.ChainId = rootIndex++;
                        roots.Add(n1);
                    }
                }
                if (solver.BoxPossibilities[i][digit].Count == 2)
                {
                    var n1 = new SinglesChainNode() { Colour = -1, Location = Location.FromBox(i, solver.BoxPossibilities[i][digit].First()) };
                    var n2 = new SinglesChainNode() { Colour = -1, Location = Location.FromBox(i, solver.BoxPossibilities[i][digit].Last()) };
                    foreach (var element in elements)
                    {
                        if (n1.Location.Equals(element.Location))
                        {
                            n1 = element;
                        }
                        if (n2.Location.Equals(element.Location))
                        {
                            n2 = element;
                        }
                    }

                    if (n1.ChainId != n2.ChainId)
                    {
                        if (n1.ChainId == -1)
                        {
                            n1.ChainId = n2.ChainId;
                        }
                        else if (n2.ChainId == -1)
                        {
                            n2.ChainId = n1.ChainId;
                        }
                        else
                        {
                            foreach (var root in roots)
                            {
                                if (root.ChainId == n2.ChainId)
                                {
                                    roots.Remove(root);
                                    break;
                                }
                            }
                            n2.UpdateChainId(n1.ChainId);
                        }
                    }
                    n1.Connections.Add(n2);
                    n2.Connections.Add(n1);

                    elements.Add(n1);
                    elements.Add(n2);
                    if (n1.ChainId == -1)
                    {
                        n1.ChainId = n2.ChainId = rootIndex++;
                        roots.Add(n1);
                    }
                }
            }

            var chains = new List<SinglesChain>();
            foreach(var root in roots)
            {
                var chain = new SinglesChain(digit + 1, root);
                chains.Add(chain);
            }
            return chains;
        }
    }

    public class SinglesChainNode
    {
        public int Colour { get; set; }
        public Location Location { get; set; }
        public HashSet<SinglesChainNode> Connections { get; private set; } = new HashSet<SinglesChainNode>();

        public int ChainId { get; set; } = -1;

        public void UpdateChainId(int newId)
        {
            ChainId = newId;
            foreach(var node in Connections)
            {
                if(node.ChainId != newId)
                {
                    node.UpdateChainId(newId);
                }
            }
        }

        public void FlipColours()
        {
            FlipColours(new HashSet<SinglesChainNode>());
        }

        private void FlipColours(HashSet<SinglesChainNode> flipped)
        {
            Colour = (Colour + 1) % 2;
            flipped.Add(this);
            foreach (var node in Connections)
            {
                if (!flipped.Contains(node))
                {
                    node.FlipColours(flipped);
                }
            }
        }

        public override string ToString()
        {
            return Location.ToString() + " (" + Colour + ")";
        }
    }
}
