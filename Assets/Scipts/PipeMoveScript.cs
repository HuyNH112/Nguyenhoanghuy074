using UnityEngine;

public class pipeMoveScript : MovableObjectBase
{
    public float deadZone = -30;
    // Vertical movement properties
    public float verticalMoveAmplitude = 2f; 
    public float verticalMoveFrequency = 1f; 
    private float verticalMoveOffset;
    // Rotation properties
    public float rotationAmplitude = 15f; 
    public float rotationFrequency = 1.5f; 
    private float rotationOffset;

    void Start()
    {
        verticalMoveOffset = Random.Range(0f, 10f);
        rotationOffset = Random.Range(0f, 10f);
    }
    
    void Update()
    {
        
        if (LogicScript.instance != null && LogicScript.instance.gameHasStarted && !LogicScript.instance.isGameOver)
        { 
            MoveStep(Time.deltaTime);

            if (LogicScript.instance.Score >= 10)
            {
                float verticalMovement = Mathf.Sin(Time.time * verticalMoveFrequency + verticalMoveOffset) * verticalMoveAmplitude;
                transform.position = new Vector3(transform.position.x, transform.position.y + verticalMovement * Time.deltaTime, transform.position.z);
            }

            if (LogicScript.instance.Score >= 20)
            {
                
                float currentRotationZ = Mathf.Sin(Time.time * rotationFrequency + rotationOffset) * rotationAmplitude;
                transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject); 
        }
    }
}