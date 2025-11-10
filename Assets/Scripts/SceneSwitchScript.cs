using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchScript : MonoBehaviour
{

    public void toExit()
    {
        Application.Quit();
    }

    public void toScene(string name)
    {
        InterstitialAd interstitalAd = FindFirstObjectByType<InterstitialAd>();

        if (interstitalAd != null )
        {
            interstitalAd.ShowInterstitial();
        }

        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
