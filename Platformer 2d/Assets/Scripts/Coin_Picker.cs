using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin_Picker : MonoBehaviour
{
    [SerializeField]
    private TMP_Text coinText;

    private float coins = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coins++;
            coinText.text = coins.ToString();
            Destroy(collision.gameObject);
        }
    }
}
