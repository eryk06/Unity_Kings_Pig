using UnityEngine;

public class monterBoob : MonoBehaviour
{
  Collider2D objectCollider;
  private new Rigidbody2D rigidbody2D;
  public Sprite newSprite; // hình chết
  public bool isAlive; // kiểm tra còn sống hay không
  private Vector2 originalPosition; // vị trí ban đầu

  public GameObject bombPrefab;  // Prefab của quả bom
  public float throwForce = 5f;  // Lực ném quả bom
  public int bombLifetime = 2;  // Thời gian tồn tại của quả bom

  private void Start()
  {
    isAlive = true;
    InvokeRepeating("ThrowBomb", 1f, 6f);  // Gọi hàm ThrowBomb mỗi 3 giây sau 1 giây
  }

  void ThrowBomb()
  {
    Vector2 spawnPosition = (Vector2)transform.position - new Vector2(1f, 0f); // Vị trí quả bom sẽ được tạo ra
    GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

    Rigidbody2D bombRigidbody = bomb.AddComponent<Rigidbody2D>();
    CircleCollider2D bombCollider = bomb.AddComponent<CircleCollider2D>();


    bombRigidbody.AddForce(new Vector2(-throwForce, 0f), ForceMode2D.Impulse); // Hướng lực ném qua bên trái

    Destroy(bomb, bombLifetime); // Hủy quả bom sau thời gian bombLifetime
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.collider.tag == "Gietquai")
    {
      Destroy(gameObject);
    }
  }
  
}
