using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHandler : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private Rigidbody _rb;
    private SphereCollider _sc;
    private GravityHandler _gravityHandler;
    public float DestroyTimer = 20f;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _sc = gameObject.GetComponent<SphereCollider>();
        _gravityHandler = gameObject.GetComponent<GravityHandler>();
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(DestroyTimer);
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
            _rb.detectCollisions = false;
            ParticleSystem.MainModule _main = _particleSystem.main;
            _main.startColor = Color.white;
            _main.startLifetime = 0.8f;
            _main.startSize= 1.2f;
            _sc.isTrigger = true;
            _gravityHandler.enabled = false;
        }
    }
}
