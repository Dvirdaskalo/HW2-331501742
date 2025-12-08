using UnityEngine;

public class spawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemys;
    private GameObject[] points;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        points = GameObject.FindGameObjectsWithTag("Points");
        spawn();
    }

    public void spawn()
    {
        var enemy = Instantiate(Enemys[Random.Range(0, Enemys.Length)], points[Random.Range(0, Enemys.Length)].transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
