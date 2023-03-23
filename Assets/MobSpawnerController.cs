using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private float spawnRate; //in seconds
    [SerializeField] private Sprite[] mobTypes;

    private Queue<Sprite> availableMobTypes;
    private int mobCount = 0;

    private float elapsedTime = 0f;
    void Start()
    {
        availableMobTypes = new Queue<Sprite>(mobTypes);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > spawnRate)
        {
            SpawnMob();
        }
    }


    private void SpawnMob()
    {
        if(mobCount < 4)
        {
            elapsedTime = 0f;
            mobCount++;
            GameObject mob = Instantiate(mobPrefab, transform.position, Quaternion.identity);
            mob.GetComponent<SpriteRenderer>().sprite = availableMobTypes.Dequeue();
        }
    }

    private void KillMob(GameObject mob)
    {
        availableMobTypes.Enqueue(mob.GetComponent<SpriteRenderer>().sprite);
        mobCount--;
        Destroy(mob);
    }
}
