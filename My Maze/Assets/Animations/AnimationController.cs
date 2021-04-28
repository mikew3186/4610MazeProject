using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    public Rigidbody body;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var flatVelocity = body.velocity;
        flatVelocity.y = 0;

        var flatSpeed = flatVelocity.magnitude;

        var isRunning = flatSpeed > 0.001f;

        _animator.SetBool("isRunning", isRunning);

        if(isRunning){
            var direction = Quaternion.LookRotation(flatVelocity);
            body.MoveRotation(direction);
        }

    }
}
