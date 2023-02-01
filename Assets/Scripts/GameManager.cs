using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    /*
     *  space -> cafe, mcDonald, supermarket, ... -> 
     * 
     * cafe -> iced coffee, cafe latte, iced tea ...
     * supermarket -> carrot, snack, ... -> List?
     * 
     * 
     */

    /* Tutorial -> 1
     * Easy -> 2
     * Normal -> 3
     * Hard -> 4
     */

    enum PlayMode
    {
        Tutorial = 1, Easy = 2,
        Normal = 3, Hard = 4
    };

    PlayMode Mode = PlayMode.Easy;


    private void Awake()
    {
        Mission = new Dictionary<Place, List<string>>();
    }

    void Start()
    {
        SpawnPlayer();

        SetMission();
    }

#region Mission Setting

    Dictionary<Place, List<string>> Mission;
    enum Place
    {
        Cafe, McDonald, SuperMaket, Bank
    };

    List<string> CafeMenu = new List<string> { "Iced Coffee", "Cafe Latte", "Iced Tea" };
    List<string> MacMenu = new List<string> { "Big Mac", "Coke", "French Fries" };
    List<string> SuperMarket = new List<string> { "Carrot", "Apple", "Orange Juice" };


    [SerializeField] GameObject MissionPanel;
    [SerializeField] GameObject MissionContentPrefab;

    void SetMission()
    {
        for (int MissionCount = 0; MissionCount < (int)Mode; MissionCount++)
        {
            Place MissionPlace = (Place)Random.Range(0, 3);

            GetMyMission(MissionPlace);
        }

        foreach (var MissionPlace in Mission.Keys)
        {
            List<string> MissionList = Mission[MissionPlace];

            string MissionContent = "";

            for (int i = 0; i < MissionList.Count; i++)
            {
                MissionContent += MissionList[i];

                if (i != MissionList.Count - 1)
                    MissionContent += ", ";
                else
                    MissionContent += (" in " + MissionPlace.ToString());
            }


            Instantiate(MissionContentPrefab, MissionPanel.transform.position, Quaternion.identity, MissionPanel.transform);
            /*
             *  A, B in Place1
             *  A in Place2
             */
        }
    }

    void GetMyMission(Place PlaceName)
    {
        string MissionContent;

        switch (PlaceName)
        {
            case Place.Cafe:
                MissionContent = CafeMenu[Random.Range(0, CafeMenu.Count - 1)];
                break;
            case Place.McDonald:
                MissionContent = MacMenu[Random.Range(0, MacMenu.Count - 1)];
                break;
            default:
                MissionContent = "Mission";
                break;
        }

        if (Mission.ContainsKey(PlaceName))
        {
            Mission[PlaceName].Add(MissionContent);
        }
        else
        {
            Mission.Add(PlaceName, new List<string> { MissionContent });
        }
    }
    
#endregion

#region PlayerSpawn

    [SerializeField] List<Transform> SpawnPoses;
    [SerializeField] GameObject Player;
    void SpawnPlayer()
    {
        Player.transform.position = SpawnPoses[Random.Range(0, SpawnPoses.Count)].position;
    }

#endregion
}
