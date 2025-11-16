using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pole : MonoBehaviour
{
    public List<GameObject> bricks;

    private GameObject pole;
    private int readyCount;

    void OnTriggerEnter2D(Collider2D col) {

        if (readyCount > 0) { // all bricks havent fallen yet
            RealignIncomingBrick(col.gameObject);
        } else {
            if (bricks.Count > 0 && bricks.Last().GetComponent<Draggable>().order < col.gameObject.transform.GetComponent<Draggable>().order) {
                col.gameObject.GetComponent<Draggable>().GetBack();
                return;
            }

            DropIncomingBrick(col.gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        List<GameObject> addedBricks = new List<GameObject>();
        pole = this.gameObject;
        
        foreach(Transform brick in gameObject.transform) {
            addedBricks.Add(brick.gameObject);
        }

        bricks = addedBricks.OrderByDescending(g => g.GetComponent<Draggable>().order).ToList();
        readyCount = bricks.Count;

        int j = 0;
        for (int i = bricks.Count - 1; i >= 0; i--) {
          //  Debug.Log(bricks[i].transform.GetComponent<Draggable>().order);

            bricks[i].transform.position = new Vector3(
                transform.position.x,
                transform.position.y - (GetComponent<RectTransform>().rect.height / 2) + 45 +  (
                    bricks[j].transform.GetComponent<Draggable>().order * 90
                ),
                bricks[i].transform.position.z
            );

            bricks[i].GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, .0001f);
            StartCoroutine(WaitForBrickVelocityDrop(bricks[i]));

            j++;
        }
        
    }

    IEnumerator WaitForBrickVelocityDrop(GameObject brick) {
        yield return new WaitUntil(() => brick.GetComponent<Rigidbody2D>().linearVelocity.magnitude < 0.01);
   
        readyCount--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropIncomingBrick(GameObject brick) {
        Vector3 brickPosition = brick.transform.position;
        Vector3 polePosition = transform.position;


        brick.transform.SetParent(pole.transform);

        GameObject oldPole = brick.transform.GetComponent<Draggable>().oldPole;
        if (!oldPole) {
            oldPole = pole;
        }

        oldPole.GetComponent<Pole>().bricks.Remove(brick);
        bricks.Add(brick);

        brick.transform.GetComponent<Draggable>().oldPole = pole;
        brick.transform.position = new Vector3(polePosition.x, brickPosition.y, brickPosition.z);

    }

    void RealignIncomingBrick(GameObject brick) {
        Vector3 brickPosition = brick.transform.position;
        Vector3 polePosition = transform.position;

        GameObject oldPole = brick.transform.GetComponent<Draggable>().oldPole;
        if (!oldPole) {
            oldPole = pole;
        }

        brick.transform.GetComponent<Draggable>().oldPole = pole;
        brick.transform.position = new Vector3(polePosition.x, brickPosition.y, brickPosition.z);

    }
}
