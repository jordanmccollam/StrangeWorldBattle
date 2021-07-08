using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Orb : MonoBehaviour
{
    public float speed;
    public float ttl;

    private bool activated = false;
    private GameObject equippedBy;
    private OrbSpawner spawner;

    private void Start() {
        spawner = FindObjectOfType<OrbSpawner>();
    }

    private void Update() {
        if (activated) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (!activated) {
                PlayerController playerComp = other.GetComponent<PlayerController>();
                if (playerComp.equipped == null) {
                    playerComp.equipped = gameObject;
                    equippedBy = other.gameObject;

                    transform.parent = playerComp.hand;
                    transform.position = playerComp.hand.position;
                    transform.rotation = playerComp.hand.rotation;
                }
            } else if (activated && equippedBy != other.gameObject) {
                // Deal damage to player
                DestroyOrb();
            }
        }
    }

    public void Activate() {
        transform.parent = null;
        activated = true;
        Invoke("DestroyOrb", ttl);
    }

    private void DestroyOrb() {
        spawner.currentlySpawned--;
        Destroy(gameObject);
    }
}
