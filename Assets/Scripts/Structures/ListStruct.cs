using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListStruct : Structures
{
    [SerializeField]
    private float speed;

    private Vector3 adjustmentVector;

    private IEnumerator AdjustPosition()
    {
        float elapsedTime = 0;
        float animDuration = 0.5f;

        List<Vector3> currentPositions = new();
        List<Vector3> newPositions = new();

        if (iterator == items.Count-1) yield break;

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

    public override void AddItem()
    {
        if (items.Count >= maxCount) return;
        base.AddItem();

        adjustmentVector = Vector3.right;
        items[iterator].GetComponent<Rigidbody>().useGravity = false;
        StartCoroutine(AdjustPosition());
        items[iterator].GetComponent<Rigidbody>().useGravity = true;
        iterator++;
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

    public override void PeekItem()
    {
        base.PeekItem();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PopItem();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            AddItem();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PeekItem();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (iterator < maxCount && iterator < items.Count)
                iterator++;
            Debug.Log(iterator);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (iterator > 0)
                iterator--;
            Debug.Log(iterator);
        }

        //if (Input.GetKeyDown(KeyCode.U))
        //    StartCoroutine(AdjustPosition(AdjustMode.outwards));
    }
}
