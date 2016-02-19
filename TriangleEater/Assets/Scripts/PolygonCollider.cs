using UnityEngine;
using System.Collections;

public class PolygonCollider : MonoBehaviour
{

    // the area of the triangle collider to be made
    public float area;
    public int triNum;

    private Vector2[] points = new Vector2[3];
    private Vector2 position;
    public PolygonCollider2D col;

    void Start()
    {

        makeTriangle(area, transform.position);
    }

    void Update()
    {

    }

    public void makeTriangle(float area, Vector2 startPos)
    {
        position = startPos;

        col = gameObject.AddComponent<PolygonCollider2D>();

        // create the points for an equallateral triangle
        points[0] = position;
        points[1] = (new Vector2(position.x + 2 * area, position.y));
        points[2] = (new Vector2(position.x + area, position.y + (area * Mathf.Sqrt(3))));

        // update the points of the collider
        col.points = points;
        col.SetPath(0, points);

        // increase the number of triangles
        triNum++;
    }

    // Returns the nearest vertex to a position
    // by traversing through the points array
    Vector2 getNearestVertex(Vector2 position)
    {
        int nearestVertexIndex = 0;
        float minDistance = 0f;

        for(int i = 0; i > points.Length; i++)
        {
            
            float dist = Vector2.Distance(position, points[i]);
            
            if (i == 0) minDistance = dist;

            // if the distance is closer than our current minimum distance,
            // we now have a closer point, update the index and the distance
            else if (dist < minDistance)
            {
                nearestVertexIndex = i;
                minDistance = dist;
            }
        }

        return points[nearestVertexIndex];
    }

    // eat 
    void eat(GameObject food)
    {
        PolygonCollider prey = food.GetComponent<PolygonCollider>();
        if (prey != null)
        {

            if (prey.area < area)
            {
                Debug.Log("We're Bigger");
                //addTriangle(food)
                Destroy(prey);
                
            }
            else
            {
                Debug.Log("We're Smaller");
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit!");
            eat(coll.collider.gameObject);
        }

    }

    // Add new triangle to the polygon
    void addTriangle(Vector2 position)
    {
        Vector2 startPosition = getNearestVertex(position);

        makeTriangle(area, startPosition);
    }


    void pullPoint(Vector2 pointPos) 
    {
        
    }

    // Resize : total area, distribute across total # of triangles

}
