using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        eventManager.OnDeath+=ResetScore;
        eventManager.OnScore+=AddScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScore() 
    {
        Score = 0;
        textMeshProUGUI.text = $"Score: {Score}";
    }

    public void AddScore() 
    {
        Score++;
        textMeshProUGUI.text = $"Score: {Score}";
    }
}
