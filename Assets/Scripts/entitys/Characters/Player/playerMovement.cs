using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 cameraOffset; //mainly to keep z-distance

    private Entity Player;

    //movement input
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = cameraTransform.position;
        Player = GetComponent<Entity>();
    }
    // called once per physics frame
    void FixedUpdate()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movement != Vector2.zero) Player.rb.AddForce(movement.normalized * Player.entityStats["ms"]);
        cameraTransform.position = Player.rb.transform.position + cameraOffset;
    }
}
