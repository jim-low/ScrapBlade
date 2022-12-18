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
        firePoint = transform.Find("FirePoint");
    }

    void Update()
    {
		transform.LookAt(player);

        Vector3 direction = (firePoint.position - player.position).normalized;
        Quaternion lookDirection = Quaternion.LookRotation(direction);

		if (Input.GetMouseButtonDown(1))
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(-90f, 0, lookDirection.eulerAngles.y), bulletCollection);
        }
    }
}
