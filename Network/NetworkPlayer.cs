using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    private string networkID;

    private string NetworkID { get { return networkID; } }

    public NetworkPlayer(string networkID)
    {
        this.networkID = networkID;
    }
}
