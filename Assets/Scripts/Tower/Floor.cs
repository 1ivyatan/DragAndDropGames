using UnityEngine;

public class Floor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(Screen.width, 25);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
