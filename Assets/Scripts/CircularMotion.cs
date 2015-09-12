using UnityEngine;
using System.Collections;

public class CircularMotion : MonoBehaviour {
    public float radius;
    public float period;

    private float angle;
    private float start_time;

    void Start() {
        start_time = Time.time;
    }

	void Update () {
        angle = 2 * Mathf.PI * (Time.time - start_time) / period;

        transform.position = new Vector3 (radius * Mathf.Cos(angle),
                                          radius * Mathf.Sin(angle),
                                          transform.position.z);
	}
}
