using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour
{
    [SerializeField] protected int coin;
    public Text txtCoin;
    public void AddCoin(int value)
    {
        coin += value;
        txtCoin.text = coin.ToString();
    }


}
