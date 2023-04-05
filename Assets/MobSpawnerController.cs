using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnerController : MonoBehaviour
{

    public static MobSpawnerController Instance { get; private set; }

    // Start is called before the first frame update
    [SerializeField] private GameObject _mobPrefab;
    [SerializeField] private float _spawnRate; //in seconds
    [SerializeField] private Sprite[] _mobTypes;
    private Queue<Sprite> _availableMobTypes;

    private int _mobCount = 0;
    private int _maxMobCount = 4; //with current logic it cannot be more than 4 due to _mobTypes dependency. Consider programatically changing the sprite color
    private float _elapsedTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _availableMobTypes = new Queue<Sprite>(_mobTypes);
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _spawnRate)
        {
            SpawnMob();
        }
    }


    public void SpawnMob()
    {
        if (_mobCount < _maxMobCount)
        {
            _elapsedTime = 0;
            _mobCount++;
            GameObject mob = Instantiate(_mobPrefab, transform.position, Quaternion.identity);
            mob.GetComponent<SpriteRenderer>().sprite = _availableMobTypes.Dequeue();
        }
    }

    public void KillMob(GameObject mob)
    {
        _availableMobTypes.Enqueue(mob.GetComponent<SpriteRenderer>().sprite);
        _mobCount--;
        Destroy(mob);
    }

    public void KillAllMobs()
    {
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("mob");
        Array.ForEach(mobs, m => KillMob(m));

    }
}
