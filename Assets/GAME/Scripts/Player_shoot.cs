using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shoot : MonoBehaviour
{
	[Header("Shooting")]
	[SerializeField] Transform headTransform;
	[SerializeField] float shotDelay;

	[SerializeField] Transform gunEnd;
	[SerializeField] Transform orangeBall;
	[SerializeField] Transform blueBall;


	float nextShot;
	private RaycastHit hit;

	PlayerPortalManager ppm;
	Animator myAnimator;

	private void OnEnable()
	{
		ppm = GetComponent<PlayerPortalManager>();
		myAnimator = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetAxis("Fire1") != 0 && Time.time >= nextShot)
		{
			Shoot(PortalColor.BLUE);
			nextShot = Time.time + shotDelay;
		}
		if (Input.GetAxis("Fire2") != 0 && Time.time >= nextShot)
		{
			Shoot(PortalColor.ORANGE);
			nextShot = Time.time + shotDelay;
		}
	}

	private void Shoot(PortalColor portalToPlaceColor)
	{
		//Debug.DrawRay(headTransform.position, headTransform.forward, Color.green, 2f);
		if (Physics.Raycast(headTransform.position, headTransform.forward, out hit))
		{
			//print(hit.transform.name);
			ShootBall(portalToPlaceColor, hit);
			myAnimator.SetTrigger("Shoot");
			ppm.PlacePortal(portalToPlaceColor, hit);
		}
	}

	private void ShootBall(PortalColor portalToPlaceColor, RaycastHit hit)
	{
		// Który prefab kulki chcemy?
		Transform ballPrefab = portalToPlaceColor == PortalColor.BLUE ? blueBall : orangeBall;

		// Obliczamy kierunek od broni do celu
		Vector3 kierunek = hit.point - gunEnd.position;

		Debug.DrawRay(gunEnd.position, kierunek, Color.red, 2f);


		// Tworzymy kulkê
		var ball = Instantiate(ballPrefab, gunEnd.position, Quaternion.identity);

		// Nadajemy kulce si³ê
		ball.GetComponent<Rigidbody>().AddForce(kierunek.normalized * 10, ForceMode.VelocityChange);
	}
}
