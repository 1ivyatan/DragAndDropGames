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

    public CameraScript cameraScript;
    public static CameraScript cameraScr;

    private static void checkVictory() {
        counterText.text = counter + " / " + realMax;

        if (counter != realMax) return;

        TickerScript.mustTick = false;
        flierSpawner.isActivated = false;

        while (flierSpawner.spawnPoint.childCount > 0) {
            DestroyImmediate(flierSpawner.spawnPoint.GetChild(0).gameObject);
        }

        CameraScript.isActive = false;

        //cameraScr.cam.GetComponent<Canvas>().gameObject.SetActive(false);


        //cameraScr.cam.transform.Find("Canvas").GetComponent<Canvas>().gameObject.SetActive(false);

        Transform floatingHangarHud = hangarHud.transform.Find("HangarMenu");


        if (lostCar) {
            gameEndHud.transform.Find("EndText").GetComponent<Text>().text = "Neuzvarēji!\n" + counter + " / " + realMax + " (" + max + ")" + "\n" + floatingHangarHud.transform.Find("TimeText").GetComponent<Text>().text;                                    
            gameEndHud.transform.Find("StarsText").GetComponent<Text>().text = "";
        } else {
            gameEndHud.transform.Find("EndText").GetComponent<Text>().text = "Uzvarēji!\n" + counter + " / " + realMax + "\n" + floatingHangarHud.transform.Find("TimeText").GetComponent<Text>().text;

            if (ticker.getTime().TotalSeconds <= 250) {
                gameEndHud.transform.Find("StarsText").GetComponent<Text>().text = "★★★";
            } else if (ticker.getTime().TotalSeconds <= 360) {
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

        cameraScr = cameraScript;

        counterText.text = "0 / " + realMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
