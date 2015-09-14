using UnityEngine;
using System.Collections;

public class EnemyControllerB : MonoBehaviour {
    private int enemy_index;
    private int rot_index;
    private int prev_index;
    private int next_index;
    private int tot_enemies;
    private Vector3 start_pos;
    private Vector3[] target_pos;
    private float start_time;
    private float period_start_time;
    private float curr_time;
    private float init_progress;
    private float rot_progress;
    private float period = 2f;
    private float duration = 0.5f;
    private float radius = 13f;

    void Start() {
        start_pos = transform.position;
    }

    void Update() {
        curr_time = Time.time;

        init_progress = (curr_time - start_time) /  duration;
        if (init_progress >= 1) {
            rot_progress = (curr_time - period_start_time) / period;

            if (rot_progress >= 1) {
                rot_index++;
                period_start_time = curr_time;
            } else {
                prev_index = (rot_index + enemy_index) % tot_enemies;
                next_index = (rot_index + enemy_index + 1) % tot_enemies;

                transform.position = new Vector3 (
                    target_pos[prev_index].x + (target_pos[next_index].x - target_pos[prev_index].x) * rot_progress,
                    target_pos[prev_index].y + (target_pos[next_index].y - target_pos[prev_index].y) * rot_progress,
                    target_pos[prev_index].z + (target_pos[next_index].z - target_pos[prev_index].z) * rot_progress );
            }

        } else {
            // move toward target position
            transform.position = new Vector3 (start_pos.x + (target_pos[enemy_index].x - start_pos.x) * init_progress,
                                              start_pos.y + (target_pos[enemy_index].y - start_pos.y) * init_progress,
                                              start_pos.z + (target_pos[enemy_index].z - start_pos.z) * init_progress );
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

    public void Initialize (int index, int num_enemies) {
        float delta_angle;

        enemy_index = index;
        tot_enemies = num_enemies;

        start_time = Time.time;
        period_start_time = Time.time + duration;
        rot_index = 0;

        target_pos = new Vector3[4];

        for (int i = 0; i < tot_enemies; i++) {
            delta_angle = 2 * Mathf.PI / tot_enemies;

            target_pos[i] = new Vector3 (radius * Mathf.Cos( delta_angle * i + delta_angle / 2),
                                         radius * Mathf.Sin( delta_angle * i + delta_angle / 2),
                                         radius );
        }
    }
}
