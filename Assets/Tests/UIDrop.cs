using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDrop : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    private bool del=false;

    public void OnDrop(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject.name.Contains("Function_")) {
            EventSystem.current.currentSelectedGameObject.transform.parent = transform;
            var sizey = GetComponent<RectTransform>().rect.size.y;
            int index = (int)((((transform.position.y + (sizey / 2)) - Input.mousePosition.y)) / 35) + 1;
            if (index <= 0)
            {
                index = 1;
            }
            EventSystem.current.currentSelectedGameObject.transform.SetSiblingIndex(index);
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "trash")
    //    {
    //        Debug.Log("trash");
    //        del = true;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.tag == "trash")
    //    {
    //        del = false;
    //    }
    //}
    public void OnPointerClick(PointerEventData eventData)
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }

}
