using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider))]
public class Enemy : PolygonCollider {

    private PolygonCollider enemCol;

	// Use this for initialization
	void OnValidate () {

        enemCol = GetComponent<PolygonCollider>();

        transform.tag = "Enemy";


		if(!enemCol.firstTriangleMade) enemCol.makeFirstTriangle(area, transform.position);
	}
	

}
