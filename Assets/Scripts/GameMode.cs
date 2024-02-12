using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    Player player;
    Transform canvas;
    public GameObject prefabEnemy;
    public GameObject prefabAlly;
    int kills = 0;
    int allies = 0;

    public static GameMode Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        canvas = GameObject.Find("Canvas").transform;
    }

    public void OnEnemyDied(Transform transform)
    {
        kills++;
        canvas.Find("Kills").GetComponent<Text>().text = "Kills: " + kills;
        if (UnityEngine.Random.Range(0, 10) > 7)
        {
            allies++;
            canvas.Find("Allies").GetComponent<Text>().text = "Allies: " + allies;
            GameObject ally = Instantiate(prefabAlly, null);
            ally.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            ally.GetComponent<MeshRenderer>().material.color = GameObject.FindGameObjectWithTag("Player").GetComponent<MeshRenderer>().material.color;
        }
    }

    public void OnAllyDied(Transform transform)
    {
        --allies;
        canvas.Find("Allies").GetComponent<Text>().text = "Allies: " + allies;

        GameObject enemy = Instantiate(prefabEnemy, null);
        enemy.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

    }

    public void OnPlayerHpChange(float hp)
    {
        if (hp <= 0)
        {
            canvas.Find("PlayerStatus").GetComponent<Text>().text = "Failed!";
            GameObject.Find("Spawner").GetComponent<Spawner>().enabled = false;
        }
        else
        {
            canvas.Find("PlayerStatus").GetComponent<Text>().text = "HP: " + hp;

        }

    }
    public void SetWeaponText(int weapon)
    {
        Text t = canvas.Find("Weapon").GetComponent<Text>();
        switch (weapon)
        {
            case 0:
                t.text = "Handgun";
                break;
            case 1:
                t.text = "Shotgun";
                break;
            case 2:
                t.text = "Automatic Rifle";
                break;
        }
    }
}
