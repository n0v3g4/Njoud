using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straightLineEnemy : enemy
{
    private float msDefault = 25;
    //apply enemyspesific things
    public override void applySpesifics()
    {
        if (!Enemy.entityStats.TryGetValue("ms", out float value)) Enemy.entityStats["ms"] = msDefault;
    }

    //the movement ai
    public override void movement()
    {
        Vector2 movement = targetLocation.position - Enemy.rb.transform.position;
        Enemy.rb.AddForce(movement.normalized * Enemy.entityStats["ms"]);
    }
}
