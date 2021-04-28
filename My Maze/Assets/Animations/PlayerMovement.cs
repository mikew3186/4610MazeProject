using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody _body;
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate(){
        var targetVelocity = Vector3.zero;

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
        targetVelocity *= speed;

        _body.velocity = targetVelocity;
    }
}
