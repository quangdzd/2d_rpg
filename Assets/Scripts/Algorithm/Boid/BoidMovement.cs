using UnityEngine;
using System.Collections.Generic;
public abstract class ABoidMovement : MonoBehaviour
{
    public EEnemyRank EnemyRank { get; set; }
    public float radius{ get; set; }
    public Vector3 Velocity { get; set; }

    protected virtual void Awake() { }

    public virtual void Move()
    {
        // default flocking logic
    }

    public virtual Vector2 Separation(List<ABoidMovement> boids) { return Vector2.zero; }
    public virtual Vector2 Aligment(List<ABoidMovement> boids) { return Vector2.zero; }
    public virtual Vector2 Cohesion(List<ABoidMovement> boids) { return Vector2.zero; }
    public virtual Vector2 Center() { return Vector2.zero; }

    public virtual Vector3 GetRandomRotatedDirection(Vector3 tangent)
    {
        return tangent;
    }
}
