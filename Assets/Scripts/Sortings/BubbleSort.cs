using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : Sortings
{

    protected override IEnumerator Sort()
    {
        yield return new WaitForSeconds(1);
 
        for (int i=0; i<numberOfItems; i++)
        {
            for (int j=0; j<numberOfItems-1-i; j++)
            {
                StartCoroutine(ChangeColor(items[j], yellowColor));
                StartCoroutine(ChangeColor(items[j + 1], yellowColor));
                yield return new WaitForSeconds(timeout);


                if (GetValue(items[j]) > GetValue(items[j + 1]))
                {
                    StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.up));
                    StartCoroutine(MoveObject(items[j+1], items[j+1].transform.localPosition + Vector3.up));
                    yield return new WaitForSeconds(timeout);


                    StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.back/2));
                    StartCoroutine(MoveObject(items[j + 1], items[j + 1].transform.localPosition + Vector3.forward/2));
                    yield return new WaitForSeconds(timeout);

                    Vector3 posJ = items[j].transform.localPosition;
                    Vector3 posJ1 = items[j+1].transform.localPosition;
                    StartCoroutine(MoveObject(items[j], new Vector3(posJ1.x, posJ.y, posJ.z)));
                    StartCoroutine(MoveObject(items[j+1], new Vector3(posJ.x, posJ1.y, posJ1.z)));
                    yield return new WaitForSeconds(timeout);

                    StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.forward/2));
                    StartCoroutine(MoveObject(items[j + 1], items[j + 1].transform.localPosition + Vector3.back/2));
                    yield return new WaitForSeconds(timeout);

                    StartCoroutine(MoveObject(items[j], items[j].transform.localPosition + Vector3.down));
                    StartCoroutine(MoveObject(items[j+1], items[j+1].transform.localPosition + Vector3.down));
                    yield return new WaitForSeconds(timeout);

                    (items[j], items[j+1]) = (items[j+1], items[j]);
                }

                StartCoroutine(ChangeColor(items[j], blueColor));
                StartCoroutine(ChangeColor(items[j+1], blueColor));
                yield return new WaitForSeconds(timeout);

            }

            StartCoroutine(ChangeColor(items[items.Count - i - 1], greenColor));
            yield return new WaitForSeconds(timeout);

        }

        Debug.Log("Sorting finished.");
    }


}
