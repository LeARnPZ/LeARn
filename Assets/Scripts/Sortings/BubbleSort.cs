using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : Sortings
{

    public bool isPaused;
    protected override IEnumerator Sort()
    {
        isPaused = false;
        yield return new WaitForSeconds(1);
 
        for (int i=0; i<numberOfItems; i++)
        {
            for (int j=0; j<numberOfItems-1-i; j++)
            {
                while (isPaused)
                {
                    yield return null;
                }

                StartCoroutine(ChangeColor(items[j], Color.yellow));
                StartCoroutine(ChangeColor(items[j+1], Color.yellow));
                yield return new WaitForSeconds(timeout);

                
                while (isPaused)
                {
                    yield return null;
                }

                if (GetValue(items[j]) > GetValue(items[j + 1]))
                {
                    StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.up));
                    StartCoroutine(MoveObject(items[j+1], items[j+1].transform.localPosition + Vector3.up));
                    yield return new WaitForSeconds(timeout);

                    while (isPaused)
                    {
                        yield return null;
                    }

                    StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.back/2));
                    StartCoroutine(MoveObject(items[j + 1], items[j + 1].transform.localPosition + Vector3.forward/2));
                    yield return new WaitForSeconds(timeout);

                    while (isPaused)
                    {
                        yield return null;
                    }

                    Vector3 posJ = items[j].transform.localPosition;
                    Vector3 posJ1 = items[j+1].transform.localPosition;
                    StartCoroutine(MoveObject(items[j], new Vector3(posJ1.x, posJ.y, posJ.z)));
                    StartCoroutine(MoveObject(items[j+1], new Vector3(posJ.x, posJ1.y, posJ1.z)));
                    yield return new WaitForSeconds(timeout);

                    while (isPaused)
                    {
                        yield return null;
                    }

                    StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.forward/2));
                    StartCoroutine(MoveObject(items[j + 1], items[j + 1].transform.localPosition + Vector3.back/2));
                    yield return new WaitForSeconds(timeout);

                    while (isPaused)
                    {
                        yield return null;
                    }

                    StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.down));
                    StartCoroutine(MoveObject(items[j+1], items[j+1].transform.localPosition + Vector3.down));
                    yield return new WaitForSeconds(timeout);

                    while (isPaused)
                    {
                        yield return null;
                    }

                    (items[j], items[j+1]) = (items[j+1], items[j]);
                }

                StartCoroutine(ChangeColor(items[j], Color.white));
                StartCoroutine(ChangeColor(items[j+1], Color.white));
                yield return new WaitForSeconds(timeout);

                while (isPaused)
                {
                    yield return null;
                }
            }

            StartCoroutine(ChangeColor(items[items.Count - i - 1], Color.green));
            yield return new WaitForSeconds(timeout);

            while (isPaused)
            {
                yield return null;
            }
        }

        Debug.Log("Sorting finished.");
    }

    public void setPaused(bool paused)
    {
        isPaused = paused;
    }

    public bool getPaused()
    {
        return isPaused;
    }


}
