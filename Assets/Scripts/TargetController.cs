using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {
    private GameObject target;
    private Vector3 offset;

    void Start() {
        offset = new Vector3 (0, 0, 2);
    }

    public void SetTarget(GameObject shot_target) {
        target = shot_target;
    }

	void Update () {
        transform.position = new Vector3 (target.transform.position.x - offset.x,
                                          target.transform.position.y - offset.y,
                                          target.transform.position.z - offset.z);

        transform.Rotate (new Vector3 (0, 0, Random.Range(40, 100)) * Time.deltaTime);
	}
}
