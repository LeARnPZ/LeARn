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

        // "Œci¹ganie" kostek (przy usuwaniu)
        if (adjustMode == AdjustMode.inwards)
        {
            if (iterator == items.Count) yield break;

            foreach (GameObject item in items)
            {
                currentPositions.Add(item.transform.localPosition);
                if (items.IndexOf(item) >= iterator)
                {
                    newPositions.Add(item.transform.localPosition + offset * Vector3.left);
                }
                else
                {
                    newPositions.Add(currentPositions.Last());
                }
            }
        }
        // "Rozci¹ganie" kostek (przy dodawaniu)
        else if (adjustMode == AdjustMode.outwards)
        {
            if (iterator == items.Count-1) yield break;

            foreach (GameObject item in items)
            {
                currentPositions.Add(item.transform.localPosition);
                if (items.IndexOf(item) > iterator)
                {
                    newPositions.Add(item.transform.localPosition + offset * Vector3.right);
                }
                else
                {
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
        if (items.Count >= maxCount) return;
        base.AddItem();

        items[iterator].GetComponent<Rigidbody>().useGravity = false;
        StartCoroutine(AdjustPosition(AdjustMode.outwards));
        items[iterator].GetComponent<Rigidbody>().useGravity = true;
        iterator++;
    }

    public override void PopItem()
    {
        if (items.Count < 1) return;
        base.PopItem();

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

        //if (Input.GetKeyDown(KeyCode.U))
        //    StartCoroutine(AdjustPosition(AdjustMode.outwards));
    }
}
