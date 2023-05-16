using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Sprite blueFilledSprite;
    public Sprite blueOutlineSprite;

    public Sprite orangeFilledSprite;
    public Sprite orangeOutlineSprite;

    public Image blueImagePlace;
    public Image orangeImagePlace;

    public PlayerPortalManager ppm;

    public void UpdateOrange(bool doesExist)
    {
        if (doesExist)
        {
            orangeImagePlace.sprite = orangeFilledSprite;
        }
        else
        {
            orangeImagePlace.sprite = orangeOutlineSprite;
        }

        print("wywo³a³em funkcjê updateOrange");
    }

    public void UpdateBlue(bool doesExist)
    {
        if (doesExist)
        {
            blueImagePlace.sprite = blueFilledSprite;
        }
        else
        {
            blueImagePlace.sprite = blueOutlineSprite;
        }

        print("wywo³a³em funkcjê updateBlue");
    }
}
