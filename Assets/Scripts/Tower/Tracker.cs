using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public List<GameObject> bricks;

    public static GameObject activeBrick = null;

    public GameObject winningPole;

    public void InspectVictory() {
        if (winningPole.transform.childCount == bricks.Count) {
            Debug.Log("win");
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
