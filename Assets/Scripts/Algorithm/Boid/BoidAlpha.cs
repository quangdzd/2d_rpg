using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BoidAlpha : ABoidMovement
{
    private List<ABoidMovement> flockMembers;
    
    public int _count{ get; set; }

    private Boundary boundary;


    [SerializeField]
    public GameObject _gameObject;


    protected override void Awake()
    {
        EnemyRank = EEnemyRank.Alpha;
        flockMembers = new List<ABoidMovement>();
        Velocity = Random.insideUnitCircle.normalized;

    }
    void FixedUpdate()
    {
        Velocity = CaculateAlphaVelocity();
        transform.position += Velocity * Time.deltaTime;

        foreach (var member in flockMembers)
        {
            member.Velocity = Vector2.Lerp(member.Velocity, CaculateFlockMemberVelocity(member), Time.deltaTime);
            member.transform.position += member.Velocity* Time.deltaTime;
        }

    }

    public void GenMember()
    {
        for (int i = 0; i < _count; i++)
        {
            GameObject boid = Instantiate(_gameObject);
            float x = Random.Range(-2f, 2f);
            float y = Random.Range(-2f, 2f);
            boid.transform.position = new Vector2(transform.position.x +  x, transform.position.y + y);

            BoidNormal boidNormal = boid.AddComponent<BoidNormal>();
            boidNormal.Velocity = Random.insideUnitCircle.normalized; 
            flockMembers.Add(boidNormal);
        }
    }
    public void SetBoundary(Vector3 pos, EBoundary typeBoundary, int diameter = 0, int inradius = 0, Vector2 diameterXY = default)
    {
        boundary = new Boundary(pos, typeBoundary, diameter, inradius, diameterXY);
    }

    public  Vector2 CaculateAlphaVelocity()
    {
        Vector2 velocity = Center();

        return velocity;
    }
    public Vector2 CaculateFlockMemberVelocity(ABoidMovement boid)
    {

        Vector2 velocity = ((Vector2)boid.Velocity
        + 1f * Separation(flockMembers)
        + 0.7f * Aligment(flockMembers)
        + 0.2f * Cohesion(flockMembers)
        + 1f * (Vector2)(transform.position - boid.transform.position).normalized
        ).normalized;


        return velocity;
    }

    // public List<ABoidMovement> BoisinRange()
    // {
    //     return BoidManager.Instance.GetEnemiesNerest(transform.position, 1);
    // }

    // public  bool InVisionCone(Vector2 pos)
    // {
    //     Vector2 dirToPos = pos - (Vector2)transform.position;
    //     float dotProduct = Vector2.Dot(transform.forward, dirToPos);
    //     float cosHalfVisionAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);
    //     return dotProduct >= cosHalfVisionAngle;

    // }

    public override Vector2 Separation(List<ABoidMovement> boidNormals)
    {
        Vector2 dir = Vector2.zero;

        foreach (var boid in boidNormals)
        {
            if (boid == this) continue;
            float ratio = 1f - Mathf.Clamp01((boid.transform.position - transform.position)
            .magnitude / radius);

            dir -= ratio * (Vector2)(boid.transform.position - transform.position);
        }
        return dir.normalized;
    }
    public override Vector2 Aligment(List<ABoidMovement> boidNormals)
    {
        Vector2 dir = Vector2.zero;

        foreach (var boid in boidNormals)
        {
            if (boid == this) continue;
            dir += (Vector2)boid.Velocity;
        }

        if (boidNormals.Count != 0) dir /= boidNormals.Count;
        else dir = Velocity;

        return dir.normalized;
    }
    public override Vector2 Cohesion(List<ABoidMovement>  boidNormals)
    {
        Vector2 dir = Vector2.zero;
        Vector2 center = Vector2.zero;

        foreach (var boid in boidNormals)
        {
            if (boid == this) continue;
            center += (Vector2)boid.transform.position;
        }

        if (boidNormals.Count != 0) center /= boidNormals.Count;
        else center = transform.position;

        dir = center - (Vector2)transform.position;
        return dir.normalized;
    }
    public override Vector2 Center()
    {
        Vector2 direction = Velocity;

        switch (boundary.typeBoundary)
        {
            case EBoundary.Circle:
                float warningRadius = boundary.diameter * 0.4f;
                if (Vector3.Distance(boundary.center, transform.position) > warningRadius)
                {
                    Vector3 normal = (transform.position - boundary.center).normalized;
                    Vector3 tangent = new Vector3(-normal.y, normal.x, 0);

                    direction = GetRandomRotatedDirection(tangent);
                    
                }
                break;

            case EBoundary.Square:
                if (transform.position.x > boundary.center.x + boundary.inradius)
                {
                    Vector3 tangent = new Vector3(0, 1, 0);
                    direction = GetRandomRotatedDirection(tangent);
                }
                else if (transform.position.x < boundary.center.x - boundary.inradius)
                {
                    Vector3 tangent = new Vector3(0, -1, 0);
                    direction = GetRandomRotatedDirection(tangent);
                }
                else if (transform.position.y > boundary.center.y + boundary.inradius)
                {
                    Vector3 tangent = new Vector3(-1, 0, 0);
                    direction = GetRandomRotatedDirection(tangent);
                }
                else if (transform.position.y < boundary.center.y - boundary.inradius)
                {
                    Vector3 tangent = new Vector3(1, 0, 0);
                    direction = GetRandomRotatedDirection(tangent);
                }


                break;

            case EBoundary.Rectangle:
                if (transform.position.x > boundary.center.x + boundary.diameterXY.x)
                {
                    Vector3 tangent = new Vector3(0, 1, 0);
                    direction = GetRandomRotatedDirection(tangent);
                }
                else if (transform.position.x < boundary.center.x - boundary.diameterXY.x)
                {
                    Vector3 tangent = new Vector3(0, -1, 0);
                    direction = GetRandomRotatedDirection(tangent);
                }
                else if (transform.position.y > boundary.center.y + boundary.diameterXY.y)
                {
                    Vector3 tangent = new Vector3(-1, 0, 0);
                    direction = GetRandomRotatedDirection(tangent);
                }
                else if (transform.position.y < boundary.center.y - boundary.diameterXY.y)
                {
                    Vector3 tangent = new Vector3(1, 0, 0);
                    direction = GetRandomRotatedDirection(tangent);
                }
                break;
        }
        return direction;


    }
    public override Vector3 GetRandomRotatedDirection(Vector3 tangent)
    {
        float angleDeg = Random.Range(30f, 150f);
        Vector3 rotated = Quaternion.AngleAxis(angleDeg, Vector3.forward) * tangent;
        transform.position = transform.position + (Vector3) rotated.normalized * 5 * Time.deltaTime;
        return rotated.normalized;
    }
    // public  void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, radius);
    //     var boidinRange = BoisinRange();
    //     foreach (var boid in boidinRange)
    //     {
    //         Gizmos.color = Color.yellow;
    //         Debug.DrawLine(transform.position , boid.transform.position );
    //     }    
    // }

}
