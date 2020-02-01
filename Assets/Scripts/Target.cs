using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody targetRb;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    
    public ParticleSystem explosionparticle;
    
    public int pointValue;
    
    private GameManager gameManager; 
    
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque()
            , ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            //vernietigt een object als je er op klikt
            Destroy(gameObject);
            //De hoeveelheid punten van een object worden toegevoegd
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionparticle, transform.position, explosionparticle.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            //game over wanneer er een goed item valt, maar niet als er een bom valt
            gameManager.GameOver();
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    //de random kracht waarmee dingen in de lucht schieten
    Vector3 RandomForce()
    { 
        return Vector3.up * Random.Range(minSpeed, maxSpeed); 
    }

    //de random draaing van de objecten
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    //random spawnpositie binnen een bepaald gebied (Xrange)
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

}
