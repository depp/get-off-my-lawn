using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMelee : MonoBehaviour {

    [SerializeField]
    private CircleCollider2D areaOfEffect = null;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            GameObject[] kidsHit = Physics2D.OverlapCircleAll(areaOfEffect.transform.position, areaOfEffect.radius * AverageParentXY(areaOfEffect.transform)).Where(c => c.CompareTag("Kid")).Select(c => c.gameObject).ToArray();
            foreach (GameObject kid in kidsHit) {
                kid.GetComponent<MoveKid>().SetRunning();
                Destroy(kid, 10);
            }
        }
    }

    private static float AverageParentXY(Transform child) {
        Transform parent = child.parent;
        return (parent.localScale.x + parent.localScale.y) / 2;
    }

}