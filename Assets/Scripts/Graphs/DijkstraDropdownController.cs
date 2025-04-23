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
    private TMP_Dropdown tmp_drop;

    public void OnValueChange()
    {
        dijkstra.MarkPath(tmp_drop.value + 1);
    }

    private void Start()
    {
        tmp_drop = dropdown.GetComponent<TMP_Dropdown>();

        string algorithm = PlayerPrefs.GetString("algorithm");
        if (!algorithm.Contains("Dijkstra"))
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        tmp_drop.ClearOptions();
        tmp_drop.interactable = false;
    }

    public void DropdownSetup()
    {
        dijkstra = anim.transform.GetChild(0).GetComponent<DijkstraGraph>();
        numberOfNodes = dijkstra.GetNumberOfNodes();

        List<string> options = new();
        for (int i = 1; i < numberOfNodes; i++)
            options.Add(i.ToString());
        tmp_drop.AddOptions(options);
    }
}
