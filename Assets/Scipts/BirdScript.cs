using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;  
    public bool birdIsAlive = true;
  
    public SpriteRenderer birdSpriteRenderer; 
    public Sprite[] availableBirdSprites; 
    private const string SelectedCharacterIndexKey = "SelectedCharacterIndex";                                                                                 

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        if (birdSpriteRenderer == null)   
        {
            birdSpriteRenderer = GetComponent<SpriteRenderer>();
        }
        int selectedIndex = PlayerPrefs.GetInt(SelectedCharacterIndexKey, 0);

        if (selectedIndex >= 0 && selectedIndex < availableBirdSprites.Length)
        {
            birdSpriteRenderer.sprite = availableBirdSprites[selectedIndex];
        }
        else
        {
            Debug.LogWarning("Selected bird index is out of bounds. Using default bird (index 0).");
            if (availableBirdSprites.Length > 0)
            {
                birdSpriteRenderer.sprite = availableBirdSprites[0]; 
            }
        }
        myRigidbody.bodyType = RigidbodyType2D.Kinematic;
        birdIsAlive = true;
    }

    void Update()
    {
        if (logic != null && !logic.gameHasStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                logic.StartTheGame();
                myRigidbody.bodyType = RigidbodyType2D.Dynamic;   
                myRigidbody.linearVelocity = Vector2.up * flapStrength;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive && Time.timeScale > 0)
            {
                myRigidbody.linearVelocity = Vector2.up * flapStrength;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdIsAlive && logic != null && logic.gameHasStarted)
        {
            birdIsAlive = false;
            myRigidbody.linearVelocity = new Vector2(0, myRigidbody.linearVelocity.y);
            logic.gameOver();
        }
    }
}
