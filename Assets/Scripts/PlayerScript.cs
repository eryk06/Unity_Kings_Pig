using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
  private bool isALive;
  private float speed;
  public bool isNen;
  private new Rigidbody2D rigidbody2D;
  public bool isRight;
  public AudioClip diamondClip, hpClip;
  private int countPoint = 0;
  private int countHPPoint = 10;
  public TMP_Text txtDiamond, txtHP, txtTime;
  private int time = 0;
  private AudioSource audioSource;
  public GameObject menu;
  private bool Play = true;
  public GameObject otherGameObject;
  private BoxCollider2D boxCollider;

  public int jumpForce;
  public int maxHeight;
  private bool canJump = true;

  private void Awake() {
    boxCollider = otherGameObject.GetComponent<BoxCollider2D>();
  }

  // animator
  private Animator animator;
  // Start is called before the first frame update
  void Start() {
    jumpForce = 10;
    maxHeight = 100;
    rigidbody2D = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
    txtDiamond.text = "x " + countPoint;
    txtHP.text = "x " + countHPPoint;
    isALive = true;
    time = 500;
    txtTime.text = time + "";
    StartCoroutine(UpdateTime());
  }

  // Update is called once per frame
  void Update() {
    jumpForce = 10;
    maxHeight = 100;
    animator.SetBool("isRunning", false);
    animator.SetBool("isJump", false);

    Attack();

    Vector2 scale = transform.localScale;

    if (Input.GetKeyDown(KeyCode.Space)) {
      var x = transform.position.x + (isRight ? 0.5f : -0.5f);
      var y = transform.position.y;
      var z = transform.position.z;
    }

    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
      if (isRight == false) {
        scale.x *= scale.x > 0 ? 1 : -1;
        transform.localScale = scale;
        isRight = true;
      }
      animator.SetBool("isRunning", true);
      scale.x = 8;
      transform.Translate(Vector3.right * 15f * Time.deltaTime);
    }

    if (Input.GetKeyDown(KeyCode.F)) {
      Menu();
    }

    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
      if (isRight == true) {
        scale.x *= scale.x > 0 ? -1 : 1;
        transform.localScale = scale;
        isRight = false;
      }
      animator.SetBool("isRunning", true);
      scale.x = -8;
      transform.Translate(Vector3.left * 15f * Time.deltaTime);
    }

    if (Input.GetKeyDown(KeyCode.UpArrow) && canJump ||
        Input.GetKeyDown(KeyCode.W) && canJump) {
      Jump();
      isNen = false;
    }
    if (!isNen) {
      animator.SetBool("isJump", true);
    }
  }

  private void Jump() {
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    if (rb != null) {
      if (transform.position.y < maxHeight) {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
      }
    }
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.CompareTag("San")) {
      isNen = true;
      canJump = true;
    }
    if (collision.gameObject.CompareTag("Home")) {
      Home();
    }
    if (collision.gameObject.CompareTag("Enemy")) {
      countHPPoint -= 1;
      txtHP.text = "x " + countHPPoint;
      if (countHPPoint == 0) {
        Home();
      }
    }
    if (collision.gameObject.CompareTag("CannonBall") ||
        collision.gameObject.CompareTag("bom")) {
      countHPPoint -= 1;
      txtHP.text = "x " + countHPPoint;
      if (countHPPoint == 0) {
        Home();
      }
    }
    if (collision.collider.CompareTag("NextLevel")) {
      SceneManager.LoadScene("Scene2");
      Time.timeScale = 1;
    }
  }

  private void OnCollisionExit2D(Collision2D collision) { canJump = false; }

  public void Menu() {
    if (Play) {
      menu.SetActive(true);
      Time.timeScale = 0;
      Play = false;
    } else {
      menu.SetActive(false);
      Time.timeScale = 1;
      Play = true;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    var name = collision.gameObject.tag;
    if (name.Equals("Diamond")) {
      countPoint += 10;
      txtDiamond.text = "x " + countPoint;
      PlayClip(diamondClip);
      // xóa mất kim cương
      Destroy(collision.gameObject);
    }
    if (name.Equals("HP")) {
      countHPPoint += 10;
      txtHP.text = "x " + countHPPoint;
      PlayClip(hpClip);
      // xóa mất trái tim
      Destroy(collision.gameObject);
    }
  }

  private void PlayClip(AudioClip clip) { audioSource.PlayOneShot(clip); }

  void Attack() {
    if (Input.GetButtonDown("Fire1")) {
      animator.SetBool("Attack", true);
      boxCollider.enabled = true;
    }
    if (Input.GetButtonUp("Fire1")) {
      animator.SetBool("Attack", false);
      boxCollider.enabled = false;
    }
  }

  public void Home() {
    menu.SetActive(false);
    Time.timeScale = 1;
    Play = true;
    SceneManager.LoadScene(0);
  }

  public void RestartGame() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Time.timeScale = 1;
    Play = true;
  }

  IEnumerator UpdateTime() {
    while (isALive) {
      time--;
      txtTime.text = time + "";
      yield return new WaitForSeconds(1);
      if (time == 0) {
        Home();
        Time.timeScale = 0;
      }
    }
  }
}
