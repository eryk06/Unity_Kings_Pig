using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monterBossUpdate : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    public float speed;
    public bool isRight;
    public float left, right;

    private float timeSpawn; // thời gian sẽ bắn
    private float time; // đếm thời gian

    public GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timeSpawn = 1.0f; // 1s sẽ bắn
        time = timeSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        float positionX = transform.position.x; // global
        float positionY = transform.position.y; // global
        float playerX = player.transform.position.x; // vị trí player

        if (playerX > left && playerX < right)
        {
            if (playerX < positionX)
            {
                isRight = false;
            }
            else if (playerX > positionX)
            {
                isRight = true;
            }
        }

        if (positionX < left)
        {
            isRight = true;
        }
        if (positionX > right)
        {
            isRight = false;
        }

        Vector3 vector3;

        if (isRight)
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;

            vector3 = new Vector3(1, 2, 0);
        }
        else
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;

            vector3 = new Vector3(-1, 0, 0);
        }
        transform.Translate(vector3 * speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Gietquai")
        {
            Destroy(gameObject);
        }
    }

}