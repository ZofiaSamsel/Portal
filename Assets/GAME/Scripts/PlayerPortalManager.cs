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
	// Jaki portal mam postawiæ? (otwarty/zamkniêty)
	Transform portalToPlacePrefab;

	public void PlacePortal(PortalColor pc, RaycastHit hit)
	{
		// Sprawdzamy czy w tym miejscu da siê postawiæ portal
		if (!CanPlace(hit)) return;


		// Je¿eli portal, który chcemy otworzyæ to BLUE
		if (pc == PortalColor.BLUE)
		{
			// Jeœli istnieje ju¿ jakikolwiek ORAGNE...
			if (OrangeCurrent != null)
			{
				// To bêdziemy stawiaæ BLUE *OPEN*
				portalToPlacePrefab = portalBlue_open;

				// A jednoczeœnie zmieniamy ju¿ istniej¹cy ORANGE na *OPEN*
				Transform tempTransform = OrangeCurrent;
				Destroy(OrangeCurrent.gameObject);
				OrangeCurrent = Instantiate(portalOrange_open, tempTransform.position, tempTransform.rotation);
			}
			else
			{
				// Jeœli zaœ ORANGE _nie_ istnieje to stawiamy BLUE *CLOSED*
				portalToPlacePrefab = portalBlue_closed;
			}

			// Jeœli wczeœniej istnia³ BLUE to go zamykamy
			if (BlueCurrent != null)
			{
				Destroy(BlueCurrent.gameObject);
			}
		}
		else // Stawiamy orange
		{
			if (BlueCurrent != null)
			{
				// To bêdziemy stawiaæ ORANGE *OPEN*
				portalToPlacePrefab = portalOrange_open;

				// A jednoczeœnie zmieniamy ju¿ istniej¹cy BLUE na *OPEN*
				Transform tempTransform = BlueCurrent;
				Destroy(BlueCurrent.gameObject);
				BlueCurrent = Instantiate(portalBlue_open, tempTransform.position, tempTransform.rotation);
			}
			else
			{
				portalToPlacePrefab = portalOrange_closed;
			}

			// Jeœli wczeœniej istnia³ ORANGE to go zamykamy
			if (OrangeCurrent != null)
			{
				Destroy(OrangeCurrent.gameObject);
			}
		}

	}
	public void Portal(PortalColor pc, RaycastHit hit) { 
		
		// Stawiamy odpowiedni (wg. powy¿szych IF-ów) portal
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

	// Funkcja sprawdzaj¹ca, czy na powierzchni, w któr¹ trafiliœmy
	// mo¿na postawiæ portal. Steruje tym TAG danego obiektu sceny
	private bool CanPlace(RaycastHit hit)
	{
		if (hit.transform.CompareTag(PortalNoTag))
		{
			return false;
		}
		return true;
	}
}
