using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    [SerializeField] EventManager eventManager;

    private void OnTriggerEnter(Collider other)
    {
        eventManager.OnScore.Invoke();
    }
}
