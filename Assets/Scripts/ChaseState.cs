using UnityEditor.VisionOS;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private StatePatternEnemy enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Chase();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {

    }

    public void ToPatrolState()
    {

    }

    void Look()
    {
        // We need to get a vector from enemy eye to player
        // This is the direction we give to raycast
        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eye.position;

        // Debug ray to visual the eye sight.
        Debug.DrawRay(enemy.eye.position, enemyToTarget, Color.yellow);
        // Raycast
        RaycastHit hit; // hit variable gets all the information where the ray hits. We can call hit.something to get info
        if (Physics.Raycast(enemy.eye.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            // We go here only if the ray hits Player
            // If the ray hits player the enemy sees it and goes instantly to Chase State. And enemy knows what to follow. 
            enemy.chaseTarget = hit.transform;  // Enemy makes sure the chasetarget is Player. 


        }
        else
        {
            // If Player goes around corner, enemy does not see player.
            ToAlertState();
        }

    }


    void Chase()
    {
        
        enemy.indicator.material.color = Color.red;
        enemy.navMeshAgent.stoppingDistance = 1;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false; 



    }


}
