using UnityEngine;
using System.Collections;

public class EnemiesCommon : MonoBehaviour {
    private bool selected = false;

    public bool IsSelected() {
        return selected;
    }

    public void SetSelected() {
        selected = true;
    }

    public void SelfDestruct() {
        Destroy (gameObject);
    }
}
