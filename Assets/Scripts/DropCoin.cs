using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Drops a coin on object destroyed
public class DropCoin : MonoBehaviour
{
    public GameObject coins;
    public int CoinAmount;

    private void OnDestroy()
    {
        Coins();
    }

    void Coins()

    {
        GameObject coinInstance1;

        for (int i = 0; i < CoinAmount; i++)
        {
            coinInstance1 = Instantiate(coins, transform.position + new Vector3(0.2f, 0.2f, 0.2f), Quaternion.identity);
        }
    }
}
