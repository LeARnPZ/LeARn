using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModifyStructure : MonoBehaviour
{
    [SerializeField]
    private GameObject anim;
    [SerializeField]
    private float timeout;
    private bool isTimeout;
    private bool touchInput;
    [SerializeField]
    private GameObject stackButtonsObject;
    [SerializeField]
    private GameObject queueButtonsObject;
    [SerializeField]
    private GameObject listButtonsObject;


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
        Structures structures = anim.transform.GetChild(0).GetComponent<Structures>();
        if (structures.GetCount() < structures.GetMaxCount())
        {
            addBtn.GetComponent<Button>().interactable = true;
        }
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
            if (PlayerPrefs.GetString("algorithm").Contains("Stack"))
            {
                stackButtonsObject.SetActive(true);
                queueButtonsObject.SetActive(false);
                listButtonsObject.SetActive(false);
            } 
            else if (PlayerPrefs.GetString("algorithm").Contains("Queue"))
            {
                queueButtonsObject.SetActive(true);
                stackButtonsObject.SetActive(false);
                listButtonsObject.SetActive(false);
            }
            //gameObject.SetActive(true);
            else if (PlayerPrefs.GetString("algorithm").Contains("List"))
            {
                touchInput = true;
                listButtonsObject.SetActive(true);
                stackButtonsObject.SetActive(false);
                queueButtonsObject.SetActive(false);
            }
                
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
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            int screenWidth = Screen.width;
            int screenHeight = Screen.height;
            ListStruct listStruct = anim.transform.GetChild(0).GetComponent<ListStruct>();

            if (touch.position.y > 0.2 * screenHeight && touch.position.y < 0.8 * screenHeight)
            {
                if (touch.position.x > 0.7 * screenWidth)
                {
                    if (listStruct.GetIterator() < listStruct.GetCount())
                    {
                        StartCoroutine(listStruct.MoveIterator(Vector3.right));
                        StartCoroutine(ButtonsTimeout());
                    }
                }

                if (touch.position.x < 0.3 * screenWidth)
                {
                    if (listStruct.GetIterator() > 0)
                    {
                        StartCoroutine(listStruct.MoveIterator(Vector3.left));
                        StartCoroutine(ButtonsTimeout());
                    }
                }
            }
        }
    }
}
