using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;  

public class PlayerDeadState : PlayerBaseState
{
    private MonoBehaviour coroutineRunner;  
    private TMP_Text deathText;  

    public PlayerDeadState(PlayerStateMachine stateMachine, TMP_Text deathText) : base(stateMachine)
    {
        this.coroutineRunner = stateMachine;  
        this.deathText = deathText;  
    }

    public override void Enter()
    {
        PlayDeathSound();
        stateMachine.Ragdoll.ToggleRagdoll(true);
        deathText.gameObject.SetActive(true);  
        coroutineRunner.StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(3f);  
        SceneManager.LoadScene("MainMenu");
    }

    public override void Tick(float deltaTime) { }

    public override void Exit()
    {
        deathText.gameObject.SetActive(false);  
    }
}



