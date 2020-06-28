using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] arrows;
    public Vector2 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    int randArrow;

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            randArrow = Random.Range(0, 2);
            spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
            Vector2 spawnPosition = new Vector2(transform.position.x, Random.Range(-spawnValues.y, spawnValues.y));
            Instantiate(arrows[randArrow], spawnPosition, gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
