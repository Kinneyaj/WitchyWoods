using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : BaseEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        stateMachine.ChangeState(pursueState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
