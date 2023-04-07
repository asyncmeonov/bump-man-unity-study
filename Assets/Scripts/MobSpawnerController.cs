using System.Collections.Generic;
using UnityEngine;

public class MobSpawnerController : MonoBehaviour
{

    public static MobSpawnerController Instance { get; private set; }
    public int MobCount { get => _mobCount; set => _mobCount = value; }

    [SerializeField] private GameObject _mobPrefab;
    [SerializeField] private MobAssetDefinition[] _mobDefinitions;
    [SerializeField] private float _spawnRate; //in seconds

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
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _spawnRate && MobCount >= _maxMobCount) _elapsedTime = 0; //reset if max
        else if (_elapsedTime > _spawnRate && MobCount < _maxMobCount) SpawnMob();   //spawn otherwise. there might be a simpler if
    }


    public void SpawnMob()
    {
        _elapsedTime = 0;
        MobCount++;
        _mobPrefab.GetComponent<MobController>().mobAD = _mobDefinitions[Random.Range(0, _mobDefinitions.Length)];
        GameObject mob = Instantiate(_mobPrefab, transform.position, Quaternion.identity);

    }

    public void KillMob(GameObject mob)
    {
        MobCount--;
        _maxMobCount++;
        Destroy(mob);
    }

    public void KillAllMobs()
    {
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("mob");
        System.Array.ForEach(mobs, m => KillMob(m));

    }
}
