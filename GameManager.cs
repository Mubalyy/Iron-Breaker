using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private InputReader inputReader;
    private string previousScene;

    private void OnEnable()
    {
        inputReader.PauseEvent += HandlePause;
    }

    private void OnDisable()
    {
        inputReader.PauseEvent -= HandlePause;
    }

    private void HandlePause()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Pause");
    }

    public void ReturnToPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void TriggerWinCondition()
    {
        PlayerStateMachine player = FindObjectOfType<PlayerStateMachine>();
        if (player != null)
        {
            player.SwitchState(new PlayerWinState(player, player.winText));
        }
    }
}


