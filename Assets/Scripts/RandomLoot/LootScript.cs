using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    public GameObject coinObj;

    [System.Serializable]
    public class DropCurrency
    {
        public string name;
        public GameObject item;
        public int dropRarity;
    }

    public Animator myAnimator;
    public int dropChance;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<DropCurrency> LootTable = new List<DropCurrency>();

    public void calculateLoot()
    {
        int calc_dropChance = Random.Range(0, 101);

        if (calc_dropChance > dropChance)
        {
            Instantiate(coinObj, transform.position, Quaternion.identity);
            Debug.Log("anda dapat coin");
            return;
        } else if (calc_dropChance <= dropChance)
        {
            int itemWeight = 0;

            for (int i = 0; i < LootTable.Count; i++)
            {
                itemWeight += LootTable[i].dropRarity;
            }
            Debug.Log("itemWeight = " + itemWeight);

            int randomValue = Random.Range(0, itemWeight);

            for (int j = 0; j <LootTable.Count; j++)
            {
                if (randomValue <= LootTable[j].dropRarity)
                {
                    Instantiate(LootTable[j].item, transform.position, Quaternion.identity);
                    Debug.Log("anda dapat " + LootTable[j].name);
                    return;
                }
                randomValue -= LootTable[j].dropRarity;
            }
        }
    }

    void OnTriggerStay2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                myAnimator.SetTrigger("OpenChest");
                calculateLoot();
            }
        }
    }
}
