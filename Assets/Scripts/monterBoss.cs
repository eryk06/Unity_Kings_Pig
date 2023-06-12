using UnityEngine;

public class monterBoss : MonoBehaviour
{
    public GameObject bombPrefab;  // Prefab của quả bom
    public float throwForce = 5f;  // Lực ném quả bom
    public float bombLifetime = 2f;  // Thời gian tồn tại của quả bom
    public float throwInterval = 3f;  // Khoảng thời gian giữa các lần ném bom
    public float jumpForce = 10f;  // Lực nhảy của boss
    public int jumpCount = 5;  // Số lần nhảy của boss

    private int currentJumpCount = 0;  // Biến đếm số lần nhảy đã thực hiện

    Collider2D objectCollider;
    private new Rigidbody2D rigidbody2D;
    public Sprite newSprite; // hình chết
    public bool isAlive; // kiểm tra còn sống hay không
    private Vector2 originalPosition; // vị trí ban đầu
    public float speed;
    public float start, end;
    public bool isRight;
    public GameObject player;

    private void Start()
    {
        isAlive = true;
        InvokeRepeating("ThrowBombLeft", 1f, throwInterval);
        InvokeRepeating("ThrowBombRight", throwInterval / 2f, throwInterval);
        InvokeRepeating("Jump", 2f, 2f);
    }

    private void Update() {
        if (!isAlive) return;

        var positionEnemy = transform.position.x;
        var positionPlayer = player.transform.position.x;

        if (positionPlayer > start && positionPlayer < end)
        {
            if (positionPlayer < positionEnemy) isRight = false;
            else isRight = true;
        }

        if (positionEnemy < start)
        {
            isRight = true;
        }
        if (positionEnemy > end)
        {
            isRight = false;
        }

        Vector3 vector3;
        if (isRight)
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;

            vector3 = new Vector3(1, 0, 0);
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

    void ThrowBombLeft()
    {
        Vector2 spawnPosition = (Vector2)transform.position - new Vector2(1f, 0f);
        GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D bombRigidbody = bomb.AddComponent<Rigidbody2D>();
        CircleCollider2D bombCollider = bomb.AddComponent<CircleCollider2D>();

        bombRigidbody.AddForce(new Vector2(-throwForce, 0f), ForceMode2D.Impulse);

        Destroy(bomb, bombLifetime);
        Destroy(bomb, 2);
    }

    void ThrowBombRight()
    {
        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(1f, 0f);
        GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D bombRigidbody = bomb.AddComponent<Rigidbody2D>();
        CircleCollider2D bombCollider = bomb.AddComponent<CircleCollider2D>();

        bombRigidbody.AddForce(new Vector2(throwForce, 0f), ForceMode2D.Impulse);

        Destroy(bomb, bombLifetime);
    }

    void Jump()
    {
        if (currentJumpCount < jumpCount)
        {
            Rigidbody2D bossRigidbody = GetComponent<Rigidbody2D>();
            bossRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            currentJumpCount++;
        }
        else
        {
            CancelInvoke("Jump");  // Hủy việc gọi nhảy khi đã đạt số lần nhảy tối đa
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var direction = collision.GetContact(0).normal;
            // chạm dưới chân Player
            if (Mathf.Round(direction.y) == -1)
            {
                // chuyển thành hình chết
                GetComponent<SpriteRenderer>().sprite = newSprite;
                // tắt Animation
                GetComponent<Animator>().enabled = false;
                // tắt chuyển động
                isAlive = false;
                // bật trigger, đi xuống nền
                GetComponent<BoxCollider2D>().isTrigger = true;
                originalPosition = transform.position;
                // xóa khỏi game
                Destroy(gameObject, 1);
            }
        }

        if (collision.collider.tag == "Gietquai")
        {
            Destroy(gameObject);
        }

    }

}
