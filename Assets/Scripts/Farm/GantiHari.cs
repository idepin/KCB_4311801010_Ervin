using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GantiHari : MonoBehaviour
{
    public Text dayText;
    public int dayCount;
    public delegate void ChangeEvent();
    public static event ChangeEvent changeEvent;

    // Start is called before the first frame update
    void Start()
    {
        dayCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        dayText.text = "Hari ke" + dayCount.ToString();
        dayChange();
    }

    public void dayChange()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            dayCount++;
            changeEvent();
        }
    }
}
