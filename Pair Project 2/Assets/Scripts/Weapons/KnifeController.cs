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
        GameObject spawnedKnife = Instantiate(prefab, transform.position, Quaternion.identity); // Instantiate knife
        spawnedKnife.transform.position = transform.position;  // Assign knife position to player's position
        spawnedKnife.GetComponent<KnifeBehavior>().DirectionChecker(pm.lastMovedVector);  // Set knife direction

        // Detach the knife from the player so it can move independently
        spawnedKnife.transform.parent = null;
    }
}
