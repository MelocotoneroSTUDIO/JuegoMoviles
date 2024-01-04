using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    [SerializeField] int Score = 0;

    [SerializeField] List<GameObject> Levels;
    [SerializeField] List<GameObject> Obstacles;
    [SerializeField] List<PhysicMaterial> physicsMaterials;
    [SerializeField] List<Material> materials;


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
        Score = 0;
        ball.ChangeMaterial(physicsMaterials[0], materials[0]);
        platform.ChangeMaterial(physicsMaterials[0], materials[0]);
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

            //Parent to platform
        }

        if (Score % 5 == 0) 
        {
            randomBallMat = Random.Range(0, materials.Count);    
            //Change ball material every 5 scores
        }
        ball.ChangeMaterial(physicsMaterials[randomBallMat], materials[randomBallMat]);
        if (Score % 10 == 0) 
        {
            randomPlatMat = Random.Range(0, materials.Count);    
            //Change platform material every 10 scores
        }
        platform.ChangeMaterial(physicsMaterials[randomPlatMat], materials[randomPlatMat]);
    }

    public void UpdateControls() 
    {
        platform.UpdateControls();
    }
}
