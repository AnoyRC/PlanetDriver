using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject Meteor;
    public float TimeInterval = 1f;
    public float distance = 20f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnMeteor());
    }

    // Update is called once per frame
    IEnumerator spawnMeteor()
    {
        Vector3 pos = Random.onUnitSphere * distance;
        Instantiate(Meteor, pos, Quaternion.identity);
        yield return new WaitForSeconds(TimeInterval);
        StartCoroutine(spawnMeteor());
    }
}
