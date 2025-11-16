using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public List<GameObject> bricks;

    public static GameObject activeBrick = null;

    public static bool locked = false;

    public GameObject winningPole;

    public void InspectVictory() {
        if (winningPole.transform.childCount == bricks.Count) {
            locked = true;
            StartCoroutine(SpawnWinScreen(activeBrick));
        }
        
    }

    IEnumerator SpawnWinScreen(GameObject brick) {
        yield return new WaitUntil(() => brick.GetComponent<Rigidbody2D>().linearVelocity.magnitude < 0.0001);
   
        Debug.Log("win");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   
        locked = false;
        activeBrick = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
