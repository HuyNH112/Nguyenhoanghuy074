using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeIncreaseScore : MonoBehaviour
{
    public LogicScript logic;
    private bool scoreIncreased = false;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        scoreIncreased = false;
    }

    void Update()  {}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && !scoreIncreased)
        {
            logic.UpdateScore();
            scoreIncreased = true;
        }
    }
}