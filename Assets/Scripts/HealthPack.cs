using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    Player player;
    GameMode gameMode;

    // Start is called before the first frame update
    void Start()
    {   
        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player.hp += 5;
            gameMode.OnPlayerHpChange(player.hp);
            Destroy(gameObject);
        }
    }
}
