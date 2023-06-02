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

  private void Start()
  {
    InvokeRepeating("ThrowBombLeft", 1f, throwInterval);
    InvokeRepeating("ThrowBombRight", throwInterval / 2f, throwInterval);
    InvokeRepeating("Jump", 2f, 2f);
  }

  void ThrowBombLeft()
  {
    Vector2 spawnPosition = (Vector2)transform.position - new Vector2(1f, 0f);
    GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);

    Rigidbody2D bombRigidbody = bomb.AddComponent<Rigidbody2D>();
    CircleCollider2D bombCollider = bomb.AddComponent<CircleCollider2D>();

    bombRigidbody.AddForce(new Vector2(-throwForce, 0f), ForceMode2D.Impulse);

    Destroy(bomb, bombLifetime);
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
}
