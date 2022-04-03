using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField] BoxCollider SpawnArea;
    // Start is called before the first frame update

    public Vector3 GetRandomSpawnArea()
    {
        Vector3 spawnExtend = SpawnArea.bounds.size;
        Vector3 spawnAreaCorner = SpawnArea.bounds.center - SpawnArea.bounds.extents;
        Vector3 spawnPosition = spawnAreaCorner + spawnExtend.normalized * Random.Range(0, spawnExtend.magnitude);
        return spawnPosition;
    }
}
