using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int order;
    
    [HideInInspector]
    public GameObject oldPole;
    
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

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(col);
        Debug.Log("Brick touches something");
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 10 + (10 * order);
    }

    public void GetBack() {
        transform.position = new Vector3(
            oldPole.transform.position.x, 
            oldPole.transform.position.y + (oldPole.GetComponent<RectTransform>().rect.height / 2), 
            transform.position.z
        );
        Debug.Log(oldPole.GetComponent<RectTransform>().rect.height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
