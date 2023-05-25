using UnityEngine;
using System.Collections;

public class canon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public float bulletLifetime = 3f;

    private bool isFiring = false;

    void Start()
    {
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            FireBullet();
            yield return new WaitForSeconds(fireRate);
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-5f, 0f);

        Destroy(bullet, bulletLifetime);
    }
}
