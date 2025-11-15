using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pole : MonoBehaviour
{
    public List<GameObject> bricks;

    private GameObject pole;

    void OnTriggerEnter2D(Collider2D col) {

        RealignIncomingBrick(col.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        List<GameObject> addedBricks = new List<GameObject>();
        pole = this.gameObject;
        
        foreach(Transform brick in gameObject.transform) {
            addedBricks.Add(brick.gameObject);
        }

        bricks = addedBricks.OrderBy(g => g.transform.GetComponent<Draggable>().order).ToList();

        int j = 0;
        for (int i = bricks.Count - 1; i >= 0; i--) {
            Debug.Log(bricks[i].transform.GetComponent<Draggable>().order);

            bricks[i].transform.position = new Vector3(
                transform.position.x,
                transform.position.y - (GetComponent<RectTransform>().rect.height / 2) + 45 +  (
                    bricks[j].transform.GetComponent<Draggable>().order * 90
                ),
                bricks[i].transform.position.z
            );

            j++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RealignIncomingBrick(GameObject brick) {
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
}
