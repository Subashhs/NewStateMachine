using UnityEngine;

public class AlertState : IEnemyState
{
    private StatePatternEnemy enemy;
    float searchTimer; 

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Search();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        // We cannot use because we are already on AlertState

    }

    public void ToChaseState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.patrolState;
    }

    void Look()
    {
        // Debug ray to visual the eye sight.
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.yellow);
        // Raycast
        RaycastHit hit; // hit variable gets all the information where the ray hits. We can call hit.something to get info
        if (Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            // We go here only if the ray hits Player
            // If the ray hits player the enemy sees it and goes instantly to Chase State. And enemy knows what to follow. 
            enemy.chaseTarget = hit.transform;  // Chasetarget is the player
            ToChaseState(); // We change our state to chase state. 

        }

    }


    void Search()
    {
        enemy.indicator.material.color = Color.yellow;

        enemy.navMeshAgent.isStopped = true; // Stop the enemy

        // Rotate the enemy
        enemy.transform.Rotate(0, enemy.searchTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;
        if(searchTimer > enemy.searchDuration)
        {
            // Enemy has searched enough. It goes back to Patrol State
            ToPatrolState();
        }






    }


}
