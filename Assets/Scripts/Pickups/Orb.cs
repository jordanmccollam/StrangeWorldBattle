using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public float speed;
    public float ttl;

    private bool activated = false;
    private bool pickedUp = false;

    void Update()
    {
        if (activated) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && pickedUp == false) {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.pickedUp == false) {
                pickedUp = true;
                transform.parent = player.hand.transform;
                transform.position = new Vector2(player.hand.transform.position.x + 1f, player.hand.transform.position.y + 0.5f);
                transform.rotation = player.transform.rotation;
                player.pickedUp = true;
            }
        }
        else if (other.tag == "Player" && pickedUp == true) {
            // TODO: do damage to player (orb has been shot)
        }
    }

    public void Activate() {
        transform.parent = null;
        activated = true;
        Invoke("DestroyOrb", ttl);
    }

    private void DestroyOrb() {
        Destroy(gameObject);
    }
}
