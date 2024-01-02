using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        eventManager.OnScore += UpdateLevel;
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
        int numberOfObstacles = 0;
        if (numberOfObstacles > positions.obstaclePostions.Count) 
        {
            numberOfObstacles= positions.obstaclePostions.Count;
        }

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
            while (!obstaclesPlaced.Contains(randPos));

            obstaclesPlaced.Add(randPos);

            Instantiate(Obstacles[randObstacle], positions.obstaclePostions[randPos]);
            //Parent to platform
        }

        if (Score % 5 == 0) 
        {
            int random = Random.Range(0, materials.Count);
            ball.ChangeMaterial(physicsMaterials[random], materials[random]);
            //Change ball material
        }
        if (Score % 10 == 0) 
        {
            int random = Random.Range(0, materials.Count);
            platform.ChangeMaterial(physicsMaterials[random], materials[random]);
            //Change platform material
        }
    }
}
