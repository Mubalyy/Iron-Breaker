using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class EnemyStateMachine : StateMachine
{
  [field : SerializeField] public Animator Animator  { get; private set; }
  [field : SerializeField] public CharacterController Controller  { get; private set; }
  [field : SerializeField] public ForceReceiver ForceReceiver  { get; private set; }
  [field : SerializeField] public NavMeshAgent Agent  { get; private set; }
  [field : SerializeField] public WeaponDamage Weapon  { get; private set; }
  [field : SerializeField] public Health Health  { get; private set; }
  [field : SerializeField] public Target Target  { get; private set; }
  [field : SerializeField] public Ragdoll Ragdoll  { get; private set; }
  [field : SerializeField] public float MovementSpeed  { get; private set; }
  [field : SerializeField] public float PlayerChasingRange  { get; private set; }
  [field : SerializeField] public float AttackRange  { get; private set; }
  [field : SerializeField] public int AttackDamage  { get; private set; }
  [field : SerializeField] public int AttackKnockback  { get; private set; }
  public static List<EnemyStateMachine> allEnemies = new List<EnemyStateMachine>();

  public Health Player {get; private set;}
  

  private void Start()
    {
        allEnemies.Add(this);
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }

    private void OnEnable()
    {
       Health.OnTakeDamage += HandleTakeDamage;
       Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
      allEnemies.Remove(this);  
      Health.OnTakeDamage -= HandleTakeDamage;
      Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,PlayerChasingRange);
    }

    public void CheckWinCondition()
    {
        // Check if all enemies are dead
        bool allEnemiesDead = true;
        foreach (var enemy in allEnemies)
        {
            if (enemy != null && enemy.Health != null && !enemy.Health.IsDead)
            {
                allEnemiesDead = false;
                break;
            }
        }

        if (allEnemiesDead && GameManager.Instance != null)
        {
            GameManager.Instance.TriggerWinCondition();
        }
    }

}
