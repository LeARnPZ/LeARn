using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DijkstraDropdownController : MonoBehaviour
{
    [SerializeField]
    private GameObject dropdown;
    [SerializeField]
    private GameObject anim;

    private int numberOfNodes;
    private DijkstraGraph dijkstra;
    private bool isSet = false;

    public void OnValueChange()
    {
        int value = dropdown.GetComponent<TMP_Dropdown>().value;
        dijkstra.MarkPath(value);
    }

    private void Start()
    {
        string algorithm = PlayerPrefs.GetString("algorithm");
        if (!algorithm.Contains("Dijkstra"))
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        dropdown.GetComponent<TMP_Dropdown>().options.Clear();
        dropdown.GetComponent<TMP_Dropdown>().interactable = false;
    }

    public void DropdownSetup()
    {
        dijkstra = anim.transform.GetChild(0).GetComponent<DijkstraGraph>();

        numberOfNodes = dijkstra.GetNumberOfNodes();

        List<string> options = new();
        for (int i = 1; i < numberOfNodes; i++)
            options.Add(i.ToString());
        dropdown.GetComponent<TMP_Dropdown>().AddOptions(options);

        isSet = true;
    }

    private void Update()
    {
        if (isSet)
        {
            if (dijkstra.IsFinished())
                dropdown.GetComponent<TMP_Dropdown>().interactable = true;
            else
                dropdown.GetComponent<TMP_Dropdown>().interactable = false;
        }
    }
}
