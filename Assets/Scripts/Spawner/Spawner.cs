using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform _container;
    [SerializeField] private int _repeatCount;
    [SerializeField] private int _bonusSpawnRate;
    [SerializeField] private int _distanceBetweenFullLine;
    [SerializeField] private int _distanceBetweenRandomLine;
    [Header("Block")]
    [SerializeField] private Block _blockTemplate;
    [SerializeField] private int _blockSpawnChance;
    [Header("Wall")]
    [SerializeField] private Wall _wallTemplate;
    [SerializeField] private int _wallSpawnChance;
    [Header("Bonus")]
    [SerializeField] private Bonus _bonusTemplate;
    [SerializeField] private int _bonusSpawnChance;

    private BlockSpawnPoint[] _blockSpawnPoints;
    private WallSpawnPoint[] _wallSpawnPoints;
    private BonusSpawnPoint[] _segmentSpawnPoints;
    private void Start()
    {
        _blockSpawnPoints = GetComponentsInChildren<BlockSpawnPoint>();
        _wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();
        _segmentSpawnPoints = GetComponentsInChildren<BonusSpawnPoint>();

        for (int i = 0; i < _repeatCount; i++)
        {
            for (int z = 0; z < _distanceBetweenFullLine; z++)
            {
            MoveSpawner(_distanceBetweenRandomLine);
            GenerateRandomElements(_wallSpawnPoints, _wallTemplate.gameObject, _wallSpawnChance, _distanceBetweenRandomLine - 1, _distanceBetweenRandomLine / 2);
            if (Random.Range(0,100) <= _bonusSpawnRate)
            {
                    Debug.Log("element spawned");
            GenerateRandomElements(_segmentSpawnPoints, _bonusTemplate.gameObject, _bonusSpawnChance, 0.25f);
            }
            else
            {
            GenerateRandomElements(_blockSpawnPoints, _blockTemplate.gameObject, _blockSpawnChance);
            }
            }

            MoveSpawner(_distanceBetweenRandomLine);
            GenerateRandomElements(_wallSpawnPoints, _wallTemplate.gameObject, _wallSpawnChance, _distanceBetweenFullLine - 1, _distanceBetweenFullLine/2);
            GenerateFullLine(_blockSpawnPoints, _blockTemplate.gameObject);

        }
    }
    private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject generatedElement)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GenerateElement(spawnPoints[i].transform.position, generatedElement);
        }
    }
    private void GenerateRandomElements(SpawnPoint[] spawnPoints, GameObject generatedElement, int spawnChance, float scaleY = 1, int offsetY = 0)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Random.Range(0, 100) <= spawnChance)
            {
                GameObject element = GenerateElement(spawnPoints[i].transform.position, generatedElement,offsetY);
                element.transform.localScale = new Vector3(element.transform.localScale.x, scaleY, element.transform.localScale.z);
            }
        }
    }

    private GameObject GenerateElement (Vector3 spawnPoint, GameObject generatedElement, float offsetY = 0)
    {
        spawnPoint.y -= offsetY;
        return Instantiate(generatedElement, spawnPoint, Quaternion.identity, _container);
    }
    private void MoveSpawner(int distanceY)
    {
        transform.position += Vector3.up * distanceY;
    }
}
