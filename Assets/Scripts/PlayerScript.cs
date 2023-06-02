using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  private AudioSource audioSource;
  private bool isALive;
  private float speed;
  public bool isNen;
  private new Rigidbody2D rigidbody2D;
  public bool isRight;

  // animator
  private Animator animator;
  // Start is called before the first frame update
  void Start()
  {
    isALive = true;
    rigidbody2D = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    animator.SetBool("isRunning", false);
    animator.SetBool("isJump", false);

    Vector2 scale = transform.localScale;

    if (Input.GetKeyDown(KeyCode.Space))
    {
      var x = transform.position.x + (isRight ? 0.5f : -0.5f);
      var y = transform.position.y;
      var z = transform.position.z;
    }

    if (Input.GetKey(KeyCode.RightArrow))
    {
      if (isRight == false)
      {
        scale.x *= scale.x > 0 ? 1 : -1;
        transform.localScale = scale;
        isRight = true;
      }
      animator.SetBool("isRunning", true);
      scale.x = 8;
      transform.Translate(Vector3.right * 15f * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.LeftArrow))
    {
      if (isRight == true)
      {
        scale.x *= scale.x > 0 ? -1 : 1;
        transform.localScale = scale;
        isRight = false;
      }
      animator.SetBool("isRunning", true);
      scale.x = -8;
      transform.Translate(Vector3.left * 15f * Time.deltaTime);
    }

    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
      rigidbody2D.AddForce(new Vector2(0, 466));
      isNen = false;
    }
    if (!isNen)
    {
      animator.SetBool("isJump", true);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("San"))
    {
      isNen = true;
    }
  }

  private void PlayClip(AudioClip clip)
  {
    audioSource.PlayOneShot(clip);
  }

}
