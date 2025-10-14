using UnityEngine;

public class CarPlacerScript : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public ObjectScript objectScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(objectScript.vehicles);

        
        GameObject[] shuffledSpots = new GameObject[spawnPoints.Length];
        spawnPoints.CopyTo(shuffledSpots, 0);

        System.Random rnd = new System.Random();

        int n = shuffledSpots.Length;
        while (n > 1)
        {
            int k = rnd.Next(n--);
            GameObject temp = shuffledSpots[n];
            shuffledSpots[n] = shuffledSpots[k];
            shuffledSpots[k] = temp;
        }

        for (int i = 0; i < shuffledSpots.Length; i++) {
            objectScript.vehicles[i].transform.position = new Vector3(shuffledSpots[i].transform.position.x, shuffledSpots[i].transform.position.y, shuffledSpots[i].transform.position.z);
//            Debug.Log(shuffledSpots[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
