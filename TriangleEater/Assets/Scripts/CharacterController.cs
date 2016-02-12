using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour {

    [HideInInspector]
    public Vector2 playerInput;

    private Rigidbody2D bod;

    void Start()
    {
        bod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        movePolygon();
    }

    // move polygon
    public void movePolygon()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bod.AddForce(playerInput);
    }

}
