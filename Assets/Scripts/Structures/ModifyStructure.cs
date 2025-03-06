using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ModifyStructure : MonoBehaviour
{
    [SerializeField]
    private GameObject anim;
    [SerializeField]
    private float timeout;
    private bool isTimeout;
    private bool touchInput;

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
        isTimeout = true;
        addBtn.GetComponent<Button>().interactable = false;
        popBtn.GetComponent<Button>().interactable = false;
        peekBtn.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(timeout);
        addBtn.GetComponent<Button>().interactable = true;
        Structures structures = anim.transform.GetChild(0).GetComponent<Structures>();
        if (structures.GetCount() != 0 && structures.GetIterator() < structures.GetCount())
        {
            popBtn.GetComponent<Button>().interactable = true;
            peekBtn.GetComponent<Button>().interactable = true;
        }
        isTimeout = false;
    }

    private void Start()
    {
        touchInput = false;
        if (PlayerPrefs.GetString("algorithm").Contains("Struct"))
        {
            gameObject.SetActive(true);
            if (PlayerPrefs.GetString("algorithm").Contains("List"))
                touchInput = true;
        }
        else
            gameObject.SetActive(false);

        addBtn.GetComponent<Button>().interactable = false;
        popBtn.GetComponent<Button>().interactable = false;
        peekBtn.GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        if (touchInput && anim.transform.childCount > 0 && Input.touchCount > 0 && !isTimeout)
        {
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;
            Touch touch = Input.GetTouch(0);
            Structures structures = anim.transform.GetChild(0).GetComponent<Structures>();

            if (touch.position.y < 0.8 * screenHeight)
            {
                if (touch.position.x > 0.7 * screenWidth)
                {
                    if (structures.GetIterator() < structures.GetCount())
                    {
                        StartCoroutine(anim.transform.GetChild(0).GetComponent<ListStruct>().MoveIterator(Vector3.right));
                        StartCoroutine(ButtonsTimeout());
                    }
                }

                if (touch.position.x < 0.3 * screenWidth)
                {
                    if (structures.GetIterator() > 0)
                    {
                        StartCoroutine(anim.transform.GetChild(0).GetComponent<ListStruct>().MoveIterator(Vector3.left));
                        StartCoroutine(ButtonsTimeout());
                    }
                }
            }
        }
    }
}
