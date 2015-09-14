using UnityEngine;
using System.Collections;

public class EnemyControllerA : MonoBehaviour {
    private Vector3 start_pos;
    private Vector3 target_pos;
    private float start_time;
    private float curr_time;
    private float progress;
    private float duration = 0.5f;

    void Start() {
        start_time = Time.time;
        start_pos = transform.position;
    }

    void Update() {
        curr_time = Time.time;

        progress = (curr_time - start_time) /  duration;
        if (progress >= 1) {
            // twitch in place
            transform.Rotate(GetTwitchAngle(), GetTwitchAngle(), GetTwitchAngle());
        } else {
            // move toward target position
            transform.position = new Vector3 (start_pos.x + (target_pos.x - start_pos.x) * progress,
                                              start_pos.y + (target_pos.y - start_pos.y) * progress,
                                              start_pos.z + (target_pos.z - start_pos.z) * progress );
        }

    }

    float GetTwitchAngle () {
        float a = Random.Range(-100f, 100f);
        if (Mathf.Abs(a) < 95) {
            a = 0f;
        } else {
            a = a/5;
        }
        return a;
    }

    public void SetTargetPosition(Vector3 target) {
        target_pos = target;
    }
}
