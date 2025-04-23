using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DijkstraDropdownController : MonoBehaviour
{
    [SerializeField]
    private GameObject dropdown;
    [SerializeField]
    private GameObject anim;

    private int numberOfNodes;
    private int currentValue;
    private DijkstraGraph dijkstra;
    private TMP_Dropdown tmp_drop;

    public void OnValueChange()
    {
        SetColor(tmp_drop.options[currentValue], "#FFFFFF"); // white

        currentValue = tmp_drop.value;
        dijkstra.MarkPath(currentValue + 1);

        SetColor(tmp_drop.options[currentValue], "#B399ED"); // purple
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

        currentValue = 0;
        SetColor(tmp_drop.options[currentValue], "#B399ED"); // purple
    }

    private void SetColor(TMP_Dropdown.OptionData option, string hexColor)
    {
        option.text = $"<color={hexColor}>{tmp_drop.options.IndexOf(option) + 1}</color>";
    }
}
