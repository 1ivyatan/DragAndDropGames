using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pole : MonoBehaviour
{
    public List<GameObject> bricks;

    void OnTriggerEnter2D(Collider2D col) {

        RealignIncomingBrick(col.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bricks = new List<GameObject>();
        
        foreach(Transform brick in gameObject.transform) {
            bricks.Add(brick.gameObject);
            RealignIncomingBrick(brick.gameObject);
//            Debug.Log(bricks.Last());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RealignIncomingBrick(GameObject brick) {
        Vector3 brickPosition = brick.transform.position;
        Vector3 polePosition = transform.position;


        brick.transform.SetParent(this.gameObject.transform);

        GameObject oldPole = brick.transform.GetComponent<Draggable>().oldPole;
        if (!oldPole) {
            oldPole = this.gameObject;
        }

        oldPole.GetComponent<Pole>().bricks.Remove(brick);
        bricks.Add(brick);

        brick.transform.GetComponent<Draggable>().oldPole = this.gameObject;
        brick.transform.position = new Vector3(polePosition.x, brickPosition.y, brickPosition.z);

    }
}
