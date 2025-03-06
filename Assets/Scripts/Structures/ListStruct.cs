using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListStruct : Structures
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject iteratorObject;

    float animDuration = 0.5f;
    private Vector3 adjustmentVector;

    private IEnumerator AdjustPosition()
    {
        float elapsedTime = 0;

        List<Vector3> currentPositions = new();
        List<Vector3> newPositions = new();

        if (iterator == items.Count - 1) yield break;

        foreach (GameObject item in items)
        {
            currentPositions.Add(item.transform.localPosition);
            if (items.IndexOf(item) > iterator)
                newPositions.Add(item.transform.localPosition + offset * adjustmentVector);
            else
                newPositions.Add(currentPositions.Last());
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

    public IEnumerator MoveIterator(Vector3 moveVector)
    {
        if (moveVector == Vector3.right)
            iterator++;
        else if (moveVector == Vector3.left)
            iterator--;

        float elapsedTime = 0;
        Vector3 currentPosition = iteratorObject.transform.localPosition;
        Vector3 newPosition = currentPosition + moveVector * offset;

        while (elapsedTime < animDuration)
        {
            iteratorObject.transform.localPosition = Vector3.Lerp(currentPosition, newPosition, elapsedTime / animDuration);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

        iteratorObject.transform.localPosition = newPosition;
    }

    public override void AddItem()
    {
        if (items.Count >= maxCount) return;
        base.AddItem();

        adjustmentVector = Vector3.right;
        items[iterator].GetComponent<Rigidbody>().useGravity = false;
        StartCoroutine(AdjustPosition());
        items[iterator].GetComponent<Rigidbody>().useGravity = true;
        StartCoroutine(MoveIterator(Vector3.right));
    }

    public override void PopItem()
    {
        if (items.Count < 1) return;
        base.PopItem();

        iterator--;
        adjustmentVector = Vector3.left;
        StartCoroutine(AdjustPosition());
        iterator++;
    }

    protected override void Start()
    {
        iteratorObject.transform.localPosition += 2 * offset * Vector3.left;
        base.Start();
    }
}
