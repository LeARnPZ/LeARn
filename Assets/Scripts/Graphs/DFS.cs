using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS : Graphs
{
    protected override IEnumerator SearchGraph()
    {
        yield return new WaitForSeconds(timeout);

        Stack<int> stack = new();
        stack.Push(startingNode);
        List<int> visited = new();

        while (stack.Count > 0)
        {
            // Przej�cie do kolejnego w�z�a
            int n = stack.Pop();
            StartCoroutine(ChangeSize(nodesList[n], 1.5f * Vector3.one));
            yield return new WaitForSeconds(timeout);

            // Oznaczenie w�z�a jako odwiedzonego
            visited.Add(n);
            StartCoroutine(ChangeColor(nodesList[n], Color.green));
            yield return new WaitForSeconds(timeout);

            // Przeszukanie s�siad�w w�z�a
            foreach (GameObject edge in edgesList)
            {
                if (edge.name.Contains($"{n}-") || edge.name.Contains($"-{n}"))
                {
                    StartCoroutine(ChangeColor(edge, Color.red));
                }
            }
            yield return new WaitForSeconds(timeout);
            
            // Dodanie nieodwiedzonych s�siad�w do stosu
            foreach (int nb in neighborsList[n])
            { 
                if (!visited.Contains(nb) && !stack.Contains(nb))
                {
                    StartCoroutine(ChangeColor(nodesList[nb], Color.yellow));
                    stack.Push(nb);
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
