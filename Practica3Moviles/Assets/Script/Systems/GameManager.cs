using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    [SerializeField] int Score = 0;
    [SerializeField] int Lives = 3;
    [SerializeField] TextMeshProUGUI LivesText;

    [SerializeField] List<GameObject> Levels;
    [SerializeField] List<GameObject> Obstacles;
    [SerializeField] List<PhysicMaterial> physicsMaterials;
    [SerializeField] List<Material> platformMaterials;
    [SerializeField] List<Material> ballMaterials;


    [SerializeField] BallBehaviour ball;
    [SerializeField] PlatformRotation platform;
    [SerializeField] GameObject platformInstance;

    int randomBallMat = 0;
    int randomPlatMat = 0;

    private void Start()
    {
        eventManager.OnScore += UpdateLevel;
        eventManager.OnDeath += ResetScore;
    }

    public void ResetScore() 
    {
        Lives--;
        if (Lives < 0) 
        {
            SceneManager.LoadScene("GameOverScreen");
        }
        LivesText.text = $"Lives: {Lives}";

        Score = 0;
        ball.ChangeMaterial(physicsMaterials[0], ballMaterials[0]);
        platform.ChangeMaterial(physicsMaterials[0], platformMaterials[0]);
    }

    public void UpdateLevel() 
    {
        Score++;

        //Change Level
        Destroy(platformInstance);
        int rand = Random.Range(0, Levels.Count);
        platformInstance = Instantiate(Levels[rand],Vector3.zero, Quaternion.identity);
        //Maybe random rotate instance on Y axis 90*rand[0,4]
        platform = platformInstance.GetComponent<PlatformRotation>();
        PlatformObstaclePositions positions = platformInstance.GetComponent<PlatformObstaclePositions>();
        //Add obstacles

        //determines number of obstacles to place
        int numberOfObstacles = Random.Range(0, positions.obstaclePostions.Count);

        //Chooses random position and obstacle avoiding same positon
        List<int> obstaclesPlaced = new();
        for (int i = 0; i < numberOfObstacles; i++) 
        {
            //Choose rand obstacle
            int randObstacle = Random.Range(0, Obstacles.Count);
            int randPos = 0;
            do
            {
                randPos = Random.Range(0, positions.obstaclePostions.Count);
            } 
            while (obstaclesPlaced.Contains(randPos));

            obstaclesPlaced.Add(randPos);

            GameObject instance = Instantiate(Obstacles[randObstacle], positions.obstaclePostions[randPos].position,Quaternion.identity);
            instance.transform.parent = positions.obstaclePostions[randPos];
            instance.transform.Rotate(new Vector3(0,Random.Range(0,360),0));
            //Parent to platform
        }

        if (Score % 5 == 0) 
        {
            randomBallMat = Random.Range(0, ballMaterials.Count);    
            //Change ball material every 5 scores
        }
        ball.ChangeMaterial(physicsMaterials[randomBallMat], ballMaterials[randomBallMat]);
        if (Score % 10 == 0) 
        {
            randomPlatMat = Random.Range(0, platformMaterials.Count);    
            //Change platform material every 10 scores
        }
        platform.ChangeMaterial(physicsMaterials[randomPlatMat], platformMaterials[randomPlatMat]);
    }

    public void UpdateControls() 
    {
        platform.UpdateControls();
    }
}
