using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    float timer;
    public GameObject chickenPrefab;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 

        if(timer>= 2f)
        {
            timer = 0f;
            float x = Random.Range(-100f, 100f);
            float z = Random.Range(-100f, 100f);
            Vector3 position = new Vector3(x,0,z);
            Quaternion rotation = new Quaternion();
            Instantiate(chickenPrefab, position,rotation); 
        }
    }
}
