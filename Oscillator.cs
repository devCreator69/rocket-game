using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 5f;
    
    [SerializeField] [Range(0,1)] float movementFactor; //Created range slider 0-1 in Unity to adjust before it was defined in the code below
    void Start()
    {
        startingPosition = transform.position;
    }
    void Update()
    {

        if (period <= Mathf.Epsilon) { return; } // protect against NaN error, when period = 0 an error saying not a number(NaN) is diplayed
        // Mathf.Epsilon is the smallest float to compare rather than 0, bc the odds of equaling exactly zero with all decmil places is low
        //return = if period is 0 do not do following code

        // Time.time = how much time has elapsed 
        // period = how long we define the cycle to last (2f)
        // ex. if 10 seconds have passed and our period is 2, 5 cycles will have elapsed
       
        float cycles = Time.time / period; //continually growing over time / how long it takes to reach one lap around circle (tau or 2pi)
        
        const float tau = Mathf.PI * 2; // tau is 2pi, constant value of 6.823
        
        float sinWave = Mathf.Sin(cycles * tau); // will give a value ranging from -1 to 1 on the sin wave after one cycle is completed

        movementFactor = (sinWave + 1f) /2f; //recaluculated to go from 0 to 2 so instaed of -1 to 1 then divided by two to put values going from 0 to 1

        Vector3 offset = movementVector * movementFactor; 
        transform.position = startingPosition + offset;
    }
}
