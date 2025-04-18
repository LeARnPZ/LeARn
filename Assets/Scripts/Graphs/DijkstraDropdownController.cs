using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraDropdownController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string algorithm = PlayerPrefs.GetString("algorithm");
        if (algorithm.Contains("Dijkstra"))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
