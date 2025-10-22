using System;
using UnityEngine;
using UnityEngine.UI;

public class CarVictoryScript : MonoBehaviour
{
    public static GameObject hangarHud;
    public static GameObject gameEndHud;


    public GameObject hangar;
    public GameObject gameEnd;
    public static TickerScript ticker;



    public FlyingObjectSpawnScript flyingObjectSpawnScript;
    public static FlyingObjectSpawnScript flierSpawner;



    public TickerScript tickerScr;


    public GameObject counterTextObject;

    private static Text counterText;

    [HideInInspector]
    public static bool lostCar;

    [HideInInspector]
    public static int counter;

    [HideInInspector]
    public static int max;

    private static int realMax;

    public GameObject carContainer;

    private static void checkVictory() {
        counterText.text = counter + " / " + realMax;

        if (counter != realMax) return;

        TickerScript.mustTick = false;
        flierSpawner.isActivated = false;

        while (flierSpawner.spawnPoint.childCount > 0) {
            DestroyImmediate(flierSpawner.spawnPoint.GetChild(0).gameObject);
        }

        if (lostCar) {
            gameEndHud.transform.Find("EndText").GetComponent<Text>().text = "Neuzvarēji!\n" + counter + " / " + realMax + " (" + max + ")" + "\n" + hangarHud.transform.Find("TimeText").GetComponent<Text>().text;                                    
            gameEndHud.transform.Find("StarsText").GetComponent<Text>().text = "";
        } else {
            gameEndHud.transform.Find("EndText").GetComponent<Text>().text = "Uzvarēji!\n" + counter + " / " + realMax + "\n" + hangarHud.transform.Find("TimeText").GetComponent<Text>().text;

    //            gameEndHud.transform.Find("StarsText")
            if (ticker.getTime().TotalSeconds <= 150) {
                gameEndHud.transform.Find("StarsText").GetComponent<Text>().text = "★★★";
            } else if (ticker.getTime().TotalSeconds <= 300) {
                gameEndHud.transform.Find("StarsText").GetComponent<Text>().text = "★★";
            } else {
                gameEndHud.transform.Find("StarsText").GetComponent<Text>().text = "★";
            }
        }


        hangarHud.SetActive(false);    
        gameEndHud.SetActive(true);    

// .transform.FindChild("EndText").gameObject
       // GameObject hangr = hangarHud.GetComponentsInChildren.Find("HangarText");

//        Debug.Log(hangarHud.Find<GameObject>("CounterText"));
    }

    public static  void decreaseMax() {
        lostCar = true;
        realMax--;

        counterText.color = new Color(.8f, .3f, .3f, 1f);

        checkVictory();
    }

    public static void increment() {
        counter++;

        checkVictory();
    }

    void Start()
    {
        
        max = carContainer.GetComponent<Transform>().childCount;
        realMax = max;
        counter = 0;
        lostCar = false;

        counterText = counterTextObject.GetComponent<Text>();


        hangarHud = hangar;
        gameEndHud = gameEnd;
        ticker = tickerScr;
        flierSpawner = flyingObjectSpawnScript;


        counterText.text = "0 / " + realMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
