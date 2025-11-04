using System;
using UnityEngine;
using UnityEngine.UI;

public class TickerScript : MonoBehaviour
{
    public GameObject TickerText;

    private Text tickerTextString;
    public float timeTicked = 0;

    public static bool mustTick = true;

    public static string pubTimeText = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tickerTextString = TickerText.GetComponent<Text>();
        timeTicked = 0;
        mustTick = true;
        pubTimeText = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!mustTick) return;

        timeTicked += Time.deltaTime;

        TimeSpan time = TimeSpan.FromSeconds((int)timeTicked);

        tickerTextString.text = pubTimeText = time.ToString(@"hh\:mm\:ss");

    }

    public TimeSpan getTime() {
        return TimeSpan.FromSeconds((int)timeTicked);
    }
}
