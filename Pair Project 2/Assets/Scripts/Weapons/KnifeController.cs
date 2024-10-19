using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

   
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(prefab);
        spawnedKnife.transform.position = transform.position;   //Assign position to be the same as obj that is parent to Player
        spawnedKnife.GetComponent<KnifeBehavior>().DirectionChecker(pm.lastMovedVector);   //Sets direction
    }
}
