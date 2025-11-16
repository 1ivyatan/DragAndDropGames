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
    public GameObject bgm;


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

            menuOverlay.SetActive(false);
            bgm.GetComponent<AudioSource>().clip = UnityEngine.Object.FindFirstObjectByType<Sounds>().sfx[2];
            bgm.GetComponent<AudioSource>().Play();
            StartCoroutine(SpawnWinScreen(activeBrick));
        }
        
    }

    IEnumerator SpawnWinScreen(GameObject brick) {
        yield return new WaitUntil(() => brick.GetComponent<Rigidbody2D>().linearVelocity.magnitude < 0.0001);
   
        TimeSpan time = TimeSpan.FromSeconds((int)timeTicked);
        winOverlay.transform.Find("Panel/FillText").GetComponent<Text>().text = time.ToString(@"hh\:mm\:ss") + "\n" + moveCount + " / " + minMoveCount;

        int seconds = (int)TimeSpan.FromSeconds((int)timeTicked).TotalSeconds;
        if (seconds <= 120 && moveCount == minMoveCount) {
            winOverlay.transform.Find("Panel/StarsText").GetComponent<Text>().text = "★★★";
        } else if (seconds <= 300 && moveCount < minMoveCount * 2) {
            winOverlay.transform.Find("Panel/StarsText").GetComponent<Text>().text = "★★";
        } else {
            winOverlay.transform.Find("Panel/StarsText").GetComponent<Text>().text = "★";
        }

        winOverlay.SetActive(true);
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
