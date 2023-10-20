using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoin : MonoBehaviour
{

    public GameObject coins;
    public int CoinAmount;

    private void OnDestroy()
    {
        Coins();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
