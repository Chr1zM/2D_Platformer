using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {
    [SerializeField] private float fanForce = 20f;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            ApplyFanForce(collision.gameObject);
        }
    }

    private void ApplyFanForce(GameObject player) {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();

        Vector2 fanToPlayer = player.transform.position - transform.position;
        Vector2 forceDirection = DetermineForceDirection(fanToPlayer);
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
