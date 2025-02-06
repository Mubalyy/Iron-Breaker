using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStateMachine : StateMachine
{

    [field : SerializeField] public InputReader InputReader { get; private set; }
    [field : SerializeField] public CharacterController Controller { get; private set; }
    [field : SerializeField] public Animator Animator  { get; private set; }
    [field : SerializeField] public Targeter Targeter  { get; private set; }
    [field : SerializeField] public ForceReceiver ForceReceiver  { get; private set; }
    [field : SerializeField] public WeaponDamage Weapon  { get; private set; }
    [field : SerializeField] public Health Health  { get; private set; }
    [field : SerializeField] public Ragdoll Ragdoll  { get; private set; }
    [field : SerializeField] public TMP_Text deathText  { get; private set; }
    [field : SerializeField] public TMP_Text winText  { get; private set; }
    [field: SerializeField] public TMP_Text objectiveText { get; private set; }
    [field : SerializeField] public float  FreeLookMovementSpeed { get; private set; }
    [field : SerializeField] public float  TargetingMovementSpeed { get; private set; }
    [field : SerializeField] public float  RotationDamping { get; private set; }
    [field : SerializeField] public Attack[] Attacks { get; private set; }
    public Transform MainCameraTransform  { get; private set; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
        StartCoroutine(ShowObjectiveMessage());
    }

     private void OnEnable()
    {
       Health.OnTakeDamage += HandleTakeDamage;
       Health.OnDie +=HandleDie;
    }

    private void OnDisable()
    {
      Health.OnTakeDamage -= HandleTakeDamage;
      Health.OnDie -=HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this, deathText));
    }

    private IEnumerator ShowObjectiveMessage()
    {
        
        if (objectiveText != null)
        {
            objectiveText.gameObject.SetActive(true);
            
            
            yield return new WaitForSeconds(2f);
            
            objectiveText.gameObject.SetActive(false);
        }
    }

   
}
