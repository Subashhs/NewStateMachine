using UnityEngine;

public interface IEnemyState
{

    // Put here list of method for the contract 

    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();






}
