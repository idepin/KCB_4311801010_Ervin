using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumber : MonoBehaviour
{
    public int spawnSomething;
    public GameObject spawnObject;
    public bool isSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        randomNum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randomNum()
    {
        for (int i = 0; i < 15; i++)
        {
            int x = Random.Range(1, 100);
            print(x);
        }
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            spawnSomething = Random.Range(0, 2);
            if  (spawnSomething == 1 && !isSpawn)
            {
                Instantiate(spawnObject, transform.position, Quaternion.identity);
                isSpawn = true;
            }
        }
    }
}
