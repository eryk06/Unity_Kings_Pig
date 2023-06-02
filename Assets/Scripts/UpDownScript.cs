using System.Collections;
using UnityEngine;

public class UpDownScript : MonoBehaviour
{
  public float speed; // vận tốc di chuyển
  public float height; // độ cao lên đến
  private Vector3 originalPosition; // vị trí ban đầu 

  // Start is called before the first frame update
  void Start()
  {
    originalPosition = transform.position;
    StartCoroutine(GoUp());
  }

  // Update is called once per frame
  void Update()
  {

  }

  IEnumerator GoUp()
  {
    // đi lên
    while (true)
    {
      transform.position = new Vector2(
          transform.position.x,
          transform.position.y + speed * Time.deltaTime
      );
      if (transform.position.y > originalPosition.y + height)
      {
        break;
      }
      yield return null; // chạy liên tục
    }
    StartCoroutine(GoDown());
  }
  IEnumerator GoDown()
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
          transform.position.x,
          transform.position.y - speed * Time.deltaTime
      );
      if (transform.position.y < originalPosition.y)
      {
        transform.position = originalPosition;
        break;
      }
      yield return null; // chạy liên tục
    }
    StartCoroutine(GoUp());
  }
}
