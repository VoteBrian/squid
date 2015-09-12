using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public GameObject shot;

    private Ray ray;
    private RaycastHit hit;
    private GameObject clone;

    void Update () {
        // Check for user click
        if (Input.GetButtonDown ("Fire1")) {
            // where did the user click
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if (hit.transform.CompareTag("Enemy")) {
                    // if the user clicked on an enemy, create instance of shot
                    clone = Instantiate (shot, transform.position,
                        Quaternion.LookRotation(hit.transform.position - transform.position)) as GameObject;

                    // also, tell the shot what object it needs to track
                    clone.GetComponent<ShotController>().SetTarget(hit.transform.gameObject);
                }
            }
        }
    }
}
