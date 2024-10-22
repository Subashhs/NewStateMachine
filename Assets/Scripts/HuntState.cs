using UnityEngine;

public class HuntState : IEnemyState
{
    private StatePatternEnemy enemy;
    private float shootTimer; // Timer to control shooting interval

    public HuntState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Hunt();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        // Handle collisions while hunting
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void ToHuntState()
    {
        // Already in HuntState
    }

    void Look()
    {
        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eye.position;

        Debug.DrawRay(enemy.eye.position, enemyToTarget, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(enemy.eye.position, enemyToTarget, out hit, enemy.sightRange))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                // If we lose sight of the player, return to AlertState
                ToAlertState();
            }
        }
        else
        {
            // If no hit, return to AlertState
            ToAlertState();
        }
    }

    void Hunt()
    {
        enemy.indicator.material.color = Color.blue;

        // Stop enemy while hunting
        enemy.navMeshAgent.isStopped = true;

        // Timer to control shooting intervals
        shootTimer += Time.deltaTime;
        if (shootTimer > 2f) // Example: fire every 2 seconds
        {
            FireBullet();
            shootTimer = 0;
        }
    }

    void FireBullet()
    {
        // Instantiate bullet and shoot it toward the player
        GameObject bullet = Object.Instantiate(enemy.bulletPrefab, enemy.shootPoint.position, enemy.shootPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Calculate direction to the player
        Vector3 direction = (enemy.chaseTarget.position - enemy.shootPoint.position).normalized;
        bulletRb.AddForce(direction * enemy.bulletSpeed, ForceMode.Impulse); // Apply force to throw bullet
    }
}
