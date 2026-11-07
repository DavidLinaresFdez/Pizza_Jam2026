using UnityEngine;

public class Warrior : MonoBehaviour
{
    [SerializeField] private GameObject player;   // assign via GameManager / find at runtime
    [SerializeField] private float desiredDistance = 5f;   // distance to maintain
    [SerializeField] private float stopTolerance = 0.3f; // dead-zone so it doesn't jitter
    [SerializeField] private float warriorSpeed = 3f;
    [SerializeField] private Rigidbody2D rigidbody;

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector2 playerPos = player.transform.position;
        Vector2 myPos = rigidbody.position;

        // Direction FROM player TO warrior
        Vector2 awayFromPlayer = myPos - playerPos;
        float currentDistance = awayFromPlayer.magnitude;

        // Dead-zone: already at the right distance → don't move
        if (Mathf.Abs(currentDistance - desiredDistance) <= stopTolerance)
        {
            rigidbody.linearVelocity = Vector2.zero;
            return;
        }

        // Move toward the target position along the player→warrior axis
        Vector2 targetPos = playerPos + awayFromPlayer.normalized * desiredDistance;
        Vector2 toTarget = targetPos - myPos;

        // Normalize so speed is constant, then set velocity
        Vector2 velocity = toTarget.normalized * warriorSpeed;
        rigidbody.linearVelocity = velocity;

        // Optional: face the player / flip sprite
        // if (velocity.x != 0) transform.localScale = new Vector3(Mathf.Sign(velocity.x), 1, 1);
    }
}