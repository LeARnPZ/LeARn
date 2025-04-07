using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DFSGraph : Graphs
{
    [SerializeField]
    private GraphStack graphStack;

    protected override IEnumerator SearchGraph()
    {
        yield return new WaitForSeconds(timeout);

        Stack<int> stack = new();
        stack.Push(startingNode);
        graphStack.PushVisual(startingNode);
        StartCoroutine(ChangeColor(nodesList[startingNode], yellowColor));
        yield return new WaitForSeconds(timeout);
        List<int> visited = new();

        while (stack.Count > 0)
        {
            // Przej�cie do kolejnego w�z�a
            int n = stack.Pop();
            graphStack.PopVisual();
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            // Pomini�cie, je�li by� ju� odwiedzony
            if (visited.Contains(n))
            {
                StartCoroutine(ChangeSize(nodesList[n], Vector3.one));
                yield return new WaitForSeconds(timeout);
                continue;
            }

            // Oznaczenie w�z�a jako odwiedzonego
            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], greenColor));
            addText(n.ToString());
            yield return new WaitForSeconds(timeout);

            // Przeszukanie s�siad�w w�z�a
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, orangeColor));
                }
            }
            yield return new WaitForSeconds(timeout);

            // Dodanie nieodwiedzonych s�siad�w do stosu
            if (neighborsList[n].Any(nb => !visited.Contains(nb)))
            {
                List<int> tmpList = new(neighborsList[n]);
                tmpList.Reverse();
                foreach (int nb in tmpList)
                {
                    if (!visited.Contains(nb))
                    {
                        StartCoroutine(ChangeColor(nodesList[nb], yellowColor));
                        stack.Push(nb);
                        graphStack.PushVisual(nb);
                    }
                }
                yield return new WaitForSeconds(timeout);
            }

            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, blueColor));
                }
            }

            StartCoroutine(ChangeSize(nodesList[n], Vector3.one));
            yield return new WaitForSeconds(timeout);
        }
    }
}
