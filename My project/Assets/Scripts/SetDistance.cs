using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDistance : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Transform playerTwo;
    private Transform selectedPlayer;
    private float selectedPlayerDistance;
    public float maxScale = 2f; // Maximum scale the object can reach
    public float minDistance = 1f; // Minimum distance at which scaling starts
    public float maxDistance = 10f; // Max

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the object and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, playerTwo.position);

        if (distanceToPlayer > distanceToPlayer2) {
            selectedPlayer = playerTwo;
            selectedPlayerDistance = distanceToPlayer2;
        } else {
            selectedPlayer = player;
            selectedPlayerDistance = distanceToPlayer;
        }

        // If the distance is within the range, scale the object
        if (selectedPlayerDistance >= minDistance && selectedPlayerDistance <= maxDistance)
        {
            // Calculate the scale factor based on the distance
            float scaleFactor = Mathf.InverseLerp(maxDistance, minDistance, selectedPlayerDistance);

            // Apply the scale factor to the object's scale
            transform.localScale = Vector3.one * Mathf.Lerp(1f, maxScale, scaleFactor);
        }
        else
        {
            // If the distance is outside the range, set the scale to its default
            transform.localScale = Vector3.one;
        }
    }
}
    
