using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip gameStartingClip;
    [SerializeField] AudioClip poweringDownClip;
    [SerializeField] AudioClip scoreClip;
    [SerializeField] AudioClip crashClip;

    [SerializeField] GameObject spaceshipPrefab;
    GameObject player1;
    GameObject player2;

    [SerializeField] float time = 120f;
    float timer = 120f;
    [SerializeField] GameObject timerPrefab;
    GameObject timerObject;

    [HideInInspector] public int score1, score2;

    bool isGameOver = false;

    static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (!m_instance)
            {
                GameObject manager = new GameObject("GameManager");
                manager.AddComponent<GameManager>();
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnGameStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                OnGameOver();
            }
            else
            {
                if (timer < 3.0f && !audioSource.isPlaying)
                {
                    audioSource.clip = poweringDownClip;
                    audioSource.volume = 0.4f;
                    audioSource.Play();
                }

                if (timerObject)
                {
                    Vector3 newScale = timerObject.transform.localScale;
                    newScale.y = (timer * timerPrefab.transform.localScale.y) / time;
                    timerObject.transform.localScale = newScale;
                }
            }
        }
    }

    public IEnumerator OnGameStart()
    {
        audioSource.clip = gameStartingClip;
        audioSource.volume = 0.5f;
        audioSource.Play();
        yield return new WaitForSeconds(gameStartingClip.length);
        isGameOver = false;
        score1 = 0;
        score2 = 0;
        SpawnPlayers();
        timerObject = Instantiate(timerPrefab);
        timer = time;
    }

    private void OnGameOver()
    {
        isGameOver = true;
        Destroy(player1);
        Destroy(player2);
        Destroy(timerObject);
        StartCoroutine(OnGameStart());
    }

    private void SpawnPlayers()
    {
        //Player 1 (Left Side) as in prefab
        player1 = Instantiate(spaceshipPrefab);
        player1.name += " Player1";
        player1.GetComponent<PlayerController>().axisName = "Vertical";
        player1.tag = "Player1";

        //Player 2 (Right Side) negate position.x to place in the oposite side of screen
        player2 = Instantiate(spaceshipPrefab);
        player2.name += " Player2";
        player2.GetComponent<PlayerController>().axisName = "Vertical2";
        player2.tag = "Player2";

        Vector2 pos = player2.transform.position;
        pos.x *= -1;
        player2.transform.position = pos;

    }

    public void UpdateScore(string playerTag)
    {
        audioSource.clip = scoreClip;
        audioSource.volume = 0.4f;
        audioSource.Play();

        if (playerTag == "Player1")
        {
            ResetPositionPlayer("Player1");
            score1++;
        }
        else if (playerTag == "Player2")
        {
            ResetPositionPlayer("Player2");
            score2++;
        }
    }

    public void ResetPositionPlayer(string playerTag)
    {
        if (playerTag == "Player1")
        {
            player1.transform.position = spaceshipPrefab.transform.position;
        }
        else if (playerTag == "Player2")
        {
            player2.transform.position = spaceshipPrefab.transform.position;
            Vector2 pos = player2.transform.position;
            pos.x *= -1;
            player2.transform.position = pos;

        }
    }

    public void PlayCrashClip()
    {
        audioSource.clip = crashClip;
        audioSource.volume = 0.4f;
        audioSource.Play();
    }
}
