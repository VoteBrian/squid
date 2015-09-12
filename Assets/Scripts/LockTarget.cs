using UnityEngine;
using System.Collections;

public class LockTarget : MonoBehaviour {
    public GameObject shot;
    public GameObject target;

    private Ray ray;
    private RaycastHit hit;
    private GameObject[] enemy;
    private GameObject[] clone;
    private GameObject player;

    private int max_num_targets = 3;
    private GameObject[] target_handles;
    private int curr_target = 0;

    private bool ignore = false;

    private Vector3 offset;

    void Start() {
        // we need a handle to the player object as starting point for shots
        player = GameObject.Find("Player");

        offset = new Vector3 (0, 0, 2);
        target_handles = new GameObject[max_num_targets];
        clone = new GameObject[max_num_targets];
        enemy = new GameObject[max_num_targets];
    }

    void OnMouseDown() {
        curr_target = 0;
    }

    void OnMouseDrag() {
        ignore = false;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (hit.transform.CompareTag("Enemy")) {
                if (curr_target < max_num_targets) {
                    // make sure new target isn't already marked
                    for (int i = 0; i < curr_target; i++) {
                        if (hit.transform.gameObject == enemy[i]) {
                            ignore = true;
                        }
                    }

                    if (!ignore) {
                        enemy[curr_target] = hit.transform.gameObject;
                        target_handles[curr_target] = Instantiate (target,
                                                                   enemy[curr_target].transform.position - offset,
                                                                   Quaternion.identity) as GameObject;

                        curr_target++;
                    }
                }
            }
        }
    }

    void OnMouseUp() {
        if (curr_target > 0) {
            for (int i = 0; i < curr_target; i++) {
                // if the user clicked on an enemy, create instance of shot
                clone[i] = Instantiate (shot,
                                        player.transform.position,
                                        Quaternion.LookRotation(enemy[i].transform.position - player.transform.position)) as GameObject;

                // also, tell the shot what object it needs to track
                clone[i].GetComponent<ShotController>().SetTarget(enemy[i]);

                Destroy(target_handles[i]);
            }
        }
    }
}
