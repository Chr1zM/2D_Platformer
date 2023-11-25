using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {
    [SerializeField] private float fanForce = 20f;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // DO NOTHING
            Debug.Log("Colliding with Fan");
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Player in fan area.");
            ApplyFanForce(collision.gameObject);
        }
    }

    private void ApplyFanForce(GameObject player) {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();

        Vector2 fanToPlayer = player.transform.position - transform.position;
        fanToPlayer.Normalize();
        Vector2 forceDirection = DetermineForceDirection(fanToPlayer);

        Debug.Log($"Applying force to player. Direction: {forceDirection * fanForce}");
        playerRigidbody.AddForce(forceDirection * fanForce, ForceMode2D.Impulse);
    }

    private Vector2 DetermineForceDirection(Vector2 fanToPlayer) {
        // Bestimme, ob der Spieler über, unter, rechts oder links vom Fan ist
        float x = Mathf.Abs(fanToPlayer.x);
        float y = Mathf.Abs(fanToPlayer.y);

        if (x > y) {
            // Spieler ist links oder rechts vom Fan
            return new Vector2(fanToPlayer.x > 0 ? 1 : -1, 0);
        } else {
            // Spieler ist über oder unter dem Fan
            return new Vector2(0, fanToPlayer.y > 0 ? 1 : -1);
        }
    }
}
