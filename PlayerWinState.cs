using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class PlayerWinState : PlayerBaseState
{
    private MonoBehaviour coroutineRunner;  
    private TMP_Text winText;
    private readonly float delayBeforeTransition = 3f;

    public PlayerWinState(PlayerStateMachine stateMachine, TMP_Text winText) : base(stateMachine)
    {
        this.winText = winText;  
    }

    public override void Enter()
    {
        
        
        if (winText != null)
        {
            winText.gameObject.SetActive(true);
        }

        
        stateMachine.StartCoroutine(TransitionToMainMenu());
    }

    private IEnumerator TransitionToMainMenu()
    {
        yield return new WaitForSeconds(delayBeforeTransition);
        SceneManager.LoadScene("MainMenu");
    }


    public override void Tick(float deltaTime) { }

    public override void Exit()
    {
        winText.gameObject.SetActive(false);  
    }
}
