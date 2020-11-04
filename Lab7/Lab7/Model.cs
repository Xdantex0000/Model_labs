using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab7
{
    internal class Model
    {
        public List<Element> list { get; set; }

        public Model(List<Element> list)
        {
            this.list = list;
        }

        public void simulate(int coutIterations, bool printState)
        {
            InArcCalculate();
            SortPriority();

            for (int i = 0; i < coutIterations; i++) // Loop till i equals countIterations
            {
                List<Transition> transitions = new List<Transition>();

                for (int j = list.Count - 1; j >= 0; j--) // Loop by all elements in list
                {
                    Position element = null;
                    if (list[j].GetType() == typeof(Position))
                        element = (Position)list[j];

                    bool transit = true;
                    if (element != null) // If position
                    {
                        Transition nextElement = null;
                        bool probabilitized = false;
                        for (int z = 0; z < element.OutArcs.Count; z++) // Loop by all outArc list for position "Element"
                        {
                            bool transitable = true;
                            probabilitized = element.OutArcs[z].Probability == 1.0 ? false : true;
                            nextElement = (Transition)element.OutArcs[z].NextElement;
                            foreach (var el in nextElement.InArcs)
                            {
                                Position inPos = (Position)el.NextElement;
                                if (el.Multiplicity > inPos.MarkersCount)
                                {
                                    transitable = false;
                                    transit = false;
                                    if (!probabilitized)
                                    {
                                        continue;
                                    }
                                    else break;
                                }
                            }
                            if (transitable && !probabilitized && !transitions.Contains(nextElement))
                            {
                                transitions.Add(nextElement);
                            }
                            if (transit && probabilitized) break;
                        }
                        if (nextElement != null)
                        {
                            if (transit && probabilitized) // Transit on probability
                            {
                                double rand = new Random().NextDouble();
                                double start = 0.0;
                                foreach (var x in element.OutArcs)
                                {
                                    if (rand >= start && rand <= start + x.Probability)
                                    {
                                        Transit((Transition)x.NextElement, printState);
                                        break;
                                    }
                                    start += x.Probability;
                                }
                                break;
                            }
                        }
                    }

                    if (transitions.Count != 0 && j == 0 && transit)
                    {
                        TransitConflict(transitions, printState);
                        break;
                    }
                }
            }
            if (printState)
                printResult();
        }

        public void printResult()
        {
            Console.WriteLine("-----------------Result-----------------");
            foreach (var x in list)
            {
                Position position;
                if (x.GetType() == typeof(Position))
                {
                    position = (Position)x;
                    Console.WriteLine($"{position.Name} has {position.MarkersCount} markers");
                }
            }
            Console.WriteLine();
            foreach (var x in list)
            {
                Transition position;
                if (x.GetType() == typeof(Transition))
                {
                    position = (Transition)x;
                    Console.WriteLine($"{position.Name} has quantity: {position.Quantity}");
                }
            }
            Console.WriteLine();
            foreach (var x in list)
            {
                Position position;
                if (x.GetType() == typeof(Position))
                {
                    position = (Position)x;
                    var min = int.MaxValue;
                    var max = int.MinValue;
                    var avg = 0.0;

                    foreach (var y in position.MarkerHistory)
                    {
                        if (y < min)
                            min = y;
                        if (y > max)
                            max = y;
                        avg += y;
                    }
                    Console.WriteLine($"{position.Name}\n  -min: {min}\n  -max: {max}\n  -avg: {avg / position.MarkerHistory.Count}");
                }
            }
        }

        public void TransitConflict(List<Transition> transitions, bool printState)
        {
            List<Position> positions = new List<Position>();
            List<Transition> transited = new List<Transition>();
            double rand = new Random().NextDouble();

            for (int i = 0; i < transitions.Count; i++)
                for (int j = 0; j < transitions[i].InArcs.Count; j++)
                    if (!positions.Contains(transitions[i].InArcs[j].NextElement))
                        positions.Add((Position)transitions[i].InArcs[j].NextElement);

            List<Transition>[] transitionsByPosition = new List<Transition>[positions.Count];
            for (int i = 0; i < transitionsByPosition.Length; i++)
                transitionsByPosition[i] = new List<Transition>();

            for (int i = 0; i < transitions.Count; i++)
                for (int j = 0; j < transitions[i].InArcs.Count; j++)
                    for (int z = 0; z < positions.Count; z++)
                        if (transitions[i].InArcs[j].NextElement == positions[z] && !transitionsByPosition[z].Contains(transitions[i]))
                            transitionsByPosition[z].Add(transitions[i]);

            for (int i = 0; i < transitionsByPosition.Length; i++)
                if (transitionsByPosition[i].Count > 1)
                {
                    for (int j = 0; j < transitionsByPosition[i].Count; j++)
                    {
                        if (rand >= (double)j / (double)transitionsByPosition[i].Count && rand < (double)(j + 1) / (double)transitionsByPosition[i].Count)
                            Transit(transitionsByPosition[i][j], printState);
                        transited.Add(transitionsByPosition[i][j]);
                    }
                }
                else if (!transited.Contains(transitionsByPosition[i][0]))
                    Transit(transitionsByPosition[i][0], printState);
        }

        public void Transit(Transition transition, bool printState)
        {
            Position position;
            if (printState)
            {
                Console.WriteLine($"{transition.Name.ToUpper()} is transited");
                Console.WriteLine("Out Arg:");
            }
            foreach (var x in transition.OutArcs)
            {
                position = (Position)x.NextElement;
                position.MarkersCount += x.Multiplicity;
                if (printState)
                    Console.WriteLine($"  {position.Name} has {position.MarkersCount} markers");
                position.MarkerHistory.Add(position.MarkersCount);
            }

            if (printState)
                Console.WriteLine("In Arg:");

            foreach (var x in transition.InArcs)
            {
                position = (Position)x.NextElement;
                position.MarkersCount -= x.Multiplicity;
                if (printState)
                    Console.WriteLine($"  {position.Name} has {position.MarkersCount} markers");
                position.MarkerHistory.Add(position.MarkersCount);
            }
            transition.Quantity++;
            if (printState)
                Console.WriteLine();
        }

        public void InArcCalculate()
        {
            foreach (var x in list)
            {
                foreach (var y in x.OutArcs)
                {
                    y.NextElement.InArcs.Add(new Arc(y.Multiplicity, x, y.Priority));
                }
            }
        }

        public bool DoesProbEqual(Position position)
        {
            double probability = double.MinValue;

            foreach (var x in position.OutArcs)
                if (x != null)
                    probability = x.Probability;
            if (probability != double.MinValue)
            {
                for (int i = 0; i < position.OutArcs.Count; i++)
                    if (position.OutArcs[i] != null)
                        if (position.OutArcs[i].Probability != probability)
                            return false;
                return true;
            }
            return false;
        }

        public void SortPriority()
        {
            foreach (var x in list)
            {
                x.InArcs.Sort((x, y) => x.Priority.CompareTo(y.Priority));
                x.OutArcs.Sort((x, y) => x.Priority.CompareTo(y.Priority));
            }
        }
    }
}