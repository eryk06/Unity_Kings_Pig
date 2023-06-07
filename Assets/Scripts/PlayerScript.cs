using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
  private float speed;
  public bool isNen;
  private new Rigidbody2D rigidbody2D;
  public bool isRight;
  public AudioClip diamondClip, hpClip;
  private int countPoint = 0;
  private int countHPPoint = 3;
  public TMP_Text txtDiamond, txtHP , txtTime;
  private int time = 0;
  private AudioSource audioSource;
  public GameObject menu;
  private bool Play = true;
  public GameObject otherGameObject;
  private BoxCollider2D boxCollider;
  private void Awake() {
    boxCollider = otherGameObject.GetComponent<BoxCollider2D>();
  }

  // animator
  private Animator animator;
  // Start is called before the first frame update
  void Start()
  {
    rigidbody2D = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
    txtDiamond.text = countPoint + " ";
    txtHP.text = countHPPoint + " ";
    txtTime.text = time + " ";
    StartCoroutine(Updatetime());
  }

  // Update is called once per frame
  void Update()
  {
    animator.SetBool("isRunning", false);
    animator.SetBool("isJump", false);

    Attack();

    Vector2 scale = transform.localScale;

    if (Input.GetKeyDown(KeyCode.Space))
    {
      var x = transform.position.x + (isRight ? 0.5f : -0.5f);
      var y = transform.position.y;
      var z = transform.position.z;
    }

    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
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

    if (Input.GetKeyDown(KeyCode.F))
    {
      Menu();
    }

    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
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

    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
    {
      rigidbody2D.AddForce(new Vector2(0, 560));
      isNen = false;
    }
    if (!isNen)
    {
      animator.SetBool("isJump", true);
    }
  }

  IEnumerator Updatetime()
  {
    while (true)
      {
        time++;
        txtTime.text = time + "s";
        yield return new WaitForSeconds(1);
      }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("San"))
    {
      isNen = true;
    }
    if (collision.gameObject.CompareTag("Enemy"))
    {
      Destroy(collision.gameObject);
    }
  }

  public void Menu()
  {
    if (Play)
    {
        menu.SetActive(true);
        Time.timeScale = 0;
        Play = false;
    }
    else
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        Play = true;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    var name = collision.gameObject.tag;
    if (name.Equals("Diamond"))
    {
      countPoint += 10;
      txtDiamond.text = countPoint + " ";
      PlayClip(diamondClip);
      // xóa mất kim cương
      Destroy(collision.gameObject);
    }
    if (name.Equals("HP"))
    {
      countHPPoint += 1;
      txtHP.text = countHPPoint + " ";
      PlayClip(hpClip);
      // xóa mất trái tim
      Destroy(collision.gameObject);
    }
  }

  private void PlayClip(AudioClip clip)
  {
    audioSource.PlayOneShot(clip);
  }

  void Attack()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      animator.SetBool("Attack", true);
      boxCollider.enabled = true;
    }
    if (Input.GetButtonUp("Fire1"))
    {
      animator.SetBool("Attack", false);
      boxCollider.enabled = false;
    }
  }

  public void Home()
  {
    menu.SetActive(false);
    Time.timeScale = 1;
    Play = true;
    SceneManager.LoadScene(0);
  }

  public void RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Time.timeScale = 1;
    Play = true;
  }

}
