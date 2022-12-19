using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Setup")]
    public GameObject bulletPrefab;
    public Transform bulletCollection;
    public Transform target;
    public Transform firePoint;

    [Header("Field Of View")]
    [Range(10f, 100f)] public float sightRadius = 5f;
    private bool inSight = false;
    public LayerMask playerLayer;

    [Header("Attack")]
    [SerializeField] private float recoilTime = 1.5f;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private bool isAlive = true;

	void Update()
	{
		if (!isAlive)
			return;

		inSight = Physics.CheckSphere(transform.position, sightRadius, playerLayer);

		if (inSight)
		{
			Vector3 playerPostition = new Vector3(target.position.x, transform.position.y, target.position.z);
			transform.LookAt(playerPostition);
			firePoint.LookAt(target);
			Shoot();
		}
	}

	void Shoot()
	{
		if (!canShoot)
			return;

		Vector3 direction = (firePoint.position - target.position).normalized;
		Quaternion lookDirection = Quaternion.LookRotation(direction);

		Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(lookDirection.eulerAngles.x - 90f, 0, lookDirection.eulerAngles.y), bulletCollection);
		canShoot = false;
		StartCoroutine(Recoil());
	}

	IEnumerator Recoil()
	{
		yield return new WaitForSeconds(recoilTime);
		canShoot = true;
	}

	public void SetLiveStatus(bool status)
	{
		isAlive = status;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = inSight ? Color.red : Color.gray;
		Gizmos.DrawWireSphere(transform.position, sightRadius);
	}

	public void SetCanShoot(bool newCanShoot)
	{
		canShoot = newCanShoot;
	}
}
