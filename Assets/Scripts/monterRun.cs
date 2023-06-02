using UnityEngine;

public class monterRun : MonoBehaviour
{
  public float speed;
  public float left, right;
  public bool isRight;

  private bool isAlive; //kiếm tra sống/chết
  private Vector2 originalPostion;
  void Start()
  {
    isAlive = true;
  }

  // Update is called once per frame
  void Update()
  {
    if (!isAlive) return;// nếu đã chết thì tắt chuyển động
    float postionX = transform.position.x;
    if (postionX < left)
    {
      isRight = true;

    }
    else if (postionX > right)
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



