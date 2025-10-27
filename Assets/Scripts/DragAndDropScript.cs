using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class DragAndDropScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,
    IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGro;
    private RectTransform rectTra;
    public ObjectScript objectScr;
    public ScreenBoundriesScript screenBou;

    private Vector3 dragOffsetWorld;
    private Camera uiCamera;
    private Canvas canvas;

    // Start is called befor

    // andr
    public void OnPointerDown(PointerEventData eventData)
    {
            Debug.Log("OnPointerDown");
            objectScr.effects.PlayOneShot(objectScr.audioCli[0]);
    }

    private bool ScreenPointToWorld(Vector2 screenPoint, out Vector3 worldPoint)
    {
        worldPoint = Vector3.zero;

        if (uiCamera == null)
        {
            return false;
        }

        float z = Mathf.Abs(uiCamera.transform.position.z - transform.position.z);
        Vector3 sp = new Vector3(screenPoint.x, screenPoint.y, z);

        return true;
    }

    void Awake()
    {
        canvasGro = GetComponent<CanvasGroup>();
        rectTra = GetComponent<RectTransform>();

        if (objectScr == null)
        {
            objectScr = Object.FindFirstObjectByType<ObjectScript>();
        }

        if (screenBou == null)
        {
            screenBou = Object.FindFirstObjectByType<ScreenBoundriesScript>();
        }

        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            uiCamera = canvas.worldCamera;
        }
        else
        {
            Debug.LogError("No canvas found!!!!!!!!");
        }
    }

    // andr
    public void OnBeginDrag(PointerEventData eventData)
    {
            ObjectScript.drag = true;
            ObjectScript.lastDragged = eventData.pointerDrag;
            canvasGro.blocksRaycasts = false;
            canvasGro.alpha = 0.6f;
            //rectTra.SetAsLastSibling();
            int lastIndex = transform.parent.childCount - 1;
            int position = Mathf.Max(0, lastIndex - 1);
            transform.SetSiblingIndex(position);

            Vector3 pointerWorld;

            if (ScreenPointToWorld(eventData.position, out pointerWorld))
            {
                ///
                dragOffsetWorld = transform.position - pointerWorld;
            } else
            {
                dragOffsetWorld = Vector3.zero;
            }

            screenBou.screenPoint = Camera.main.WorldToScreenPoint(rectTra.localPosition);

            ObjectScript.lastDragged = eventData.pointerDrag;
    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector3 pointerWorld;

        if (!ScreenPointToWorld(eventData.position, out pointerWorld))
        {
            return;
        }

        Vector3 desiredPosition = pointerWorld + dragOffsetWorld;
        desiredPosition.z = transform.position.z;

        screenBou.RecalculateBounds();

        Vector2 clamped = screenBou.GetClampedPosition(desiredPosition);
        transform.position = new Vector3(clamped.x, clamped.y, desiredPosition.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {


        objectScr.effects.PlayOneShot(objectScr.audioCli[0]);


        ObjectScript.drag = false;
            canvasGro.blocksRaycasts = true;
            canvasGro.alpha = 1.0f;

            if(objectScr.rightPlace)
            {
               canvasGro.blocksRaycasts = false;
                ObjectScript.lastDragged = null;
            }
            
            objectScr.rightPlace = false;
   
    }

    ////////////////////// HAS PROBLEMS
}
