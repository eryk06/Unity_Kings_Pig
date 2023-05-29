using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightScript : MonoBehaviour
{
    public float speed; // vận tốc di chuyển
    public float width; // độ rộng di chuyển lên đến
    private Vector3 originalPosition; // vị trí ban đầu 

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(GoRight());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator GoRight()
    {
        // đi lên
        while (true)
        {
            transform.position = new Vector2(
                transform.position.x + speed * Time.deltaTime,
                transform.position.y 
            );
            if (transform.position.x > originalPosition.x + width)
            {
                break;
            }
            yield return null; // chạy liên tục
        }
        StartCoroutine(GoLeft());
    }
    IEnumerator GoLeft()
    {
        bool stop = false;
        while (!stop)
        {
            yield return new WaitForSeconds(2);
            stop = true;
        }
        // đi xuống
        while (true)
        {
            transform.position = new Vector2(
                transform.position.x - speed * Time.deltaTime,
                transform.position.y 
            );
            if (transform.position.x < originalPosition.x)
            {
                transform.position = originalPosition;
                break;
            }
            yield return null; // chạy liên tục
        }
        StartCoroutine(GoRight());
    }
}
