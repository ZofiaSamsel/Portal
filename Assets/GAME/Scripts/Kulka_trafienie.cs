using UnityEngine;

public class Kulka_trafienie : MonoBehaviour
{
	[SerializeField] PortalColor PortalColor;
	[SerializeField] PlayerPortalManager ppm;

	RaycastHit hit;
    [SerializeField] float shootingDelay = 5;

    private void OnEnable()
	{
		ppm = GameObject.FindObjectOfType<PlayerPortalManager>();
	}


	private void OnCollisionEnter(Collision collision)
	{
		Vector3 kierunek = collision.contacts[0].point - transform.position;

		if (Physics.Raycast(transform.position, kierunek, out hit))
		{

			ppm.Portal(PortalColor, hit);
		}

		print("Trafi�em w " + collision.transform.name);
		Destroy(gameObject);
	}
}
