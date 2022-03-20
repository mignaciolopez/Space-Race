using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject spaceshipPrefab;

    // Start is called before the first frame update
    void Start()
    {
        OnGameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStart()
    {
        SpawnPlayers();
        
    }

    private void SpawnPlayers()
    {
        //Player 1 (Left Side) as in prefab
        GameObject player1 = Instantiate(spaceshipPrefab);
        player1.name += " Player1";
        player1.GetComponent<PlayerController>().axisName = "Vertical";

        //Player 2 (Right Side) negate position.x to place in the oposite side of screen
        GameObject player2 = Instantiate(spaceshipPrefab);
        player2.name += " Player2";
        player2.GetComponent<PlayerController>().axisName = "Vertical2";

        Vector2 pos = player2.transform.position;
        pos.x *= -1;
        player2.transform.position = pos;
    }
}
