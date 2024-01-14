using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    [SerializeField] List<Transform> spawnPositions;
    [SerializeField] Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        eventManager.OnDeath += ResetPostion;
        eventManager.OnScore += ResetPostion;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPostion() 
    {
        body.velocity = Vector3.zero;
        int rand = Random.Range(0, spawnPositions.Count);
        transform.position = spawnPositions[rand].position; 
        transform.rotation = spawnPositions[rand].rotation;
    }

    public void ChangeMaterial(PhysicMaterial physicMaterial, Material material)
    {
        Collider collider = GetComponent<Collider>();
        collider.material = physicMaterial;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
    }
}
