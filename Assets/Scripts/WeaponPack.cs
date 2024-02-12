using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPack : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player.weaponAvailable++;
            Destroy(gameObject);
        }
    }
}
