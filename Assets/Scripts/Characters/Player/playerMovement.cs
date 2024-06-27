using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 cameraOffset; //mainly to keep z-distance

    private entity Player;
    private Dictionary<string, float> entityStats; //read only, this is only a reference (except setting defaults)

    //movement input
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = cameraTransform.position;
        Player = GetComponent<entity>();
        entityStats = GetComponent<entity>().entityStats;
    }
    // called once per physics frame
    void FixedUpdate()
    {
        if (entityStats.TryGetValue("ms", out float value))
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (movement != Vector2.zero) Player.rb.AddForce(movement.normalized * value);
        }
        cameraTransform.position = Player.rb.transform.position + cameraOffset;
    }
}
