using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Setup")]
    public GameObject bulletPrefab;
    public Transform bulletCollection;
    public Transform player;
    public Transform firePoint;

    [Header("Field Of View")]
    [Range(10f, 100f)] public float sightRadius = 5f;
    private bool inSight = false;
    public LayerMask playerLayer;

    [Header("Attack")]
    [SerializeField] private float recoilTime = 1.5f;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private bool isAlive = true;

    void Start()
    {
        firePoint = transform.Find("Robot_Soldier_Rifle").Find("FirePoint");
    }

    void Update()
    {
		if (!isAlive)
			return;

		inSight = Physics.CheckSphere(transform.position, sightRadius, playerLayer);

		if (inSight)
		{
			transform.LookAt(player);
			Shoot();
		}
	}

	void Shoot()
	{
		if (!canShoot)
			return;

		Vector3 direction = (firePoint.position - player.position).normalized;
		Quaternion lookDirection = Quaternion.LookRotation(direction);

		Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(-90f, 0, lookDirection.eulerAngles.y), bulletCollection);
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
}
