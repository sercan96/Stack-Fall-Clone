using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private ObstacleController _obstacleController;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _obstacleController = transform.parent.GetComponent<ObstacleController>();
    }

    public void Shatter() // Parçalanma animasyonu kodu
    {
        _rigidbody.isKinematic = false;
        _collider.enabled = false;

        Vector3 forcePoint = transform.parent.position;
        float parentX = transform.parent.position.x;
        float posX = _meshRenderer.bounds.center.x; // parçalanacak objenin merkezi x noktasını aldık.

        Vector3 subDir = (parentX - posX < 0) ? Vector3.right : Vector3.left; 
        // Parent objemizin orta noktasının parçanın orta noktasına olan uzaklığını hesaplıyoruz
        // ve buna görehangi tarafa doğru hareket vereceğimizi belirliyoruz.
        Vector3 dir = (Vector3.up * 1.5f + subDir).normalized;
        //çıkan vectoru 1.5 ile çarptık;

        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);
        
        _rigidbody.AddForceAtPosition(dir*force,forcePoint,ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.left*torque);
        _rigidbody.velocity = Vector3.down;

    }
}
