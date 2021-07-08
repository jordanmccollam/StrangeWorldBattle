using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OrbSpawner : MonoBehaviour
{
    public GameObject[] orbs;
    public float minX, minY, maxX, maxY;
    public int maxOrbs;
    public float timeBetweenSpawns;
    public int currentlySpawned = 0;

    private void Start() {
        Spawn();
    }

    private void Spawn() {
        currentlySpawned++;
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject randomOrb = orbs[Random.Range(0, orbs.Length)];
        PhotonNetwork.Instantiate(randomOrb.name, randomPosition, Quaternion.identity);
        StartCoroutine(HandleTime());
    }

    private IEnumerator HandleTime() {
        yield return new WaitForSeconds(timeBetweenSpawns);
        if (currentlySpawned < maxOrbs) {
            Spawn(); 
        }
    }

}
