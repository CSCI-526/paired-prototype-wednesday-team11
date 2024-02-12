using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;

    float startTime;
    public float lifeTime = 2;
    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;
        if (startTime + lifeTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != gameObject.tag)
        {
            Destroy(gameObject);
        }
    }
}
