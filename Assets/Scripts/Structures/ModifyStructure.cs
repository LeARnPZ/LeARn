using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyStructure : MonoBehaviour
{
    [SerializeField]
    private new GameObject animation;
    [SerializeField]
    private GameObject addBtn;
    [SerializeField]
    private GameObject popBtn;

    public void OnButtonClick()
    {
        StartCoroutine(ClickHandler());
    }

    private IEnumerator ClickHandler()
    {
        if (this.gameObject.Equals(addBtn))
        {
            animation.transform.GetChild(0).GetComponent<Structures>().AddItem();
            popBtn.GetComponent<Button>().interactable = true;
        }
        else if (this.gameObject.Equals(popBtn))
        {
            animation.transform.GetChild(0).GetComponent<Structures>().PopItem();
            GetComponent<Button>().interactable = false;
            yield return new WaitForSeconds(0.5f);
            if (!animation.transform.GetChild(0).GetComponent<Structures>().IsEmpty())
                GetComponent<Button>().interactable = true;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("algorithm").Contains("Struct"))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        GetComponent<Button>().interactable = false;
    }
}
