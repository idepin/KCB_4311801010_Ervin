using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainFarm : MonoBehaviour
{
    [Header("Main Object")]
    public int tanahStats; //1.rumput - 2.tanah - 3.digali -4.disiram
    public int tanamanStats; //1.bibit - 2.tumbuh1 dst
    public int pupukStats; //1.ada pupuk - 0.tidak ada

    public GameObject tanahObj;
    public GameObject tanamanObj;
    public GameObject pupukObj;

    [Header("Atribute")]
    public int subur;
    public int lembab;
    public int suburRand;
    public Image suburImage;
    public int kesehatanTanaman;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Player.playerTool == "sabit")
            {
                tanahStats = 2;
            } else if (Player.playerTool == "cangkul" && tanahStats == 2)
            {
                tanahStats = 3;
            } else if (Player.playerTool == "gembor" && tanahStats == 3)
            {
                tanahStats = 4;
                lembab = 10;
            } else if (Player.playerTool == "bibit" && tanahStats == 4)
            {
                tanamanObj.gameObject.SetActive(true);
                tanamanStats = 1;
            } else if (Player.playerTool == "pupuk" && (tanahStats == 4 || tanahStats == 3))
            {
                pupukStats = 1;
                subur = 10;
            }
        }
    }

    public void tanahFunction()
    {
        if (tanahStats == 2)
        {
            tanahObj.GetComponent<Animator>().SetBool("diSabit", true);
        } else if (tanahStats == 3)
        {
            tanahObj.GetComponent<Animator>().SetBool("diSabit", false);
            tanahObj.GetComponent<Animator>().SetBool("diCangkul", true);
            tanahObj.GetComponent<Animator>().SetBool("diSiram", false);
        } else if (tanahStats == 4)
        {
            tanahObj.GetComponent<Animator>().SetBool("diCangkul", false);
            tanahObj.GetComponent<Animator>().SetBool("diSiram", true);
        }
    }

    public void pupukFunction()
    {
        if (pupukStats == 1 || subur == 10)
        {
            pupukObj.gameObject.SetActive(true);
        } else
        {
            pupukObj.gameObject.SetActive(false);
        }
    }

    public void tanamanFunction()
    {
        if (kesehatanTanaman >= 1800)
        {
            tanamanObj.GetComponent<Animator>().SetInteger("prosesJagung", 45);
        } else if (kesehatanTanaman >= 1200)
        {
            tanamanObj.GetComponent<Animator>().SetInteger("prosesJagung",35);
        } else if (kesehatanTanaman >= 600)
        {
            tanamanObj.GetComponent<Animator>().SetInteger("prosesJagung", 25);
        } else if (kesehatanTanaman >= 200)
        {
            tanamanObj.GetComponent<Animator>().SetInteger("prosesJagung", 15);
        }
    }

    public void RandomSubur()
    {
        suburRand = Random.Range(1, 11);
        if (suburRand <= 6)
        {
            subur = 10;
        } else
        {
            subur = Random.Range(5, 11);
        }
    }

    public void suburBar()
    {
        suburImage.fillAmount = (subur / 10f);
    }

    public void dayChange()
    {
        kesehatanTanaman += (subur + lembab);
        lembab = 0;
        tanahStats = 3;
        pupukStats = 0;
        RandomSubur();
    }

    void Start()
    {
        GantiHari.changeEvent += dayChange;
        tanahStats = 1;
        lembab = 0;
        subur = Random.Range(5, 11);
    }

    // Update is called one per frame
    void Update()
    {
        tanahFunction();
        tanamanFunction();
        pupukFunction();
        suburBar();
    }
}
