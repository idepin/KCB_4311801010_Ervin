using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderSlot : MonoBehaviour
{
    public GameObject[] imageSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            this.transform.position = imageSlot[0].transform.position;
        } else if (Input.GetKey (KeyCode.Alpha2))
        {
            this.transform.position = imageSlot[1].transform.position;
        } else if (Input.GetKey(KeyCode.Alpha3))
        {
            this.transform.position = imageSlot[2].transform.position;
        } else if (Input.GetKey(KeyCode.Alpha4))
        {
            this.transform.position = imageSlot[3].transform.position;
        } else if (Input.GetKey(KeyCode.Alpha5))
        {
            this.transform.position = imageSlot[4].transform.position;
        } else if (Input.GetKey(KeyCode.Alpha6))
        {
            this.transform.position = imageSlot[5].transform.position;
        }
    }
}
