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
    private int taps;
    private float elapsedTimeForDoubleTap;
    private readonly float maxTimeForDoubleTap = 0.5f;

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

    private void RecordTap()
    {
        if (elapsedTimeForDoubleTap > maxTimeForDoubleTap)
        {
            taps = 1;
            elapsedTimeForDoubleTap = 0f;
        }
        else
        {
            taps++;
        }
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
            else if (PlayerPrefs.GetString("algorithm").Contains("List"))
            {
                touchInput = true;
                taps = 0;
                elapsedTimeForDoubleTap = 0f;
                listButtonsObject.SetActive(true);
                stackButtonsObject.SetActive(false);
                queueButtonsObject.SetActive(false);
            }

        }
        else
        {
            gameObject.SetActive(false);
        }

        addBtn.GetComponent<Button>().interactable = false;
        popBtn.GetComponent<Button>().interactable = false;
        peekBtn.GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        elapsedTimeForDoubleTap += Time.deltaTime;

        if (!touchInput) 
            return;

        if (Input.touchCount == 1 && anim.transform.childCount > 0)
        {
            if (isTimeout)
                return;

            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject() || touch.phase != TouchPhase.Began)
                return;

            int screenWidth = Screen.width;
            int screenHeight = Screen.height;
            ListStruct listStruct = anim.transform.GetChild(0).GetComponent<ListStruct>();

            if (touch.position.y > 0.2 * screenHeight && touch.position.y < 0.8 * screenHeight)
            {
                if (touch.position.x > 0.7 * screenWidth)
                {
                    RecordTap();
                    if (listStruct.GetIterator() < listStruct.GetCount() && taps == 2)
                    {
                        StartCoroutine(listStruct.MoveIterator(Vector3.right));
                        StartCoroutine(ButtonsTimeout());
                        taps = 0;
                    }
                }

                else if (touch.position.x < 0.3 * screenWidth)
                {
                    RecordTap();
                    if (listStruct.GetIterator() > 0 && taps == 2)
                    {
                        StartCoroutine(listStruct.MoveIterator(Vector3.left));
                        StartCoroutine(ButtonsTimeout());
                        taps = 0;
                    }
                }
            }
        }
    }
}
