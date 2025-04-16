using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using TMPro;

public class SelectSort : Sortings
{
    private int minIndex;
    private Color defaultColor;
    private int[] heights;
    private int[] checkDuplicate;
    private static int[] saveHeights;
    private bool firstTime = true;

    void setup()
    {
        ColorUtility.TryParseHtmlString("#4274b2", out defaultColor);

        if (firstTime)
        {
            heights = new int[numberOfItems];
            checkDuplicate = new int[17];
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
                int tmp;
                while (checkDuplicate[tmp = rnd.Next(3, 16)] != 0) ;  // zapewnia, ze dana liczba wyst¹py tylko raz 
                checkDuplicate[tmp]++;

                heights[i] = tmp;
            }
            
            items[i].transform.localScale = new Vector3(1, heights[i] * 0.20f, 1);
            items[i].transform.localPosition = new Vector3(items[i].transform.localPosition.x, heights[i] * 0.10f, 0);
            SetColor(items[i], defaultColor);

            float scale = heights[i] * 0.20f;

            items[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = heights[i].ToString();
            items[i].transform.GetChild(1).GetComponent<TextMeshPro>().text = heights[i].ToString();

            items[i].transform.GetChild(0).transform.localScale = new Vector3(1, (1 / scale), 1);
            items[i].transform.GetChild(1).transform.localScale = new Vector3(1, (1 / scale), 1);

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
            if(aktMin == i)
            {
                SetColor(items[i], Color.green);
                yield return new WaitForSeconds(timeout);
            }
            else
            {
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
                

                while (time < animDuration) // wysuniêcie
                {
                    items[aktMin].transform.localPosition = Vector3.Lerp(PointFirst, PointFirst + new Vector3(0, 0, 1), time / animDuration);
                    items[i].transform.localPosition = Vector3.Lerp(PointSecond, PointSecond + new Vector3(0, 0, -1), time / animDuration);
                    time += Time.deltaTime;
                    yield return null;
                }

                Vector3 PointFirst2 = items[aktMin].transform.localPosition;
                Vector3 PointSecond2 = items[i].transform.localPosition;
                PointFirst.y = 0;
                PointSecond.y = 0;
                time = 0;
                while (time < animDuration) //zmiana miejsc
                {

                    items[aktMin].transform.localPosition = Vector3.Lerp(PointFirst2, PointSecond2, time / animDuration);
                    items[aktMin].transform.localPosition = new Vector3(items[aktMin].transform.localPosition.x, (items[aktMin].transform.localScale.y / 2), 1);

                    items[i].transform.localPosition = Vector3.Lerp(PointSecond2, PointFirst2, time / animDuration);
                    items[i].transform.localPosition = new Vector3(items[i].transform.localPosition.x, items[i].transform.localScale.y / 2, -1);

                    time += Time.deltaTime;
                    yield return null;
                }

                PointFirst2 = items[aktMin].transform.localPosition;
                PointSecond2 = items[i].transform.localPosition;
                time = 0;

                PointSecond.y = (items[aktMin].transform.localScale.y / 2);
                PointFirst.y = (items[i].transform.localScale.y / 2);

                PointSecond.z = (items[aktMin].transform.localScale.z - 1);
                PointFirst.z = (items[i].transform.localScale.z -1);

                while (time < animDuration) // wsuniêcie 
                {
                    
                    items[aktMin].transform.localPosition = Vector3.Lerp(PointFirst2, PointSecond, time / animDuration);
                    items[i].transform.localPosition = Vector3.Lerp(PointSecond2, PointFirst, time / animDuration);

                    time += Time.deltaTime;
                    yield return null;
                }
                yield return new WaitForSeconds(timeout/5);

                SetColor(items[i], Color.green);
                yield return new WaitForSeconds(timeout);
            }

            
        }
        SetColor(items[numberOfItems - 1], Color.green);
    }
    void SetColor(GameObject cube, Color color)
    {
        cube.gameObject.GetComponent<MeshRenderer>().material.color = color;
    }


}
