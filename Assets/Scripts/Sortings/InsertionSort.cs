using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSort : Sortings
{

    public override void Restart()
    {
        StopAllCoroutines();

        SpeedButton speedButton = FindObjectOfType<SpeedButton>();
        if (speedButton != null)
        {
            speedButton.SpeedButtonRestart();
        }

        isPaused = false;
        Time.timeScale = 1f;
        
        // values.Clear();
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        items.Clear();
        
        Start();
    }

    protected override IEnumerator Sort()
    {
        yield return new WaitForSeconds(1);

        for (int i = 1; i < numberOfItems; i++)
        {
            GameObject keyItem = items[i];
            int keyValue = GetValue(keyItem);
            int j = i - 1;

            yield return StartCoroutine(ChangeColor(keyItem, yellowColor));
            yield return new WaitForSeconds(timeout);

            Vector3 originalPos = keyItem.transform.localPosition;
            Vector3 moveUpPos = originalPos + Vector3.up * 1.5f;
            yield return StartCoroutine(MoveObject(keyItem, moveUpPos)); 

            Vector3 finalPosition = originalPos; 
            List<GameObject> movedItems = new List<GameObject>(); 
            
            while (j >= 0)
            {
                yield return StartCoroutine(ChangeColor(items[j], yellowColor));
                yield return StartCoroutine(ChangeColor(keyItem, yellowColor));
                yield return new WaitForSeconds(timeout);

                if (GetValue(items[j]) > keyValue)
                {
                    finalPosition = items[j].transform.localPosition;
                    Vector3 moveRightPos = items[j].transform.localPosition + Vector3.right * 1.2f;

                    yield return StartCoroutine(MoveObject(items[j], moveRightPos));

                    movedItems.Add(items[j]);

                    items[j + 1] = items[j];
                    j--;
                }
                else
                {
                    yield return StartCoroutine(ChangeColor(items[j], blueColor));
                    // yield return StartCoroutine(ChangeColor(keyItem, blueColor));
                    break;
                }

                yield return StartCoroutine(ChangeColor(items[j + 1], blueColor));            
            
            }


            Vector3 moveLeftPos = new Vector3(finalPosition.x, moveUpPos.y, moveUpPos.z);
            yield return StartCoroutine(MoveObject(keyItem, moveLeftPos));

            Vector3 finalPos = new Vector3(moveLeftPos.x, originalPos.y, originalPos.z);
            yield return StartCoroutine(MoveObject(keyItem, finalPos));

            // yield return StartCoroutine(ChangeColor(keyItem, blueColor));

            items[j + 1] = keyItem;

            foreach (GameObject item in movedItems)
            {
                yield return StartCoroutine(ChangeColor(item, blueColor));
            }

            yield return StartCoroutine(ChangeColor(keyItem, blueColor));
            
            yield return new WaitForSeconds(timeout);
        }

        for ( int z = 0; z < items.Count; z++)
        {
            yield return StartCoroutine(ChangeColor(items[z], greenColor));
        }
    }
}
