using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerDownHandler, IBeginDragHandler
{
    Vector3 startPosition;
    Vector3 diffPosition;
    GameObject canvas_;
    GameObject panelGhost;
    GameObject panel;
    GameObject vo;
    public GameObject vo1;
    private RectTransform rectTransform;
    private Vector2 initialSizeDelta;
    void Awake()
    {
        panel = GameObject.Find("Panel Main Loop");
        vo1 = GameObject.Find("Void");
        rectTransform = panel.GetComponent<RectTransform>();
        initialSizeDelta = rectTransform.sizeDelta;
    }


    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = Input.mousePosition - diffPosition;
        
        

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(vo);


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = transform.position;
        diffPosition = Input.mousePosition - startPosition;
        EventSystem.current.SetSelectedGameObject(gameObject);
        EventSystem.current.currentSelectedGameObject.transform.SetParent(canvas_.transform);
        EventSystem.current.currentSelectedGameObject.transform.SetAsFirstSibling();
        Debug.Log("start drag " + gameObject.name);
    }

    void Start()
    {
        canvas_ = GameObject.Find("Canvas");
        panelGhost = GameObject.Find("Panel ghost");
        
    }

    void Update()
    {
    
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        vo = Instantiate(vo1);
        vo.transform.SetParent(panel.transform);
    }
}
