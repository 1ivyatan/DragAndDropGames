using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int order;
    
    private Camera camera;
    private Vector3 dragOffsetWorld;
    private Rigidbody2D rb;
    
    public void OnBeginDrag(PointerEventData eventData) {
        rb.bodyType = RigidbodyType2D.Static;

    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        rb.bodyType = RigidbodyType2D.Dynamic;

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.mass = 10 + (10 * order);
       // camera = GetComponentInParent<Canvas>().worldCamera;

      //  Debug.Log(camera);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
