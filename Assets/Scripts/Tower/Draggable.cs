using System.Linq;
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
    private bool dragged = false;
    
    public void OnBeginDrag(PointerEventData eventData) {
        if (oldPole.GetComponent<Pole>().bricks.Last() != gameObject) return;
        if (Tracker.activeBrick && Tracker.activeBrick != this.gameObject) return;
        else Tracker.activeBrick = this.gameObject;

        dragged = true;
        rb.bodyType = RigidbodyType2D.Static;

        Debug.Log("Brick picked up");
    }

    public void OnDrag(PointerEventData eventData) {
        if (oldPole.GetComponent<Pole>().bricks.Last() != gameObject) return;
        if (Tracker.activeBrick && Tracker.activeBrick != this.gameObject) return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = new Vector2(0f, .1f);
        dragged = false;
    }

    void OnTriggerEnter2D(Collider2D col) {
//        Debug.Log(col);
  //      Debug.Log("Brick touches something");
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

    void Update()
    {
        if (Tracker.activeBrick && Tracker.activeBrick == this.gameObject && !dragged) {

            if (rb.linearVelocity.magnitude > 0.01) return;
            Tracker.activeBrick = null;
            Debug.Log("Brick finally dropped, others can be picked up");
        }
    }
}
