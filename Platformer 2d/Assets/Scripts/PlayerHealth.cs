using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public Text textHealth;
    public GameObject player;
    private float playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<PlayerController>().health;
        textHealth.text = playerHealth.ToString();
    }
}
