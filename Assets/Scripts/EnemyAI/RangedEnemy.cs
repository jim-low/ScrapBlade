using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletCollection;

    // Update is called once per frame
    void Update()
    {
		// testing saje
		if (Input.GetMouseButtonDown(1))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(90f, 0, 0), bulletCollection);
        }
    }
}
