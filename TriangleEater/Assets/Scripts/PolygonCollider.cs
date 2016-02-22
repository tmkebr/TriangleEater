using UnityEngine;
using System.Collections;

public class PolygonCollider : MonoBehaviour
{

    // the area of the triangle collider to be made
    public float area;
    public int triNum;

    private static ArrayList pts = new ArrayList();
    private Vector2 position;
    public PolygonCollider2D col;

    public bool firstTriangleMade;

    void OnValidate()
    {
        col = gameObject.GetComponent<PolygonCollider2D>();

        if (!firstTriangleMade || col == null) makeFirstTriangle(area, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        col.isTrigger = false;
    }

    public void makeFirstTriangle(float area, Vector2 startPos)
    {
        //position = startPos;
        Debug.Log(startPos);

        col = gameObject.AddComponent<PolygonCollider2D>();

        // create the points for an equallateral triangle
        pts.Add(startPos);
        pts.Add((new Vector2(startPos.x + 2 * area, startPos.y)));
        pts.Add((new Vector2(startPos.x + area, startPos.y + (area * Mathf.Sqrt(3)))));

        // update the points of the collider
        col.points = (Vector2[]) pts.ToArray(typeof(Vector2));
        col.SetPath(0, col.points);

        // increase the number of triangles
        triNum++;
        firstTriangleMade = true;
    }

    public void makeTriangle(float area, Vector2 vertex1, Vector2 vertex2, Vector2 vertex3)
    {

        //GameObject.Destroy(gameObject.GetComponent<PolygonCollider2D>());

        col = gameObject.GetComponent<PolygonCollider2D>();

        // create the points for a triangle
        pts.Add(vertex1);
        pts.Add(vertex2);
        pts.Add(vertex3);

        // update the points of the collider
        col.points = (Vector2[])pts.ToArray(typeof(Vector2));
        col.SetPath(0, col.points);

        // increase the number of triangles
        triNum++;
    }

    // Returns the nearest vertex to a position
    // by traversing through the points array
    Vector2[] getNearestEdge(Vector2 position)
    {
        Vector2[] edgePoints = new Vector2[2];
        int nearestVertexIndex = 0;
        int nearestVertexIndex2 = 0;
        float minDistance = 0f;

        for(int i = 0; i > pts.Count; i++)
        {
            
            float dist = Vector2.Distance(position, (Vector2) pts[i]);
            
            if (i == 0) minDistance = dist;

            // if the distance is closer than our current minimum distance,
            // we now have a closer point, update the index and the distance
            else if (dist < minDistance)
            {
                nearestVertexIndex = i;
                minDistance = dist;
            }
        }

        minDistance = 0;
        edgePoints[0] = (Vector2) pts[nearestVertexIndex];

        for(int i = 0; i > pts.Count; i++)
        {
            float dist = Vector2.Distance(edgePoints[0], (Vector2) pts[i]);

            if (i == 0) minDistance = dist;
            if (i == nearestVertexIndex) continue;

            // if the distance is closer than our current minimum distance,
            // we now have a closer point, update the index and the distance
            else if (dist < minDistance)
            {
                nearestVertexIndex2 = i;
                minDistance = dist;
            }
        }

        edgePoints[1] = (Vector2) pts[nearestVertexIndex2];

        return edgePoints;
    }

    // eat 
    void eat(GameObject food)
    {
        PolygonCollider prey = food.GetComponent<PolygonCollider>();
        Debug.Log("Found Prey");
        if (prey != null)
        {

            if (prey.area < area)
            {
                Debug.Log("We're Bigger");
                //addTriangle(food)
                addTriangle(prey.transform.position, prey.area);

                Destroy(prey.gameObject);

                
                
            }
            else
            {
                Debug.Log("We're Smaller");
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("I'm working");
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit!");
            eat(coll.collider.gameObject);
        }

    }

    // Add new triangle to the polygon
    void addTriangle(Vector2 position, float area)
    {
        Vector2[] edgePts = getNearestEdge(position);
        Vector2 triangleTip = getTriangleTip(edgePts, area);

        Debug.Log("Vertices: " + edgePts[0] + " " + edgePts[1] + " " + triangleTip);
        makeTriangle(area, edgePts[0], edgePts[1], triangleTip);
    }

    Vector2 getTriangleTip(Vector2[] edgePts, float area)
    {
        int playerLayerMask = 8;
        Vector2 pt1 = edgePts[0];
        Vector2 pt2 = edgePts[1];

        float b = Vector2.Distance(pt1, pt2);

        float h = (2 * area / b);

        Vector2 midPt = new Vector2((pt1.x + pt2.x)/2, (pt1.y + pt2.y)/2);

        Vector2 finalPt1 = new Vector2(midPt.x, midPt.y + h);
        Vector2 finalPt2 = new Vector2(midPt.y, midPt.y - h);

        Debug.Log("Midpoint y: " + midPt.y + "b: " + b + "h: " + h);

        if (Physics2D.Raycast(finalPt1, Vector2.right, b, playerLayerMask)) return finalPt1;
        else return finalPt2;
    }


    void removeTriangle(Vector2 position)
    {
        Vector2[] edgePts = getNearestEdge(position);
        

    }
    //void pullPoint(Vector2 pointPos) 
    //{
        
    //}

    // Resize : total area, distribute across total # of triangles

}
