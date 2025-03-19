using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS : Graphs
{
    [SerializeField]
    private GraphStack graphStack;

    protected override IEnumerator SearchGraph()
    {
        yield return new WaitForSeconds(timeout);

        Stack<int> stack = new();
        stack.Push(startingNode);
        graphStack.PushVisual(startingNode);
        StartCoroutine(ChangeColor(nodesList[startingNode], Color.yellow));
        yield return new WaitForSeconds(timeout);
        List<int> visited = new();

        while (stack.Count > 0)
        {
            // Przejœcie do kolejnego wêz³a
            int n = stack.Pop();
            graphStack.PopVisual();
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            // Oznaczenie wêz³a jako odwiedzonego
            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], Color.green));
            yield return new WaitForSeconds(timeout);

            // Przeszukanie s¹siadów wêz³a
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, Color.red));
                }
            }
            yield return new WaitForSeconds(timeout);
            
            // Dodanie nieodwiedzonych s¹siadów do stosu
            foreach (int nb in neighborsList[n])
            { 
                if (!visited.Contains(nb) && !stack.Contains(nb))
                {
                    StartCoroutine(ChangeColor(nodesList[nb], Color.yellow));
                    stack.Push(nb);
                    graphStack.PushVisual(nb);
                }
            }
            yield return new WaitForSeconds(timeout);

            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, Color.white));
                }
            }

            StartCoroutine(ChangeSize(nodesList[n], Vector3.one));
            yield return new WaitForSeconds(timeout);
        }
    }
}
