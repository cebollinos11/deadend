using UnityEngine;
using System.Collections;

public class EyeEnemy : Enemy {

    [SerializeField]
    GameObject projectile;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        //base.Update();

        //aim to player
        float angle = Mathf.Atan2(Player.transform.position.y - transform.position.y, Player.transform.position.x - transform.position.x);

        transform.rotation = Quaternion.Euler(0f, 0f, angle*180/Mathf.PI);

        if(Input.GetKeyDown(KeyCode.X))
        {
            ShootLaser();
        }
    }

    public void ShootLaser()
    {
        Instantiate(projectile, transform.position, transform.rotation);

    }
}
