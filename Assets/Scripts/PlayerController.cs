using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int movementSpeed = 10;
    float leftBound = -33;
    float rightBound = 2.5f;
    float topBound = 8.5f;
    float lowBound = -7;
    public bool hasPowerUp = false;
    public GameObject powerUpIndicator;
    public ParticleSystem explosionParticle;
    public AudioSource audioSource;
    public AudioClip explosionAudio;
    public AudioClip powerUpAudio;


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * movementSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * movementSpeed);


        if(transform.position.x < leftBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > rightBound)
        {
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        }

        if(transform.position.z < lowBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lowBound);
        }
        else if(transform.position.z > topBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Speed") || other.gameObject.CompareTag("Shield"))
        {
            Instantiate(powerUpIndicator, transform.position, powerUpIndicator.transform.rotation);
            StartCoroutine(PowerUpCountDownRoutine());

            if(other.gameObject.CompareTag("Speed"))
            {
                movementSpeed += 5;
                audioSource.PlayOneShot(powerUpAudio);
            }
            else if(other.gameObject.CompareTag("Shield"))
            {
                hasPowerUp = true;
                audioSource.PlayOneShot(powerUpAudio);
            }
        }

        if (other.gameObject.CompareTag("Obstacle") && !hasPowerUp)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();

            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            audioSource.PlayOneShot(explosionAudio);
        }
        else if (other.gameObject.CompareTag("Obstacle") && hasPowerUp)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            audioSource.PlayOneShot(explosionAudio);
        }
        
        Destroy(other.gameObject);
    }

    IEnumerator PowerUpCountDownRoutine()
    {

        yield return new WaitForSeconds(7);
        Debug.Log("Ya pasaron los 7 segundos");
        hasPowerUp = false;
        GameObject pUI = GameObject.FindGameObjectWithTag("PowerUpInd");
        Destroy(pUI);
        movementSpeed = 10;
    }
}
