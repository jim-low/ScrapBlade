using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletCollection;
    public Transform player;
    public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
		transform.LookAt(player);

        float rotation = transform.localRotation.eulerAngles.y;

        if (rotation > 180f)
        {
            rotation = rotation - 360f;
        }
        Debug.Log(rotation);

		if (Input.GetMouseButtonDown(1))
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(90f, 0, rotation), bulletCollection);
        }
    }
}
