using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnInterval = 1.0f;
    public int maxSpawn = 20;
    [SerializeField] GameObject prefab;
    BoxCollider2D boxCollider;

    float timeElapsed = 0.0f;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > spawnInterval)
        {
            timeElapsed = 0.0f;

            if (count < maxSpawn)
            {
                Vector2 spawnPos = transform.position;
                spawnPos.y = Random.Range(transform.position.y - boxCollider.size.y / 2.0f, transform.position.y + boxCollider.size.y / 2.0f);

                Instantiate(prefab, spawnPos, prefab.transform.rotation).transform.parent = transform.parent;

                count++;
            }
        }
    }
}
