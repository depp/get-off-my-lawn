using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMelee : MonoBehaviour {

    [SerializeField]
    private CircleCollider2D areaOfEffect = null;

    private Transform carrying = null;
    private ParticleSpawner carryingSpawner = null;

    public int PlayerNumber { get; set; }

    [SerializeField]
    private GameObject phonographArms = null, regularArms = null;

    [SerializeField]
    private AudioClip missSound = null;

    [SerializeField]
    private AudioClip[] hitSounds = null;

    private void Update() {
        if (Input.GetButtonDown("P" + PlayerNumber + " Interact")) {
            Animator[] animators = regularArms.GetComponentsInChildren<Animator>();
            foreach (Animator a in animators)
                a.Play("Arm Swing");

            GameObject[] inCircle = Physics2D.OverlapCircleAll(areaOfEffect.transform.position, areaOfEffect.radius * AverageParentXY(areaOfEffect.transform)).Select(c => c.gameObject).ToArray();
            GameObject[] items = inCircle.Where(c => c.CompareTag("Item")).ToArray();
            if (items.Length > 0) {
                RecordPlayer recordPlayer = items[0].GetComponent<RecordPlayer>();
                recordPlayer.MusicPlaying = true;
                if (!carrying) {
                    carrying = recordPlayer.transform;
                    carryingSpawner = carrying.GetComponentInChildren<ParticleSpawner>();
                    carrying.Find("Phonograph").gameObject.SetActive(false);
                    carrying.GetComponent<RecordPlayer>().GetParticleSpawner().enabled = true;
                } else {
                    carrying.transform.Find("Phonograph").gameObject.SetActive(true);
                    carrying = null;
                    carryingSpawner = null;
                }
            } else {
                GameObject[] kidsHit = inCircle.Where(c => c.CompareTag("Kid")).ToArray();
                if (kidsHit.Length == 0)
                    AudioSource.PlayClipAtPoint(missSound, Vector3.zero);
                else
                    AudioSource.PlayClipAtPoint(hitSounds[Random.Range(0, hitSounds.Length)], Vector3.zero);
                foreach (GameObject kid in kidsHit) {
                    kid.GetComponent<MoveKid>().SetRunning();
                    Destroy(kid, 10);
                }
            }
        }

        if (carrying) {
            carrying.position = transform.position;
            if (transform.localScale.x < 0) {
                carrying.localScale = new Vector3(-Mathf.Abs(carrying.localScale.x), carrying.localScale.y, carrying.localScale.z);
                carryingSpawner.velocity = -Mathf.Abs(carryingSpawner.velocity);
            } else {
                carrying.localScale = new Vector3(Mathf.Abs(carrying.localScale.x), carrying.localScale.y, carrying.localScale.z);
                carryingSpawner.velocity = Mathf.Abs(carryingSpawner.velocity);
            }
            if (!phonographArms.activeSelf) {
                phonographArms.SetActive(true);
                regularArms.SetActive(false);
            }
        } else {
            if (!regularArms.activeSelf) {
                phonographArms.SetActive(false);
                regularArms.SetActive(true);
            }
        }
    }

    private static float AverageParentXY(Transform child) {
        Transform parent = child.parent;
        return (parent.localScale.x + parent.localScale.y) / 2;
    }

}