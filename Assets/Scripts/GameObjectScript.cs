using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectScript : MonoBehaviour {

  public Transform[] spawnPoints;
  public GameObject[] enemyPrefabs;

  private bool check;

  public int time; // đếm thời gian tính theo s

  // Start is called before the first frame update
  void Start() {
    check = true;

    StartCoroutine(UpdateTime());
  }

  // Update is called once per frame
  void Update() {

    for (int i = 0; i <= 15; i++) {

      UpdateTime();

      Debug.Log("Log Update: " + time);
    }
  }

  IEnumerator UpdateTime() {
    int randEnemy = Random.Range(0, enemyPrefabs.Length);
    int randSpawnPoint = Random.Range(0, spawnPoints.Length);

    while (check) {
      time--;
      yield return new WaitForSeconds(0.6f);

      if (time == 0) {
        Instantiate(enemyPrefabs[randEnemy],
                    spawnPoints[randSpawnPoint].position, transform.rotation);
        break;
      }
    }

    for (int i = 0; i <= 15; i++) {
      if (time == 0) {
        time = 5;
        while (check) {
          time--;
          yield return new WaitForSeconds(0.6f);

          if (time == 0) {
            Instantiate(enemyPrefabs[randEnemy],
                        spawnPoints[randSpawnPoint].position,
                        transform.rotation);
            break;
          }
        }
      }
    }
  }
}