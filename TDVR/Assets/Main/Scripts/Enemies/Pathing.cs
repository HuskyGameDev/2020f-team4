using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{

    public Transform goal;
    public Transform enemy;
    public GameObject[] waypoints;
    int currentWP = 0;
    public float rotationSpeed = 50f;
    private float speed = 5f;
    public float accuracyWP = 1f;
    public float scaleFactor = 1f;

    private EnemyStat stat;
    private GameObject wpStoreObject;
    //private WPStore wpStore;
    private WPMPStore wpStore;

    void Awake()
    {
        stat = GetComponent<EnemyStat>();
        wpStoreObject = GameObject.FindGameObjectWithTag("Waypoints");
        //wpStore = wpStoreObject.GetComponent<WPStore>();
        wpStore = wpStoreObject.GetComponent<WPMPStore>();
        //waypoints = wpStore.waypoints;

        //multipathing - get radnom path and assign waypoiny array accordingly
        int pathNum = wpStore.pathNum;
        int randy = Random.Range(1, pathNum+1);
        //Debug.Log(randy);
        waypoints = wpStore.list[randy - 1].WPArrays;
        //end of multipathing

        goal = waypoints[waypoints.Length - 1].transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        //speed = stat.speed * scaleFactor;
        //accuracyWP *= scaleFactor;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = goal.position - this.transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, enemy.up);

        if (waypoints.Length > 0)
        {
            if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
            {
                currentWP++;
                if (currentWP >= waypoints.Length)
                {
                    currentWP = 0;
                    WaveSpawner.EnemiesAlive--;
                    Destroy(gameObject);
                }
            }

            //rotate to waypoint
            direction = waypoints[currentWP].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }

    }
}
