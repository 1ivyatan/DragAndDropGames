using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


using System;
using UnityEngine.UI;


public class Tracker : MonoBehaviour
{
    public List<GameObject> bricks;

    public static GameObject activeBrick = null;

    public static bool locked = false;

    public GameObject winningPole;
    public GameObject winOverlay;
    public GameObject menuOverlay;

    public GameObject tickerText;
    public GameObject movesText;

    public int moveCount;
    public int minMoveCount;
    private float timeTicked = 0;

    public void InspectVictory() {

        moveCount++;
        movesText.GetComponent<Text>().text = moveCount + " / " + minMoveCount;
        

        if (winningPole.transform.childCount == bricks.Count) {
            locked = true;
            StartCoroutine(SpawnWinScreen(activeBrick));
        }
        
    }

    IEnumerator SpawnWinScreen(GameObject brick) {
        yield return new WaitUntil(() => brick.GetComponent<Rigidbody2D>().linearVelocity.magnitude < 0.0001);
   
        TimeSpan time = TimeSpan.FromSeconds((int)timeTicked);
        winOverlay.transform.Find("Panel/FillText").GetComponent<Text>().text = time.ToString(@"hh\:mm\:ss");

        winOverlay.SetActive(true);
        menuOverlay.SetActive(false);
        Debug.Log("win");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   
        locked = false;
        activeBrick = null;
        moveCount = 0;
        minMoveCount = (int)Mathf.Pow(2, bricks.Count) - 1;
    }

    void Start() {
        movesText.GetComponent<Text>().text = moveCount + " / " + minMoveCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!locked) {
            timeTicked += Time.deltaTime;

            TimeSpan time = TimeSpan.FromSeconds((int)timeTicked);
            
            tickerText.GetComponent<Text>().text = time.ToString(@"hh\:mm\:ss");
        }
    }
}
