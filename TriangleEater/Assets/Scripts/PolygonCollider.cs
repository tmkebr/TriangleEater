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

        makeTriangle(area);
    }

    void Update()
    {

    }

    public void makeTriangle(float area)
    {
        position = transform.position;

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


    // eat 
    void eat(GameObject food)
    {
        PolygonCollider prey = food.GetComponent<PolygonCollider>();
        if (prey != null)
        {

            if (prey.area < area)
            {
                Debug.Log("We're Bigger");
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

    // Add Vertex

    // Resize : total area, distribute across total # of triangles

}
