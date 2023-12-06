using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed ;
    [SerializeField]
    private float rotationSpeed ;

    

    [SerializeField]
    private Vector3 relativePosition;

    private Vector3 startPosition;
    private Vector3 endPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + relativePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                endPosition = startPosition;
                startPosition = transform.position;
            }
        }
        if (rotationSpeed > 0)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }
    }
}
