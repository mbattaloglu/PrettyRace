using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleArea : MonoBehaviour
{
    public GameObject[] collectiblePrebafs;
    GameManager gameManager;
    bool collectibleSpawned;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        collectibleSpawned = false;

    }
    void Update()
    {
        if (!collectibleSpawned & gameManager.player.position.x - transform.position.x <= 5)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int index = Random.Range(0, collectiblePrebafs.Length);
                Instantiate(collectiblePrebafs[index], transform.GetChild(i).position, Quaternion.identity);
            }
            collectibleSpawned = true;
        }
    }
}
