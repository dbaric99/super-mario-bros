using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    private const float RADIUS = 0.25f;
    private const float DISTANCE = 0.375f;

    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, RADIUS, direction.normalized, DISTANCE, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool DotPositionTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
