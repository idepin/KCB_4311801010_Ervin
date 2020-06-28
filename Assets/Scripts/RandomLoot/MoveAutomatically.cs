using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAutomatically : MonoBehaviour
{
    public float speed = 5.0f;
    public float lifetime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy (gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
    }
}
