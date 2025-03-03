using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QueueStruct : Structures
{
    [SerializeField]
    private float speed;

    private IEnumerator AdjustPosition()
    {
        float elapsedTime = 0;
        float animDuration = 0.5f;

        List<Vector3> currentPositions = new();
        List<Vector3> newPositions = new();

        foreach (GameObject item in items)
        {
            currentPositions.Add(item.transform.localPosition);
            newPositions.Add(item.transform.localPosition + offset * Vector3.left);
        }

        while (elapsedTime < animDuration)
        {
            for (int i = 0; i < items.Count; i++)
                items[i].transform.localPosition = Vector3.Lerp(currentPositions[i], newPositions[i], elapsedTime / animDuration);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        for (int i = 0; i < items.Count; i++)
            items[i].transform.localPosition = newPositions[i];
    }

    protected override void SetDirection()
    {
        direction = Vector3.up + Vector3.right;
    }

    public override void PopItem()
    {
        popIndex = 0;
        base.PopItem();

        if (items.Count < 1) return;
        StartCoroutine(AdjustPosition());
    }

    public override void PeekItem()
    {
        popIndex = 0;
        base.PeekItem();
    }
}
