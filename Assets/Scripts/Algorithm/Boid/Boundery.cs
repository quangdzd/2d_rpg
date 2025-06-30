using System;
using UnityEngine;


public class Boundary 
{
    public Vector3 center;
    public EBoundary typeBoundary;
    public int diameter;
    public int inradius;
    public Vector2 diameterXY;




    public Boundary(Vector3 center, EBoundary typeBoundary, int diameter = 0, int inradius = 0, Vector2 diameterXY = default)
    {
        this.center = center;
        this.typeBoundary = typeBoundary;
        this.diameter = diameter;
        this.inradius = inradius;
        this.diameterXY = diameterXY;

    }


}
