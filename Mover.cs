using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust =100f;
    [SerializeField] AudioClip mainEngine;
    
    [SerializeField] ParticleSystem mainEngineParticles;
    
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRoatation();
    }

    void ProcessThrust()
    {
         if(Input.GetKey(KeyCode.Space))
         {
            startThrusting();
         }
        else
        {
            stopThrusting();
        }       
    }

    void startThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if(!audioSource.isPlaying)
        {
            //PlayOneShot allows a specific sound to be played 
            audioSource.PlayOneShot(mainEngine);
        } 
        if(!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    void stopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();

    }
    
    void ProcessRoatation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
           ApplyRotation(-rotationThrust);
        }
        //Method ApplyRoataion created to clean code bc used multiple times; easier to call
        void ApplyRotation(float rotationThisFrame)
        {
           //implemented to stop the glitch which is created when rocket hits obstacle(there is a conflict with the physics and controls)
            rb.freezeRotation = true; // freezing roataion using rb(Rigidbody)so we can manually rotate
            transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
            rb.freezeRotation = false; // unfrezing rotation so the physics system can take over
        }
        
    }
}
