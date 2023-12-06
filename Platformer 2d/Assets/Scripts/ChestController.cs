using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;

    private bool isOpened ;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isOpened = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter: " + isOpened);
        if (!isOpened && collision.gameObject.tag == "Player")
        {
            Debug.Log("Opening the chest");
            animator.SetTrigger("toOpening");
            // Встановлюємо змінну стану відкриття в true
            isOpened = true;

            
        }
    }
    


}
