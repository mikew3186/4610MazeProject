using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody _body;
    public float speed = 10;
    public float acceleration = 10;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var speed = 5.0f;
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * speed);
    }

    private void FixedUpdate(){
        var targetVelocity = Vector3.zero;
        _body.angularVelocity = Vector3.zero;

        
        if(Input.GetKey(KeyCode.W)){ 
            targetVelocity.z  += 1;
        }

        if(Input.GetKey(KeyCode.S)){
            targetVelocity.z  -= 1;
        }

        if(Input.GetKey(KeyCode.D)){
            targetVelocity.x  += 1;
        }

        if(Input.GetKey(KeyCode.A)){
            targetVelocity.x  -= 1;
        }

        targetVelocity =  targetVelocity.normalized;
        targetVelocity = transform.rotation * targetVelocity;
        targetVelocity *= speed;

        _body.velocity = Vector3.MoveTowards(_body.velocity, targetVelocity, acceleration * Time.deltaTime);

    }
}
