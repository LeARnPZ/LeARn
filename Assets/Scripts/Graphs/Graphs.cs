using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Graphs : MonoBehaviour
{
    [Header("Ustawienia animacji")]
    //[SerializeField]
    protected int numberOfNodes;
    [SerializeField]
    protected int startingNode;

    [Header("Czas trwania")]
    [SerializeField]
    protected float timeout;
    [SerializeField]
    protected float animDuration;

    [Header("Obiekty-rodzice")]
    //[SerializeField]
    //protected GameObject nodes;
    //[SerializeField]
    //protected GameObject edges;
    [SerializeField]
    protected GameObject graphVersions;
    [SerializeField]
    protected GameObject queueTravel;

    protected List<List<bool>> matrix = new();
    protected Dictionary<int, List<int>> neighborsList = new();

    protected GameObject nodes;
    protected GameObject edges;
    protected List<GameObject> nodesList = new();
    protected List<GameObject> edgesList = new();

    protected bool isPaused;

    protected abstract IEnumerator SearchGraph();

    protected IEnumerator ChangeColor(GameObject gameObject, Color newColor)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Color currentColor = renderer.material.color;

        float elapsedTime = 0f;

        while (elapsedTime < animDuration)
        {
            renderer.material.color = Color.Lerp(currentColor, newColor, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = newColor;
    }

    protected IEnumerator ChangeSize(GameObject gameObject, Vector3 newScale)
    {
        Vector3 currentScale = gameObject.transform.localScale;

        float elapsedTime = 0f;

        while (elapsedTime < animDuration)
        {
            gameObject.transform.localScale = Vector3.Lerp(currentScale, newScale, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localScale = newScale;
    }

    private void InitializeMatrix()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            matrix.Add(new());
            for (int j = 0; j < numberOfNodes; j++)
            {
                matrix[i].Add(false);
            }
        }
    }

    /// MACIERZ S�SIEDZTWA TWORZONA R�CZNIE!!!
    private void CreateMatrix(int version = 0)
    {
        switch (version)
        {
            case 0:
                numberOfNodes = 9; // <-- liczba wierzcho�k�w w grafie
                InitializeMatrix(); // <-- utworzenie macierzy wype�nionej false'ami
                matrix[0][1] = true; matrix[1][0] = true;
                matrix[0][4] = true; matrix[4][0] = true;
                matrix[1][2] = true; matrix[2][1] = true;
                matrix[2][3] = true; matrix[3][2] = true;
                matrix[2][4] = true; matrix[4][2] = true;
                matrix[3][7] = true; matrix[7][3] = true;
                matrix[4][5] = true; matrix[5][4] = true;
                matrix[4][7] = true; matrix[7][4] = true;
                matrix[5][6] = true; matrix[6][5] = true;
                matrix[7][8] = true; matrix[8][7] = true;
                break;

            case 1:
                numberOfNodes = 8;
                InitializeMatrix();
                matrix[0][1] = true; matrix[1][0] = true;
                matrix[0][2] = true; matrix[2][0] = true;
                matrix[1][2] = true; matrix[2][1] = true;
                matrix[2][3] = true; matrix[3][2] = true;
                matrix[2][4] = true; matrix[4][2] = true;
                matrix[2][5] = true; matrix[5][2] = true;
                matrix[2][7] = true; matrix[7][2] = true;
                matrix[3][7] = true; matrix[7][3] = true;
                matrix[4][6] = true; matrix[6][4] = true;
                matrix[5][6] = true; matrix[6][5] = true;
                matrix[5][7] = true; matrix[7][5] = true;
                matrix[6][7] = true; matrix[7][6] = true;
                break;
        }
    }
    

    private void CreateNeighborsList()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            neighborsList.Add(i, new List<int>());
            for (int j = 0; j < numberOfNodes; j++)
            {
                if (matrix[i][j])
                {
                    neighborsList[i].Add(j);
                }
            }
        }
    }

    private void DrawEdges()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            for (int j = 0; j < numberOfNodes; j++)
            {
                if (matrix[i][j])
                {
                    GameObject line = new();
                    line.transform.parent = edges.transform;
                    line.name = $"{i}-{j}";

                    Vector3 from = nodesList[i].transform.position;
                    Vector3 to = nodesList[j].transform.position;

                    line.AddComponent<LineRenderer>();
                    LineRenderer lr = line.GetComponent<LineRenderer>();
                    lr.SetPosition(0, from);
                    lr.SetPosition(1, to);
                    lr.startWidth = lr.endWidth = 0.1f * this.transform.localScale.x;
                    lr.material.color = Color.white;
                    lr.material.SetFloat("_Glossiness", 0);

                    edgesList.Add(line);
                }
            }
        }
    }

    public void Restart()
    {
        StopAllCoroutines();

        foreach (GameObject node in nodesList)
        {
            node.transform.localScale = Vector3.one; 
            node.GetComponent<Renderer>().material.color = Color.white;
        }

        foreach (GameObject edge in edgesList)
        {
            Destroy(edge);
        }

        edgesList.Clear();
        matrix.Clear();
        neighborsList.Clear();

        Start();
    }

    public bool getPaused()
    {
        return isPaused;
    }

    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void addText(string text)
    {

        if (queueTravel.transform.GetChild(0).GetComponent<TextMeshPro>().text.Equals(""))
        {
            queueTravel.transform.GetChild(0).GetComponent<TextMeshPro>().text = text;
            queueTravel.transform.GetChild(1).GetComponent<TextMeshPro>().text = text;
        }
        else
        {
            queueTravel.transform.GetChild(0).GetComponent<TextMeshPro>().text += $", {text}";
            queueTravel.transform.GetChild(1).GetComponent<TextMeshPro>().text += $", {text}";
        }
    }

    protected void Start()
    {
        int graphVersion = (int)(Random.value * 10) % 2; // <-- po znaku modulo musi by� liczba stworzonych wersji grafu
        CreateMatrix(graphVersion);
        CreateNeighborsList();

        // Uaktywnienie odpowiedniej wersji grafu oraz pobranie jego w�z��w i kraw�dzi
        graphVersions.transform.GetChild(graphVersion).gameObject.SetActive(true);
        nodes = graphVersions.transform.GetChild(graphVersion).GetChild(0).gameObject;
        edges = graphVersions.transform.GetChild(graphVersion).GetChild(1).gameObject;

        // Dodanie w�z��w do listy i nadanie etykiet
        for (int i = 0; i < numberOfNodes; i++)
        {
            nodesList.Add(nodes.transform.GetChild(i).gameObject);
            nodesList[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
            nodesList[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = i.ToString();
        }

        // Utworzenie kraw�dzi ��cz�cych odpowiednie w�z�y i dodanie ich do listy
        DrawEdges();

        isPaused = false;
        Time.timeScale = 1f;
        queueTravel.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
        queueTravel.transform.GetChild(1).GetComponent<TextMeshPro>().text = "";

        // Uruchomienie animacji
        StartCoroutine(SearchGraph());
    }
}
