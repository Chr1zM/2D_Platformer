using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : MonoBehaviour {
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float radius;
    [SerializeField] private GameObject chainPrefab;

    private float angle = 0.0f;
    private List<GameObject> chainLinks = new List<GameObject>();

    private void Start() {
        // init dynamic Chain for the swinging Ball
        for (int i = 0; i < (int)radius * 4; i++) {
            GameObject chainLink = Instantiate(chainPrefab);
            chainLink.transform.position = transform.position;
            chainLinks.Add(chainLink);
        }
    }

    private void Update() {
        // calc Ball position
        angle += rotationSpeed * Time.deltaTime;
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, transform.position.z);

        UpdateChain();
    }

    private void UpdateChain() {
        for (int i = 0; i < chainLinks.Count; i++) {
            // calc Chain positions
            float t = (i + 1) / (float)chainLinks.Count;
            Vector3 chainPosition = Vector3.Lerp(centerPoint.position, transform.position, t);
            chainLinks[i].transform.position = chainPosition;
        }
    }
}
