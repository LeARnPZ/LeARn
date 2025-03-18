using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class SelectSort : Sortings
{
    private int minIndex;
    private Color defaultColor;
    private int[] heights;
    private static int[] saveHeights;
    private bool firstTime = true;

    void setup()
    {
        ColorUtility.TryParseHtmlString("#4274b2", out defaultColor);

        if (firstTime)
        {
            heights = new int[numberOfItems];
        }
        else { 
            heights = new int[saveHeights.Length];
            Array.Copy(saveHeights, heights, saveHeights.Length);
        }
        
        System.Random rnd = new System.Random();

        for (int i = 0; i < numberOfItems; i++)
        {
            if (firstTime)
            {
                heights[i] = rnd.Next(1, 16);
            }
            
            items[i].transform.localScale = new Vector3(1, heights[i] * 0.20f, 1);
            items[i].transform.localPosition = new Vector3(items[i].transform.localPosition.x, heights[i] * 0.10f, 0);
            SetColor(items[i], defaultColor);
        }
        if (firstTime) { 
            saveHeights = new int[heights.Length];
            Array.Copy(heights, saveHeights, heights.Length);
            firstTime = false;
        }
        

    }

    protected override IEnumerator Sort()
    {
        setup();
        for (int i = 0; i < numberOfItems-1; i++)
        {
            SetColor(items[i], Color.red);
            yield return new WaitForSeconds(timeout);
            int aktMin = i;
            for (int j = i + 1; j < numberOfItems; j++)
            {
                SetColor(items[j], Color.yellow);
                yield return new WaitForSeconds(timeout);
                if (heights[j] < heights[aktMin])
                {
                    SetColor(items[aktMin], defaultColor);
                    SetColor(items[j], Color.red);
                    aktMin = j;
                    yield return new WaitForSeconds(timeout);
                }
                else
                {
                    SetColor(items[j], defaultColor);
                }

            }


            SetColor(items[i], Color.red);
            yield return new WaitForSeconds(timeout);


            int temp = heights[i];
            heights[i] = heights[aktMin];
            heights[aktMin] = temp;

            GameObject tempObject = items[i];
            items[i] = items[aktMin];
            items[aktMin] = tempObject;

            SetColor(items[aktMin], defaultColor);

            Vector3 PointFirst = items[aktMin].transform.localPosition;
            Vector3 PointSecond = items[i].transform.localPosition;

            float time = 0;
            PointFirst.y = 0;
            PointSecond.y = 0;
            while (time < animDuration)
            {
                items[aktMin].transform.localPosition = Vector3.Lerp(PointFirst, PointSecond, time / animDuration);
                items[aktMin].transform.localPosition = new Vector3(items[aktMin].transform.localPosition.x, (items[aktMin].transform.localScale.y / 2), -1);

                items[i].transform.localPosition = Vector3.Lerp(PointSecond, PointFirst, time / animDuration);
                items[i].transform.localPosition = new Vector3(items[i].transform.localPosition.x, (items[i].transform.localScale.y / 2) - 0.1f, 1);

                time += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(timeout);
            PointSecond.y = (items[aktMin].transform.localScale.y / 2);
            items[aktMin].transform.localPosition = PointSecond;
            PointFirst.y = (items[i].transform.localScale.y / 2) - 0.1f;
            items[i].transform.localPosition = PointFirst;


            SetColor(items[i], Color.green);
            yield return new WaitForSeconds(timeout);
        }
        SetColor(items[numberOfItems - 1], Color.green);
        items[numberOfItems - 1].transform.localPosition = new Vector3(items[numberOfItems - 1].transform.localPosition.x, (items[numberOfItems - 1 ].transform.localScale.y / 2) - 0.1f, 0);
    }
    void SetColor(GameObject cube, Color color)
    {
        cube.gameObject.GetComponent<MeshRenderer>().material.color = color;
    }


}
