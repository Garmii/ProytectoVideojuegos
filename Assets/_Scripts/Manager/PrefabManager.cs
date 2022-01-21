using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    public GameObject spawnedPlayer;

    // Start is called before the first frame update
    void Start()
    {
       spawnedPlayer = Instantiate(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
