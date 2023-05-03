using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CreationBlock : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    public GameObject block;
    public GameObject Canvas;
    private GameObject clone;
    private Transform start;
    public void OnBeginDrag(PointerEventData eventDatax)
    {
        start = transform;
        clone= Instantiate(block,gameObject.transform.position,gameObject.transform.rotation,Canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        clone.GetComponent<UIDrag>().OnDrag(eventData);
        clone.GetComponent<UIDrag>().OnPointerClick(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.transform.position = start.position;

    }
}