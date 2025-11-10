using System.Collections;
using UnityEngine;

public class CarPowerupScript : MonoBehaviour
{
    public static IEnumerator SlowDownTimeTemporarily(float seconds)
    {
        Time.timeScale = 0.4f;
        Debug.Log("Time slowed down to 0.4x for " + seconds + " sec");
        yield return new WaitForSeconds(seconds);

        Time.timeScale = 1.0f;
        Debug.Log("Time restored to normal!");

    }
}
