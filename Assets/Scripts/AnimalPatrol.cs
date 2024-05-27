using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed = 2f;
    private float fixedY;  // Variable to hold the fixed y position

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isMoving", true);
        fixedY = transform.position.y;
        StartCoroutine(FreezeForSeconds(2f)); // Start the coroutine to freeze for 2 seconds
    }

    void Update()
    {
        Vector2 targetPoint = new Vector2(currentPoint.position.x, fixedY);  // Lock target point y position

        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        // Check if the animal is close to the current point
        if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
        {
            flip();
            currentPoint = currentPoint == pointB.transform ? pointA.transform : pointB.transform;
        }

        // Lock the animal's y position
        rb.position = new Vector2(rb.position.x, fixedY);
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private IEnumerator FreezeForSeconds(float seconds)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // Freeze the Rigidbody2D
        yield return new WaitForSeconds(seconds); // Wait for 2 seconds
        rb.constraints = RigidbodyConstraints2D.None; // Unfreeze the Rigidbody2D
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
            Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        }
    }
}
