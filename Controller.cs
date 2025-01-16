using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public FixedJoystick Joystick;

    void FixedUpdate()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = new Vector3(
            Joystick.Horizontal * 25,
            rigidbody.velocity.y,
            Joystick.Vertical * 25
        );
    }
}
