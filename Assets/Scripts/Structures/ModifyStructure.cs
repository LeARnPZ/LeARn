using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyStructure : MonoBehaviour
{
    [SerializeField]
    private GameObject anim;
    [SerializeField]
    private float timeout;

    [Header("Button objects")]
    [SerializeField]
    private GameObject addBtn;
    [SerializeField]
    private GameObject popBtn;
    [SerializeField]
    private GameObject peekBtn;

    public void AddButtonClick()
    {
        anim.transform.GetChild(0).GetComponent<Structures>().AddItem();
        StartCoroutine(ButtonsTimeout());
    }
    
    public void PopButtonClick()
    {
        anim.transform.GetChild(0).GetComponent<Structures>().PopItem();
        StartCoroutine(ButtonsTimeout());
    }
    
    public void PeekButtonClick()
    {
        anim.transform.GetChild(0).GetComponent<Structures>().PeekItem();
        StartCoroutine(ButtonsTimeout());
    }

    private IEnumerator ButtonsTimeout()
    {
        addBtn.GetComponent<Button>().interactable = false;
        popBtn.GetComponent<Button>().interactable = false;
        peekBtn.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(timeout);
        addBtn.GetComponent<Button>().interactable = true;
        if (!anim.transform.GetChild(0).GetComponent<Structures>().IsEmpty())
        {
            popBtn.GetComponent<Button>().interactable = true;
            peekBtn.GetComponent<Button>().interactable = true;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("algorithm").Contains("Struct"))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        addBtn.GetComponent<Button>().interactable = false;
        popBtn.GetComponent<Button>().interactable = false;
        peekBtn.GetComponent<Button>().interactable = false;
    }
}
