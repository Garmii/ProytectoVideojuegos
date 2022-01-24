using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject slime;
    [SerializeField] private GameObject cannon;

    
    public GameObject spawnedPlayer;
    public GameObject spawnedSlime;
    public GameObject spawnedCannon;

    private List<GameObject> cannons = new List<GameObject>();
    private List<GameObject> slimes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //spawnedPlayer = Instantiate(player);

        /*for (int i =0;i<5;i++)
        {
            cannons.Add(Instantiate(cannon));
            slimes.Add(Instantiate(slime));
        }
        
        slimes[0].transform.position = new Vector2(5,3);
        slimes[3].transform.position = new Vector2(5,5);
        cannons[0].transform.position = new Vector2(23.5f, 2.5f);
     */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
