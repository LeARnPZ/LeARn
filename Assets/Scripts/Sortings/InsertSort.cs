using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertSort : Sortings
{
    protected override IEnumerator Sort()
    {
        yield return new WaitForSeconds(1);

        for (int i = 1; i < numberOfItems; i++)
        {
            GameObject keyItem = items[i];
            int keyValue = GetValue(keyItem);
            int j = i-1;

            StartCoroutine(ChangeColor(keyItem, Color.yellow)); // Highlight key item
            yield return new WaitForSeconds(timeout);

            // Move key item UP before shifting
            Vector3 originalPos = keyItem.transform.localPosition;
            Vector3 moveUpPos = originalPos + Vector3.up;
            yield return StartCoroutine(MoveObject(keyItem, moveUpPos)); //tutaj wrzucamy item startowy do góry

            Vector3 finalPosition = originalPos; 
            while (j >= 0 && GetValue(items[j]) > keyValue) //petla dzialajaca dla itemów większych od keyvalue
            {

                StartCoroutine(ChangeColor(items[j], Color.blue)); // Highlight larger item
                StartCoroutine(ChangeColor(items[i], Color.blue)); // Highlight larger item
                yield return new WaitForSeconds(timeout);
                finalPosition = items[j].transform.localPosition; // Track correct key placement
                // Move larger item RIGHT
                Vector3 moveRightPos = items[j].transform.localPosition + Vector3.right * 1.2f; 
                
                yield return StartCoroutine(MoveObject(items[j], moveRightPos)); // rzucamy większy item w prawo
                
                StartCoroutine(ChangeColor(items[j], Color.green)); // Highlight larger item
                // Move reference in list
                items[j + 1] = items[j];
                
                j--;
            }

            // Move key item LEFT to correct position
            Vector3 moveLeftPos = new Vector3(finalPosition.x, moveUpPos.y, moveUpPos.z);
            yield return StartCoroutine(MoveObject(keyItem, moveLeftPos));

            // Move key item DOWN to align on Y-axis
            Vector3 finalPos = new Vector3(moveLeftPos.x, originalPos.y, originalPos.z);
            yield return StartCoroutine(MoveObject(keyItem, finalPos));

            // Update key item reference in list
            items[j + 1] = keyItem;
            StartCoroutine(ChangeColor(keyItem, Color.green)); // Mark as sorted
            yield return new WaitForSeconds(timeout);
        }

        Debug.Log("Insertion Sort finished.");
    }
}