using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumputTanah : MonoBehaviour
{
    public Sprite[] spriteRumput;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spriteRumput[0];
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Player.playerTool == "sabit")
            {
                GetComponent<SpriteRenderer>().sprite = spriteRumput[1];
            } else if (Player.playerTool == "cangkul")
            {
                GetComponent<SpriteRenderer>().sprite = spriteRumput[2];
            } else if (Player.playerTool == "gembor")
            {
                GetComponent<SpriteRenderer>().sprite = spriteRumput[3];
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
