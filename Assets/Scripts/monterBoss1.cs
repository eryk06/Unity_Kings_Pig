using System.Collections;
using UnityEngine;

public class monterBoss1 : MonoBehaviour
{
    public float moveSpeed = 5f;  // Tốc độ di chuyển
    public float moveDistance = 10f;  // Khoảng cách di chuyển qua trái và phải
    public float scaleTime = 5f;  // Thời gian để thay đổi tỷ lệ scale

    private bool isMovingRight = true;  // Biến xác định hướng di chuyển
    private bool isScaling = false;  // Biến xác định con quái đang thay đổi tỷ lệ scale
    private Vector3 originalScale;  // Tỉ lệ scale ban đầu

    private void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(ScaleAfterDelay(scaleTime));
    }

    private void Update()
    {
        if (!isScaling)
        {
            Move();
        }
    }

    private void Move()
    {
        float moveDelta = moveSpeed * Time.deltaTime;

        if (isMovingRight)
        {
            transform.Translate(Vector3.right * moveDelta);

            if (transform.position.x >= moveDistance)
            {
                transform.position = new Vector3(moveDistance, transform.position.y, transform.position.z);
                isMovingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveDelta);

            if (transform.position.x <= -moveDistance)
            {
                transform.position = new Vector3(-moveDistance, transform.position.y, transform.position.z);
                isMovingRight = true;
            }
        }
    }

    private IEnumerator ScaleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        isScaling = true;
        transform.localScale = new Vector3(25f, 25f, originalScale.z);
        yield return new WaitForSeconds(0.1f);  // Đợi 0.1 giây để hiển thị tỷ lệ scale mới

        isScaling = false;
    }
}
