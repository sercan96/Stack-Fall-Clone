using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private bool _ishit = false;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _ishit = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _ishit = false;
        }
    }
    void FixedUpdate()
    {
        MouseClick();
       // Debug.Log(_ishit);
    }
    public void MouseClick()
    {
        if (_ishit == (true))
        {
            _rigidbody.velocity = new Vector3(0f, -100f * Time.deltaTime * 7f, 0f);
        }
        
    }
    

    private void OnCollisionStay(Collision collision) // temas ettiği sürece gireceği için kullanırız.
    {
        if (_ishit != (true))
        {
            _rigidbody.velocity = new Vector3(0f, 40f * Time.fixedDeltaTime * 7f, 0f);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_ishit != (true))
        {
            _rigidbody.velocity = new Vector3(0f, 50f * Time.fixedDeltaTime * 5f, 0f);
        }
        else
        {
            if(collision.gameObject.CompareTag("enemy"))
            {
                Destroy(collision.transform.parent.gameObject);
            }
        }
        
    }
    
}
