using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject enemy_type_a;
    public GameObject enemy_type_b;

    private enum Enemies {
        TWITCH,
        SQUARE
    }
    private GameObject player;

    private float curr_time;
    private float cool_down_time;
    private Vector3 start_position;
    private Vector3 target_position;
    private float radius = 15f;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update () {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
        // Debug.Log (enemies.Length);
        if (enemies.Length == 0) {
            SpawnEnemy( (Enemies) (Random.Range(0,9) % 2) );
        }
	}

    void SpawnEnemy(Enemies type) {
        switch (type) {
            case Enemies.TWITCH:
            {
                // spawn three enemies that stay in position and twitch
                int num_enemies = 3;

                for (int e = 0; e < num_enemies; e++) {
                    start_position = new Vector3 (player.transform.position.x,
                                                  player.transform.position.y,
                                                  player.transform.position.z - radius );
                    target_position = new Vector3 (radius * Mathf.Cos(e * Mathf.PI / (num_enemies-1)),
                                                   radius * Mathf.Sin(e * Mathf.PI / (num_enemies-1)) - (radius / 4),
                                                   radius );
                    GameObject enemy = Instantiate (enemy_type_a, start_position, Quaternion.identity) as GameObject;
                    enemy.GetComponent<EnemyControllerA>().SetTargetPosition(target_position);
                }
                break;
            }
            case Enemies.SQUARE:
            {
                // spawn three enemies that stay in position and twitch
                int num_enemies = 4;

                for (int i = 0; i < num_enemies; i++) {
                    start_position = new Vector3 (player.transform.position.x,
                                                  player.transform.position.y,
                                                  player.transform.position.z - radius );
                    target_position = new Vector3 (radius * Mathf.Cos(i * 2 * Mathf.PI / num_enemies + (Mathf.PI/4)),
                                                   radius * Mathf.Sin(i * 2 * Mathf.PI / num_enemies + (Mathf.PI/4)),
                                                   radius );
                    GameObject enemy = Instantiate (enemy_type_b, start_position, Quaternion.identity) as GameObject;
                    enemy.GetComponent<EnemyControllerB>().Initialize(i, num_enemies);
                }
                break;
            }
        }
    }
}
