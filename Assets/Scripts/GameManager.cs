using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI mainTitle;
    public TextMeshProUGUI subTitle;
    public TextMeshProUGUI restartTitle;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI restartScore;
    public Button restartButton;
    public Button buttonCar1;
    public Button buttonCar2;
    public Button buttonCar3;
    public GameObject plane1;
    public GameObject plane2;
    public GameObject plane3;


    public Camera mainCamera;
    Vector3 cameraPosition;
    Vector3 startCameraPosition = new Vector3(-15, 3, -8);

    public GameObject[] obstacles;
    float spawnDelay = 0.5f;
    float spawnInterval = 0.5f;

    public GameObject[] powerUps;
    float powerUpDelay = 5;
    float powerUpInterval = 5;
    public float rightBoundary;
    public float leftBoundary;

    public float score = 0.0f;
    bool pause = true;

    public AudioSource audioSource;
    public AudioClip startGame;

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            score += Time.deltaTime;
            scoreLabel.text = "Score: " + score.ToString("F2");
        }
    }

    public void StartGame(bool restart)
    {
        audioSource.PlayOneShot(startGame);
        pause = false;
        score = 0.0f;
        cameraPosition = new Vector3(-15, 20, 0);
        // Deactivate all UI 
        mainTitle.gameObject.SetActive(false);
        subTitle.gameObject.SetActive(false);
        buttonCar1.gameObject.SetActive(false);
        buttonCar2.gameObject.SetActive(false);
        buttonCar3.gameObject.SetActive(false);

        if(!restart)
        {
            mainCamera.transform.Rotate(Vector3.right * 68);
            mainCamera.transform.position = cameraPosition;
        }
        else
        {
            restartButton.gameObject.SetActive(false);
            restartTitle.gameObject.SetActive(false);
            restartScore.gameObject.SetActive(false);

            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach(GameObject gameObject in obstacles)
            {
                Destroy(gameObject);
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().enabled = true;
            player.transform.position = new Vector3(-15, 0, -7);
            player.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        // Spawn obstacles
        InvokeRepeating("SpawnRandomEnemy", spawnDelay, spawnInterval);

        // Spawn powerups
        InvokeRepeating("SpawnPowerUp", powerUpDelay, powerUpInterval);

        // Start terrain movement
        plane1.GetComponent<PlaneMovement>().enabled = true;
        plane2.GetComponent<PlaneMovement>().enabled = true;
        plane3.GetComponent<PlaneMovement>().enabled = true;

        scoreLabel.gameObject.SetActive(true);

    }

    void SpawnRandomEnemy()
    {
        int index = Random.Range(0, obstacles.Length);
        Vector3 obstaclePos = new Vector3(Random.Range(rightBoundary, leftBoundary), 0, 15);

        Instantiate(obstacles[index], obstaclePos, transform.rotation);
    }

    void SpawnPowerUp()
    {
        int index = Random.Range(0, powerUps.Length);
        Vector3 powerUpPos = new Vector3(Random.Range(rightBoundary, leftBoundary), 0, 15);

        Instantiate(powerUps[index], powerUpPos, powerUps[index].transform.rotation);
    }

    public void GameOver()
    {
        Debug.Log("Has chocado, F.");
        pause = true;
        restartTitle.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        restartScore.gameObject.SetActive(true);
        restartScore.text = "Score: " + score.ToString("F2");
        CancelInvoke();


        // Stop obstacles from moving
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject gameObject in obstacles)
        {
            gameObject.GetComponent<Obstacle>().enabled = false;
        }

        // Stop terrain movement
        plane1.GetComponent<PlaneMovement>().enabled = false;
        plane2.GetComponent<PlaneMovement>().enabled = false;
        plane3.GetComponent<PlaneMovement>().enabled = false;

        // Stop PlayerController script
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().enabled = false;
    }
}
