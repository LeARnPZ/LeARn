using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisualQueue : QueueStruct
{
    protected override void Start()
    {
        SetDirection();
        iterator = 0;
        
    }
    public void EnqueueVisual(int value)
    {
        iterator = items.Count;
        GameObject newItem = Instantiate(prefab, this.transform);
        newItem.name = $"Block{items.Count}";

        if (iterator > 0)
            newItem.transform.localPosition = items[iterator - 1].transform.localPosition + offset * direction;
        else
            newItem.transform.localPosition = offset * direction + 3 * Vector3.left * direction.x;

        items.Insert(iterator, newItem);
        values.Insert(iterator, value);

        newItem.transform.GetChild(0).GetComponent<TextMeshPro>().text = value.ToString();
        newItem.transform.GetChild(1).GetComponent<TextMeshPro>().text = value.ToString();
    }

    public void DequeueVisual()
    {
        if (items.Count > 0)
        {
            PopItem();
        }
    }

}
