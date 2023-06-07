using UnityEngine;

public class monterRun : MonoBehaviour
{
  public float speed;
  public float start, end;
  public bool isRight;
  private Vector2 originalPosition; // vị trí ban đầu
  public Sprite newSprite; // hình chết

  private bool isAlive; //kiếm tra sống/chết
  private Vector2 originalPostion;
  public GameObject player;

  void Start()
  {
    isAlive = true;
  }

  // Update is called once per frame
  void Update()
  {
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

}



