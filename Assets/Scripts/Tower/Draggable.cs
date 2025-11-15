using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera camera;
    private Vector3 dragOffsetWorld;
    
    public void OnBeginDrag(PointerEventData eventData) {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
//        transform.position = Vector3.zero;
    }

    public void OnEndDrag(PointerEventData eventData) {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }
/*
    private bool ScreenPointToWorld(Vector2 screenPoint, out Vector3 worldPoint) {
        worldPoint = Vector3.zero;
        
        if (camera == null) {
            return false;
        }

        float z = Mathf.Abs(camera.transform.position.z - transform.position.z);
        Vector3 sp = new Vector3(screenPoint.x, screenPoint.y, z);
        worldPoint = camera.ScreenToWorldPoint(sp);
        
        return true;
    }*/

    void Awake()
    {
       // camera = GetComponentInParent<Canvas>().worldCamera;

      //  Debug.Log(camera);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
