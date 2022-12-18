using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletCollection;
    public Transform player;
    public Transform firePoint;

    void Start()
    {
        firePoint = transform.Find("Robot_Soldier_Rifle").Find("FirePoint");
    }

    void Update()
    {
		transform.LookAt(player);


		if (Input.GetMouseButtonDown(1))
        {
        }
    }

    void Shoot()
    {
        // calculate 
		Vector3 direction = (firePoint.position - player.position).normalized;
		Quaternion lookDirection = Quaternion.LookRotation(direction);

		Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(-90f, 0, lookDirection.eulerAngles.y), bulletCollection);
	}
}
