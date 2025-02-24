using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ListStruct : Structures
{
    [SerializeField]
    private float speed;

    private enum AdjustMode
    {
        inwards, outwards
    }

    private IEnumerator AdjustPosition(AdjustMode adjustMode)
    {
        float elapsedTime = 0;
        float animDuration = 0.5f;

        List<Vector3> currentPositions = new();
        List<Vector3> newPositions = new();

        if (adjustMode == AdjustMode.inwards)
        {
            foreach (GameObject item in items)
            {
                currentPositions.Add(item.transform.localPosition);
                if (items.IndexOf(item) < iterator)
                    newPositions.Add(item.transform.localPosition + offset * Vector3.left / 2);
                else
                    newPositions.Add(item.transform.localPosition + offset * Vector3.right / 2);
            }
        }
        else if (adjustMode == AdjustMode.outwards)
        {
            foreach (GameObject item in items)
            {
                if (items.IndexOf(item) >= iterator)
                {
                    currentPositions.Add(item.transform.localPosition);
                    newPositions.Add(item.transform.localPosition + offset * Vector3.right);
                }
                else
                {
                    //if (iterator > 0)
                    //    newPositions.Add(item.transform.localPosition + offset * Vector3.right / 2);
                    //else
                    //    newPositions.Add(item.transform.localPosition + offset * Vector3.right);
                    currentPositions.Add(item.transform.localPosition);
                    newPositions.Add(currentPositions.Last());
                }
            }
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
        //StartCoroutine(AdjustPosition(AdjustMode.outwards));
        base.AddItem();
        iterator++;
    }

    public override void PopItem()
    {
        base.PopItem();

        if (items.Count < 1) return;
        StartCoroutine(AdjustPosition(AdjustMode.inwards));
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

        if (Input.GetKeyDown(KeyCode.U))
            StartCoroutine(AdjustPosition(AdjustMode.outwards));
    }
}
