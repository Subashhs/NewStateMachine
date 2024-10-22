using UnityEngine;

public class PatrolState : IEnemyState
{
    // We decalre a variable called enemy. It's type is StatePattern enemy. It is a class. 
    private StatePatternEnemy enemy;

    int nextWaypoint; // Index of the waypoint in the array. 

    // When we create patrolstate object in StatePattern enemy, function below is invoked automatically.
    // This is the constructor. This function gets the whole StatePatternEnemy class as paratemer and all the features.
    // Basically we pass all enemy features to this script (object)
    // The parameter (variable name) is statePatterEnemy and we assign it to local variable "enemy".
    // This means that in the future we get acceessa to enemy's properties by writing enemy.SOMETHING
    // for example enemy.searchDuration so we get the value of that. 
    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
        
    }

    public void UpdateState()
    {
        Look();
        Patrol();
    }

    public void OnTriggerEnter(Collider other)
    {
        // We check what is triggering us. If it is player -> Alert state.
        if (other.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToAlertState()
    {
        // We change currentstate to alert state. Runnig the UdpateState function changes the AlertState script. 
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;

    }
    public void ToPatrolState()
    {
        // We cannot use this because we are already on PatrolState. We leave this empty. 
    }


    void Look()
    {
        // Debug ray to visual the eye sight.
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.green);
        // Raycast
        RaycastHit hit; // hit variable gets all the information where the ray hits. We can call hit.something to get info
        if(Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player")){
            // We go here only if the ray hits Player
            // If the ray hits player the enemy sees it and goes instantly to Chase State. And enemy knows what to follow. 
            enemy.chaseTarget = hit.transform;  // Chasetarget is the player
            ToChaseState(); // We change our state to chase state. 

        }

    }


    void Patrol()
    {
        enemy.indicator.material.color = Color.green;
        enemy.navMeshAgent.destination = enemy.waypoints[nextWaypoint].position;
        enemy.navMeshAgent.isStopped = false; 

        // When we get to the current waypoint, switch to the next one. When you get to the last waypoint, go to the first and continue
        // We want to check are we at end location. 
        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            // Enemy definitely is at goal position. 
            nextWaypoint = (nextWaypoint +1) % enemy.waypoints.Length; // This loops the waypoints!

        }

        

    }


}
