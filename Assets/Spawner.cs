using UnityEngine;
using SPWN;

public class Spawner : MonoBehaviour
{
    public GameObject[] typeAlpha;
    public GameObject[] typeBeta;
    public GameObject[] typeGamma;
    public GameObject[] typeDelta;

    [SpawnButton("SPAWN ALPHA")]
    public void SpawnAlpha()
    {
        // spawn a random alpha type. Index between 0 - amount of Alpha types
        Instantiate(typeAlpha[Random.Range(0, typeAlpha.Length)], transform.position, Quaternion.identity);
    }
    [SpawnButton("SPAWN BETA")]
    public void SpawnBeta()
    {
        // spawn a random beta type. Index between 0 - amount of Beta types
        Instantiate(typeBeta[Random.Range(0, typeBeta.Length)], transform.position, Quaternion.identity);
    }
    [SpawnButton("SPAWN GAMMA")]
    public void SpawnGamma()
    {
        // spawn a random gamma type. Index between 0 - amount of Gamma types
        Instantiate(typeGamma[Random.Range(0, typeGamma.Length)], transform.position, Quaternion.identity);
    }
    [SpawnButton("SPAWN DELTA")]
    public void SpawnDelta()
    {
        // spawn a random delta type. Index between 0 - amount of Delta types
        Instantiate(typeDelta[Random.Range(0, typeDelta.Length)], transform.position, Quaternion.identity);
    }

[SpawnButton("TEST METHOD")]
    public void TestMethod()
    {
        Dbug.Log("Test Method Called");
    }
}
