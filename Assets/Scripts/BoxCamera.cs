using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxCamera : MonoBehaviour, IsSaveable
{
    [SerializeField] Camera camera;
    [SerializeField] Transform player;
    [SerializeField] float speed = 5.0f;
    [SerializeField] MoveWalls wall1;
    private GameObject walls;

    private Vector3 target;
    private bool moure, cameraOnPlayer;
    public bool completedRoom, finished;
    private float step;
    public MeshRenderer illa;

    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public enemigo[] enemies;
    public enemyshoot[] enemies2;
    private bool canCheck, checkingIfClear;


    private void Start()
    {

        step = speed * Time.deltaTime;
        moure = false;
        cameraOnPlayer = true;
        checkingIfClear = false;
        canCheck = false;
        if (GameObject.Find("MainCamera") != null){
            camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        }
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (GetComponentInParent<MeshRenderer>() != null)
        {
            illa = GetComponentInParent<MeshRenderer>();
        }
        

        if (wall1 != null)
        {
            walls = wall1.gameObject;
            //walls.SetActive(false);
        }

        if (!completedRoom)
        {
            // Spawnejar zombies, entre 1 i 5
            int randomNumber = Random.Range(1, 5);
            if (SceneManager.GetActiveScene().name.Equals("nivell2")) 
            {
                randomNumber *= 2;
            }
            int i = 0;
            enemies = new enemigo[randomNumber];
            while (i < randomNumber)
            {
                if (enemyPrefab1 != null)
                {
                    enemies[i] = SpawnEnemy(enemyPrefab1).GetComponent<enemigo>();
                    enemies[i].illa = this;
                }
                i++;
            }

            // Spawnejar tio que dispara, entre 1 i 5
            randomNumber = Random.Range(1, 5);
            if (SceneManager.GetActiveScene().name.Equals("nivell2"))
            {
                randomNumber *= 2;
            }
            i = 0;
            enemies2 = new enemyshoot[randomNumber];
            while (i < randomNumber)
            {
                if (enemyPrefab1 != null)
                {
                    enemies2[i] = SpawnEnemy(enemyPrefab2).GetComponent<enemyshoot>();
                }
                i++;
            }
        }
    }

    private void Update()
    {
        if (moure)
        {
            if (camera != null)
            {
                camera.transform.position = Vector3.MoveTowards(camera.transform.position, target, step);
            }
        }
        else if (!cameraOnPlayer)
        {
            target = player.position + new Vector3(0, 10, -10);
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, target, step);
            if (camera.transform.position == target)
            {
                cameraOnPlayer = true;
            }
        }

        if (canCheck && !checkingIfClear && !completedRoom)
        {
            StartCoroutine(checkEnemies());
        }
        else if (completedRoom && !finished)
        {
            StartCoroutine(roomFinished());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canCheck = true;
            if (walls != null)
            {
                walls.SetActive(true);
                wall1.ascend = true;
            }
            other.gameObject.GetComponent<PlayerController>().playerInBox = true;
            moure = true;
            target = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z - 15);

            cameraOnPlayer = false;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && completedRoom)
        {
            ClearEnemies();
            restoreCamera();
        }
    }

    IEnumerator roomFinished()
    {
        finished = true;
        wall1.ascend = false;
        yield return new WaitForSeconds(2);
        restoreCamera();
        Destroy(walls);
    }
    IEnumerator checkEnemies()
    {
        checkingIfClear = true;
        int alive = 0;
        foreach(enemigo enemy in enemies)
        {
            if (!enemy.dead)
            {
                alive++;
            }
        }
        foreach (enemyshoot enemy in enemies2)
        {
            if (!enemy.dead)
            {
                alive++;
            }
        }
        yield return new WaitForSeconds(2);
        if(alive == 0)
        {
            completedRoom = true;
        }
        checkingIfClear = false;
    }


    public void restoreCamera()
    {
        moure = false;
        player.gameObject.GetComponent<PlayerController>().playerInBox = false;
    }

    public GameObject SpawnEnemy(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab, checkIfCanSpawn(), Quaternion.identity) as GameObject;

        return enemy;
    }

    private Vector3 checkIfCanSpawn()
    {
        int unstuck = 0;
        Vector3 randomPosition = RandomPointOnCircleEdge();

        while (unstuck < 20)
        {
            Collider[] hitColliders = Physics.OverlapSphere(randomPosition, 3);
            if (hitColliders.Length > 2)
            {
                randomPosition = RandomPointOnCircleEdge();
            }
            else
            {
                break;
            }

            unstuck++;
        }
        return randomPosition;
    }

    private Vector3 RandomPointOnCircleEdge()
    {
        float radius;
        if(illa.transform.localScale.x> illa.transform.localScale.z)
        {
            radius = illa.transform.localScale.z;
        }
        else
        {
            radius = illa.transform.localScale.x;
        }
        var vector2 = Random.insideUnitCircle * radius;
        return (new Vector3(vector2.x, 0, vector2.y) + this.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, illa.transform.localScale.x*1.2f);
    }
    private void ClearEnemies()
    {
        foreach (enemigo enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
        foreach (enemyshoot enemy in enemies2)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    [System.Serializable]
    struct BoxSave
    {
        public bool completedRoom;
    }

    public object CaptureState()
    {
        BoxSave data;
        data.completedRoom = completedRoom;

        return data;
    }

    public void RestoreState(object data)
    {
        BoxSave dataLoaded = (BoxSave)data;
        if (dataLoaded.completedRoom)
        {
            roomFinished();
            ClearEnemies();
        }
    }
}
