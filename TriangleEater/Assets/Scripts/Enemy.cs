using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider))]
public class Enemy : PolygonCollider {

    private PolygonCollider enemCol;

	// Use this for initialization
	void Start () {

        enemCol = GetComponent<PolygonCollider>();

        transform.tag = "Enemy";

        enemCol.makeTriangle(area);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
