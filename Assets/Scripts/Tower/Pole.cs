using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pole : MonoBehaviour
{
    private List<GameObject> bricks;

    void OnTriggerEnter2D(Collider2D col) {

        Debug.Log(col.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bricks = new List<GameObject>();
        
        foreach(Transform brick in gameObject.transform) {
            bricks.Add(brick.gameObject);
            Debug.Log(bricks.Last());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
