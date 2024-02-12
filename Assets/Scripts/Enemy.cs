using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject prefabBullet;
    public GameObject prefabBoomEffect;

    public float speed = 2;
    public float fireTime = 0.1f;
    public float maxHp = 1;

    Vector3 input;
    bool fire = false;
    string target = "Player";
    float lastFireTime;

    Transform player;
    float hp;
    bool dead = false;
    MeshRenderer render;
    GameMode gameMode;
    Weapon weapon;
    float initTime;
    void Start()
    {

        render = GetComponent<MeshRenderer>();
        render.material.color = Random.ColorHSV();
        player = GameObject.FindGameObjectWithTag(target).transform;
        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
        weapon = GetComponent<Weapon>();
        initTime = Time.time;

    }

    void Update()
    {
        Move();
        Fire();
    }

    void Move()
    {
        if (UnityEngine.Random.Range(0, 10) > 8 && target == "Player" || target == "Ally")
        {
            if (GameObject.FindGameObjectsWithTag("Ally").Length > 0)
            {
                input = GameObject.FindGameObjectWithTag("Ally").transform.position - transform.position;
                target = "Ally";
            }
            else
            {
                target = "Player";
            }
        }
        GameObject t = GameObject.FindGameObjectWithTag(target);
        if (t == null)
        {
            return;
        }
        input = t.transform.position - transform.position;

        input = player.position - transform.position;
        input = input.normalized;

        transform.position += input * speed * Time.deltaTime;
        if (input.magnitude > 0.1f)
        {
            transform.forward = input;
        }
    }

    void Fire()
    {
        if (lastFireTime + fireTime > Time.time)
        {
            return;
        }
        lastFireTime = Time.time;

        GameObject bullet = Instantiate(prefabBullet, null);
        bullet.transform.position = transform.position + transform.forward * 1.0f;
        bullet.transform.forward = transform.forward;
        // Change the color of the bullet
        MeshRenderer bullet_render = bullet.GetComponent<MeshRenderer>();
        if (bullet_render != null)
        {
            bullet_render.material.color = render.material.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (dead)
        {
            return;
        }
        if (other.CompareTag("PlayerBullet") || other.CompareTag("AllyBullet"))
        {
            // Get the color of the bullet
            MeshRenderer bulletRenderer = other.gameObject.GetComponent<MeshRenderer>();
            UnityEngine.Color bulletColor = bulletRenderer != null ? bulletRenderer.material.color : UnityEngine.Color.white;

            // Convert colors to HSV
            float h1, s1, v1;
            UnityEngine.Color.RGBToHSV(render.material.color, out h1, out s1, out v1);
            float h2, s2, v2;
            UnityEngine.Color.RGBToHSV(bulletColor, out h2, out s2, out v2);
            // Calculate hue difference
            float hueDifference = Mathf.Abs(h1 - h2);
            UnityEngine.Color interpolatedColor = render.material.color;
            // If hue difference is small, destroy the enemy
            if (hueDifference < 0.1f) // You can adjust the threshold as needed
            {
                dead = true;
                Destroy(gameObject);
                gameMode.OnEnemyDied(this.transform);
            }
            else
            {
                // Calculate the interpolated color
                interpolatedColor = UnityEngine.Color.Lerp(render.material.color, bulletColor, 0.5f);

            }
            render.material.color = interpolatedColor;
            GameObject boomEffect = Instantiate(prefabBoomEffect, transform.position, transform.rotation);

            // Change the color of the boom effect
            ParticleSystem particleSystem = boomEffect.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.startColor = interpolatedColor;
            }
        }
        else if (other.CompareTag("Player") || other.CompareTag("Ally") || other.CompareTag("Enemy"))
        {
            Vector3 collisionNormal = other.transform.position - transform.position;
            other.transform.position += collisionNormal.normalized / 10f;
        }
        else if (other.CompareTag("Face"))
        {
            // find the parent of the face
            Transform parent = other.transform.parent;
            Vector3 collisionNormal = parent.transform.position - transform.position;
            parent.transform.position += collisionNormal.normalized / 10f;
            transform.position -= collisionNormal.normalized / 10f;
        }
    }
}
