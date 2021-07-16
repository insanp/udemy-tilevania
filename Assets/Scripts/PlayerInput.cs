using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Input wrapper for mobile and standalone, and also keep inputs from Update()
// in sync with FixedUpdate()

// ensure this script runs before all scripts to prevent lag
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public float horizontal;
    public bool jumpPressed;
    public bool jumpHeld;

    bool readyToClear;

    // Start is called before the first frame update
    void Start()
    {
        readyToClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        ClearInput();
        ProcessInputs();
        ProcessTouchInputs();
    }

    private void FixedUpdate()
    {
        readyToClear = true;
    }

    private void ProcessTouchInputs()
    {
        //throw new NotImplementedException();
    }

    private void ProcessInputs()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        jumpHeld = jumpHeld || Input.GetButton("Jump");
    }

    private void ClearInput()
    {
        // not ready to clear input, exit
        if (!readyToClear) return;

        // reset all inputs
        horizontal = 0f;
        jumpPressed = false;
        jumpHeld = false;

        readyToClear = false;
    }
}
