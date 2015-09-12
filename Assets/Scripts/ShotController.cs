using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {
    public float duration;

    private GameObject target;
    private float start_time;
    private float progress;

    private Vector3 start;
    private Vector3 middle;
    private Vector3 end;
    private Vector3 line1;
    private Vector3 line2;

    private bool isTargetSet = false;

	void Start () {
        // save the start position
        start = transform.position;

        // save the start time
        start_time = Time.time;
	}

    public void SetTarget(GameObject shot_target) {
        target = shot_target;

        // get initial endpoint
        // used to calcuate static midpoint
        end = target.transform.position;

        // set the midpoint
        middle.x = start.x + (end.x - start.x) * Random.Range(0.55f, 1.5f);
        middle.y = start.y + (end.y - start.y) * Random.Range(0.25f, 0.8f);
        middle.z = start.z + (end.z - start.z) * Random.Range(0.25f, 0.75f);

        isTargetSet = true;
    }


	void Update () {
        if (isTargetSet) {
            // calculate progress towards duration
            progress = (Time.time - start_time) / duration;
            if (progress > 1) {
                Destroy(gameObject);
            }

            // get the current target position
            end = target.transform.position;

            // calculate current shot position
            line1.x = start.x + (middle.x - start.x) * progress;
            line1.y = start.y + (middle.y - start.y) * progress;
            line1.z = start.z + (middle.z - start.z) * progress;

            line2.x = middle.x + (end.x - middle.x) * progress;
            line2.y = middle.y + (end.y - middle.y) * progress;
            line2.z = middle.z + (end.z - middle.z) * progress;

            // set shot position
            transform.position = new Vector3 (line1.x + (line2.x - line1.x) * progress,
                                              line1.y + (line2.y - line1.y) * progress,
                                              line1.z + (line2.z - line1.z) * progress );

            // set shot angle
            transform.rotation = Quaternion.LookRotation(line2 - line1);
        }
	}
}
