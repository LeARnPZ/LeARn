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

    protected virtual IEnumerator SearchGraph() { yield return null; }

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

    protected int GetEdgeWeight(GameObject edge)
    {
        return int.Parse(edge.transform.GetChild(0).GetComponent<TextMeshPro>().text);
    }

    protected void InitializeMatrix()
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

    /// MACIERZ S¥SIEDZTWA TWORZONA RÊCZNIE!!!
    protected void CreateMatrix(int version = 0)
    {
        switch (version)
        {
            case 0:
                numberOfNodes = 9; // <-- liczba wierzcho³ków w grafie
                InitializeMatrix(); // <-- utworzenie macierzy wype³nionej false'ami
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

            case 2:
                numberOfNodes = 7; 
                InitializeMatrix(); 
                matrix[0][1] = true; matrix[1][0] = true;
                matrix[0][2] = true; matrix[2][0] = true;
                matrix[1][3] = true; matrix[3][1] = true;
                matrix[1][4] = true; matrix[4][1] = true;
                matrix[2][5] = true; matrix[5][2] = true;
                matrix[2][6] = true; matrix[6][2] = true;
                break;

            case 3:
                numberOfNodes = 7;
                InitializeMatrix();
                matrix[0][1] = true; matrix[1][0] = true;  
                matrix[0][2] = true; matrix[2][0] = true;
                matrix[0][3] = true; matrix[3][0] = true;
                matrix[0][4] = true; matrix[4][0] = true;
                matrix[0][5] = true; matrix[5][0] = true;
                matrix[0][6] = true; matrix[6][0] = true;
                matrix[0][6] = true; matrix[6][0] = true;
                matrix[1][5] = true; matrix[5][1] = true;
                matrix[1][3] = true; matrix[3][1] = true;
                matrix[3][4] = true; matrix[4][3] = true;
                matrix[4][2] = true; matrix[2][4] = true;
                matrix[2][6] = true; matrix[6][2] = true;
                matrix[6][5] = true; matrix[5][6] = true;
                break;

            case 4:
                numberOfNodes = 7;
                InitializeMatrix();
                matrix[0][1] = true; matrix[1][0] = true;
                matrix[1][2] = true; matrix[2][1] = true;
                matrix[1][4] = true; matrix[4][1] = true;
                matrix[2][3] = true; matrix[3][2] = true;
                matrix[2][5] = true; matrix[5][2] = true;
                matrix[2][6] = true; matrix[6][2] = true;
                matrix[3][5] = true; matrix[5][3] = true;
                matrix[5][6] = true; matrix[6][5] = true;
                break;
        }
    }


    protected void CreateNeighborsList()
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

    protected void DrawEdges(bool weights = false)
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            for (int j = i; j < numberOfNodes; j++)
            {
                if (matrix[i][j])
                {
                    GameObject line = new($"{i}-{j}");
                    line.transform.parent = edges.transform;
                    line.transform.localScale = Vector3.one;

                    Vector3 from = nodesList[i].transform.position;
                    Vector3 to = nodesList[j].transform.position;

                    line.AddComponent<LineRenderer>();
                    LineRenderer lr = line.GetComponent<LineRenderer>();
                    lr.SetPosition(0, from);
                    lr.SetPosition(1, to);
                    lr.startWidth = lr.endWidth = 0.1f * this.transform.localScale.x;
                    lr.material.color = Color.white;
                    lr.material.SetFloat("_Glossiness", 0);

                    if (weights)
                    {
                        GameObject weight = new("EdgeWeight");
                        weight.transform.parent = line.transform;
                        weight.transform.localScale = Vector3.one;
                        weight.transform.position = (from + to) / 2;

                        weight.AddComponent<TextMeshPro>();
                        TextMeshPro tmpro = weight.GetComponent<TextMeshPro>();
                        tmpro.text = Random.Range(1, 6).ToString();
                        tmpro.autoSizeTextContainer = true;
                        tmpro.fontSize = 5;
                    }

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
        //matrix.Clear();
        //neighborsList.Clear();

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

    protected virtual void Awake()
    {
        // Wylosowanie wersji grafu oraz utworzenie do niego macierzy i list s¹siedztwa
        int graphVersion = (int)(Random.value * 10) % 5; // <-- po znaku modulo musi byæ liczba dostêpnych wersji grafu
        CreateMatrix(graphVersion);
        CreateNeighborsList();
        
        // Uaktywnienie odpowiedniej wersji grafu oraz pobranie jego wêz³ów i krawêdzi
        graphVersions.transform.GetChild(graphVersion).gameObject.SetActive(true);
        nodes = graphVersions.transform.GetChild(graphVersion).GetChild(0).gameObject;
        edges = graphVersions.transform.GetChild(graphVersion).GetChild(1).gameObject;
    }

    protected virtual void Start()
    {
        // Dodanie wêz³ów do listy i nadanie etykiet
        for (int i = 0; i < numberOfNodes; i++)
        {
            nodesList.Add(nodes.transform.GetChild(i).gameObject);
            nodesList[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
            nodesList[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = i.ToString();
        }

        // Utworzenie krawêdzi ³¹cz¹cych odpowiednie wêz³y i dodanie ich do listy
        DrawEdges();

        isPaused = false;
        Time.timeScale = 1f;
        queueTravel.transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
        queueTravel.transform.GetChild(1).GetComponent<TextMeshPro>().text = "";

        // Uruchomienie animacji
        StartCoroutine(SearchGraph());
    }
}
