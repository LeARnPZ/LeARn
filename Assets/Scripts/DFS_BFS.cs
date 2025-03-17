using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DFS_BFS : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int numberOfNodes;
    [SerializeField]
    private int startingNode;
    [SerializeField]
    private string searchType;
    [SerializeField]
    private float timeout;

    private List<GameObject> nodes = new();
    private List<GameObject> edges = new();
    private bool[][] matrix;
    private bool[] visited;

    int MarkEdges(int node, Color color)
    {
        int count = 0;
        for (int i = 0; i < numberOfNodes; i++)
        {
            GameObject line = GameObject.Find($"{node}-{i}");
            if (line == null)
                line = GameObject.Find($"{i}-{node}");
            if (line != null)
            {
                line.GetComponent<Renderer>().material.color = color;
                count++;
            }
        }
        return count;
    }

    IEnumerator DFS(int start)
    {
        yield return new WaitForSeconds(timeout);

        List<int> nodesToVisit = new() { start };

        while (nodesToVisit.Count > 0)
        {
            // Go to the next node on the list to visit
            int n = nodesToVisit.First();
            var node = nodes[n];
            nodesToVisit.Remove(n);
            node.transform.localScale = 2 * Vector3.one;
            yield return new WaitForSeconds(timeout);

            // Set the node as visited
            visited[n] = true;
            node.GetComponent<Renderer>().material.color = Color.green;
            yield return new WaitForSeconds(timeout);

            // Check all neighbors of the current node
            MarkEdges(n, Color.red);
            for (int i = numberOfNodes - 1; i >= 0; i--)
            {
                if (matrix[n][i] || matrix[i][n])
                { 
                    // If the neighbor was not visited, add it to the visit list (if does not exist yet)
                    if (!visited[i])
                        if (!nodesToVisit.Contains(i))
                            nodesToVisit = nodesToVisit.Prepend(i).ToList();
                }
            }
            yield return new WaitForSeconds(timeout);

            node.transform.localScale = Vector3.one;
        }
    }

    IEnumerator BFS(int start)
    {
        yield return new WaitForSeconds(timeout);

        List<int> nodesToVisit = new() { start };

        while (nodesToVisit.Count > 0)
        {
            // Go to the next node on the list to visit
            int n = nodesToVisit.First();
            GameObject node = nodes[n];
            nodesToVisit.Remove(n);
            node.transform.localScale = 2 * Vector3.one;
            yield return new WaitForSeconds(timeout);

            // Set the node as visited
            visited[n] = true;
            node.GetComponent<Renderer>().material.color = Color.green;
            yield return new WaitForSeconds(timeout);

            // Check all neighbors of the current node
            MarkEdges(n, Color.red);
            for (int i = 0; i < numberOfNodes; i++)
            {
                if (matrix[n][i] || matrix[i][n])
                {
                    // If the neighbor was not visited, add it to the visit list (if does not exist yet)
                    if (!visited[i])
                        if (!nodesToVisit.Contains(i))
                            nodesToVisit = nodesToVisit.Append(i).ToList();
                }
            }
            yield return new WaitForSeconds(timeout);

            node.transform.localScale = Vector3.one;
        }
    }

    void Start()
    {
        visited = new bool[numberOfNodes];
        matrix = new bool[numberOfNodes][];
        for (int i = 0; i < numberOfNodes; i++)
            matrix[i] = new bool[numberOfNodes];
        for (int i = 0; i < numberOfNodes; i++)
        {
            nodes.Add(GameObject.Find($"Node{i}"));
            nodes[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = (i+1).ToString();
            nodes[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = (i+1).ToString();
        }

        //// Create sphere objects that represent the nodes
        //for (int i = 0; i < numberOfNodes; i++)
        //{
        //    Vector3 position = new Vector3(-numberOfNodes/2 + i, )
        //    nodes.Add(Instantiate(prefab, );
        //    nodes[i].name = $"Node{i}";
        //    nodes[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
        //}

        // Add edges to the matrix
        matrix[0][1] = true;
        matrix[0][4] = true;
        matrix[1][2] = true;
        matrix[2][3] = true;
        matrix[2][4] = true;
        matrix[3][7] = true;
        matrix[4][5] = true;
        matrix[4][7] = true;
        matrix[5][6] = true;
        matrix[7][8] = true;

        // Draw lines that connect the nodes
        for (int i = 0; i < numberOfNodes; i++)
        {
            for (int j = 0; j < numberOfNodes; j++)
            {
                GameObject line;
                Vector3 from, to;

                if (matrix[i][j])
                {
                    from = nodes[i].transform.position;
                    to = nodes[j].transform.position;
                    line = new() { name = $"{i}-{j}" };

                }
                else continue;

                line.transform.position = from;
                line.AddComponent<LineRenderer>();
                LineRenderer lr = line.GetComponent<LineRenderer>();
                lr.SetPosition(0, from);
                lr.SetPosition(1, to);
                lr.startWidth = 0.1f;
                lr.endWidth = 0.1f;
                line.GetComponent<Renderer>().material.color = Color.white;
                edges.Add(line);
            }
        }

        if (searchType == "DFS")
            StartCoroutine(DFS(startingNode));
        else if (searchType == "BFS")
            StartCoroutine(BFS(startingNode));
    }

    void Update()
    {
        
    }
}
