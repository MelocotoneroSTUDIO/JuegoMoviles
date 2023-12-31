using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    [SerializeField] Transform spawnPosition;
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
        transform.position = spawnPosition.position; 
        transform.rotation = spawnPosition.rotation;
    }
}
