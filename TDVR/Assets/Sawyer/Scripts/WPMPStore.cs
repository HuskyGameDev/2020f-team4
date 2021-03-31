using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WPMPStore : MonoBehaviour
{
    public int pathNum = 0;
    //public GameObject[] waypointArrays;
    public WPList[] list;
    //public List<GameObject[]> WPAList;
}

[Serializable]
public class WPList
{
    //[SerializeField]
    //public List<GameObject[]> WPAList;
    public GameObject[] WPArrays;
}
