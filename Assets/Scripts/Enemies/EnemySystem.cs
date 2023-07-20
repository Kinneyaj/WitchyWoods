using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour {

    public StateMachine stateMachine;
    public PauseState pauseState;
    public PursueState pursueState;
    public AttackState attackState;


    public GameObject target;
    public int healthPoints;
    public int seekRadius;

    private void Awake() {
        pauseState = new PauseState(gameObject);
        stateMachine = new StateMachine(pauseState);
        pursueState = new PursueState(gameObject);
        attackState = new AttackState(gameObject);
    }

    private void Update() {
        stateMachine.Update();
    }

    public void setNavTarget(GameObject target) {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.transform.position);
    }

}

public class PauseState : BaseState {
    public PauseState(GameObject owner) : base(owner) {
    }

    public override void Enter() {
    }
    public override void Execute(StateMachine s) {
    }
    public override void Exit() {
    }
}

public class PursueState : BaseState {
    float seekRadius;
    public PursueState(GameObject owner) : base(owner) {
    }

    public override void Enter() {
        NavMeshAgent agent = owner.GetComponent<NavMeshAgent>();
        agent.SetDestination(owner.GetComponent<BaseEnemy>().target.transform.position);
    }

    public override void Execute(StateMachine s) {
        float distance = Vector3.Distance(owner.transform.position, owner.GetComponent<BaseEnemy>().target.transform.position);
        if(distance <= seekRadius) {
            s.ChangeState(owner.GetComponent<BaseEnemy>().attackState);
        }
    }

    public override void Exit() {
        
    }
}

public class AttackState : BaseState {
    public AttackState(GameObject owner) : base(owner) {
    }

    public override void Enter() {

    }

    public override void Execute(StateMachine s) {

    }

    public override void Exit() {

    }
}