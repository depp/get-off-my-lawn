using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMelee : MonoBehaviour {

    [SerializeField]
    private CircleCollider2D areaOfEffect = null;

    private Transform carrying = null;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            GameObject[] inCircle = Physics2D.OverlapCircleAll(areaOfEffect.transform.position, areaOfEffect.radius * AverageParentXY(areaOfEffect.transform)).Select(c => c.gameObject).ToArray();
            GameObject[] items = inCircle.Where(c => c.CompareTag("Item")).ToArray();
            if (items.Length > 0) {
                RecordPlayer recordPlayer = items[0].GetComponent<RecordPlayer>();
                recordPlayer.MusicPlaying = true;
                if (!carrying)
                    carrying = recordPlayer.transform;
                else
                    carrying = null;
            } else {
                GameObject[] kidsHit = inCircle.Where(c => c.CompareTag("Kid")).ToArray();
                foreach (GameObject kid in kidsHit) {
                    kid.GetComponent<MoveKid>().SetRunning();
                    Destroy(kid, 10);
                }
            }
        }

        if (carrying) {
            carrying.position = transform.position;
        }
    }

    private static float AverageParentXY(Transform child) {
        Transform parent = child.parent;
        return (parent.localScale.x + parent.localScale.y) / 2;
    }

}