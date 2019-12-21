using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetResolutionForTablet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
  	/// Can be added to a game manager in a level and played on the larger screen to get a game resolution for a tablet for the google play console tablet photographs if needed
  	/// </summary>
    void Update()
    {
Debug.Log("Screen Width : " + Screen.width);
Debug.Log("Screen Width : " + Screen.height);
    }

}
