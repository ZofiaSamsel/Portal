using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortalManager : Singleton<PlayerPortalManager>
{
	[Header("Portal prefabs")]
	[SerializeField] Transform portalBlue_open;
	[SerializeField] Transform portalBlue_closed;
	[Space()]
	[SerializeField] Transform portalOrange_open;
	[SerializeField] Transform portalOrange_closed;

	[Header("Surface Tags")]
	[SerializeField] string PortalNoTag;

	public PlayerUIManager puim;

	private Transform _orangeCurrent;
	public Transform OrangeCurrent
	{
		get
		{
			return _orangeCurrent;
		}
		set
		{

			puim.UpdateOrange(true);
			_orangeCurrent = value;
		}
	}

	private Transform _blueCurrent;
	public Transform BlueCurrent
	{
		get
		{
			return _blueCurrent;
		}
		set
		{
			puim.UpdateBlue(true);
			_blueCurrent = value;
		}
	}
	// Jaki portal mam postawi�? (otwarty/zamkni�ty)
	Transform portalToPlacePrefab;

	public void PlacePortal(PortalColor pc, RaycastHit hit)
	{
		// Sprawdzamy czy w tym miejscu da si� postawi� portal
		if (!CanPlace(hit)) return;


		// Je�eli portal, kt�ry chcemy otworzy� to BLUE
		if (pc == PortalColor.BLUE)
		{
			// Je�li istnieje ju� jakikolwiek ORAGNE...
			if (OrangeCurrent != null)
			{
				// To b�dziemy stawia� BLUE *OPEN*
				portalToPlacePrefab = portalBlue_open;

				// A jednocze�nie zmieniamy ju� istniej�cy ORANGE na *OPEN*
				Transform tempTransform = OrangeCurrent;
				Destroy(OrangeCurrent.gameObject);
				OrangeCurrent = Instantiate(portalOrange_open, tempTransform.position, tempTransform.rotation);
			}
			else
			{
				// Je�li za� ORANGE _nie_ istnieje to stawiamy BLUE *CLOSED*
				portalToPlacePrefab = portalBlue_closed;
			}

			// Je�li wcze�niej istnia� BLUE to go zamykamy
			if (BlueCurrent != null)
			{
				Destroy(BlueCurrent.gameObject);
			}
		}
		else // Stawiamy orange
		{
			if (BlueCurrent != null)
			{
				// To b�dziemy stawia� ORANGE *OPEN*
				portalToPlacePrefab = portalOrange_open;

				// A jednocze�nie zmieniamy ju� istniej�cy BLUE na *OPEN*
				Transform tempTransform = BlueCurrent;
				Destroy(BlueCurrent.gameObject);
				BlueCurrent = Instantiate(portalBlue_open, tempTransform.position, tempTransform.rotation);
			}
			else
			{
				portalToPlacePrefab = portalOrange_closed;
			}

			// Je�li wcze�niej istnia� ORANGE to go zamykamy
			if (OrangeCurrent != null)
			{
				Destroy(OrangeCurrent.gameObject);
			}
		}

	}
	public void Portal(PortalColor pc, RaycastHit hit) { 
		
		// Stawiamy odpowiedni (wg. powy�szych IF-�w) portal
		var portal = Instantiate(portalToPlacePrefab, hit.point, Quaternion.identity);
		portal.rotation = Quaternion.LookRotation(hit.normal);
		portal.transform.Rotate(90, 0, 0);

		// Ustaw go jako odpowiedni "Current"
		if (pc == PortalColor.BLUE)
		{
			BlueCurrent = portal;
		}
		else
		{
			OrangeCurrent = portal;
		}
	}

	// Funkcja sprawdzaj�ca, czy na powierzchni, w kt�r� trafili�my
	// mo�na postawi� portal. Steruje tym TAG danego obiektu sceny
	private bool CanPlace(RaycastHit hit)
	{
		if (hit.transform.CompareTag(PortalNoTag))
		{
			return false;
		}
		return true;
	}
}
