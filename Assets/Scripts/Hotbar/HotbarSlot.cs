using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSlot : MonoBehaviour
{
    private KeyCode _keyCode;
    private int _keyNumber;
    public string _toolName;

    private void OnValidate()
    {
        _keyNumber = transform.GetSiblingIndex() + 1;
        _keyCode = KeyCode.Alpha0 + _keyNumber;
        gameObject.name = "Hotbar Slot" + _keyNumber;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            Selected();
        }
    }

    public void Selected()
    {
        Player.playerTool = _toolName;
    }
}
