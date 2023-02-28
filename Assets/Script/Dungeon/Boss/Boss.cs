using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public static Boss instance;

    public GameObject fallingRock;
    public GameObject fallingArea;
    public GameObject attackArea;
    public GameObject healthBar;
    public GameObject healthGem;
    public Slider slider; 
    public List<AudioSource> audioSources;
    public float timerNormal = 20f;
    public float timerHeavy = 30f;
    public float gemsCollect = 30f;
    public int numberObjectFallen;
    public float maxHealth;

    private Animator animator;
    private Dictionary<Vector3, List<GameObject>> objectAndAreas = new Dictionary<Vector3, List<GameObject>>();
    private GameObject heavyAttackArea;
    private float health;
    private float lastNormal = 0.0f;
    private float lastHeavy= 0.0f;
    private float lastCollect = 0.0f;
    private bool bossOperative = false;
    private float heavyAttackTime;
    private bool heavyAttackIncoming = false;
    private bool counter = false;
    private bool gems = false;
    private List<string> attack = new List<string>();
    private int rand;

    private void Awake()
    {
        health = maxHealth; //maxHealth;
        Debug.Log(health);
        instance = this;
    }

    void Start()
    {
        //Debug.Log(Character.instance.transform.position.z);
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        slider.value = health / maxHealth;

        if (health != 0)
        {
            if (heavyAttackIncoming)
            {
                if (Time.time > heavyAttackTime)
                {
                    Destroy(heavyAttackArea);
                    if (!counter)
                        Player.instance.HealthDown();
                    heavyAttackIncoming = false;
                    lastHeavy = Time.time + timerHeavy;
                    lastNormal = Time.time + timerNormal;
                    counter = false;
                }
            }
            if (Player.instance.transform.position.z > 205)
            {
                ClearPath.instance.ActiveWalls(true);
                healthBar.SetActive(true);
                bossOperative = true;
                //Debug.Log(Time.time);
                if (attack.Count == 2 && Time.time > heavyAttackTime+1f)
                {
                    if (gems)
                    {
                        gems = false;
                        CreateHealthCrystal();
                    }

                    if (Time.time > lastCollect)
                    {
                        attack.Clear();
                        lastHeavy = Time.time + timerHeavy;
                        lastNormal = Time.time + 5f;
                    }

                }
                else if (Time.time > lastHeavy)
                {
                    //fa attacco pesante
                    animator.SetTrigger("Heavy");

                    if (!attack.Contains("heavy"))
                        attack.Add("heavy");

                    rand = Random.Range(0, 4);
                    Debug.Log(rand);
                    heavyAttackArea = Instantiate(attackArea, transform.position, Quaternion.identity);
                    heavyAttackIncoming = true;
                    audioSources[rand].Play();
                    heavyAttackTime = Time.time + 5f;
                    lastHeavy = Time.time + timerHeavy;
                    lastNormal = Time.time + timerNormal;

                    if (attack.Count == 2)
                    {
                        lastCollect = Time.time + gemsCollect;
                        gems = true;
                    }

                }
                else if (Time.time > lastNormal)
                {
                    if (!attack.Contains("normal"))
                        attack.Add("normal");

                    animator.SetTrigger("Normal");
                    CreateFallingRocksPosition();
                    //fa attacco normale
                    lastNormal = Time.time + timerNormal;

                    if (attack.Count == 2)
                    {
                        lastCollect = Time.time + gemsCollect;
                        gems = true;
                    }
                }

                CheckDestroyAllRocks();
            }
            else
            {
                lastHeavy = Time.time + timerHeavy;
                lastNormal = Time.time + timerNormal;
            }
        } else
        {
            Destroy(slider.gameObject);
            ClearPath.instance.ActiveWalls(true);
            this.gameObject.AddComponent<Rigidbody>();
            Debug.Log("Hai Sconfitto il boss");
        }

        if(this.transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
        

    }

    private void CreateFallingRocksPosition()
    {
        //boss
        //angolo alto destro Vector3(649.406921,0.269782186,222.440506)
        //angolobasso sinistro Vector3(662.469971,0.269782186,217.610001)

        //spawn sul giocatore
        Vector3 playerPosition = new Vector3
            (Player.instance.transform.position.x,
                15, Player.instance.transform.position.z);
        CreateFallingRocks(playerPosition);

        for (int i=0; i<2; i++)
        {
            Vector3 randomposition = new Vector3(Random.Range(633, 678), 15, Random.Range(205, 230));

            while (((randomposition.x >= 654 && randomposition.x <= 665.5)
                || (randomposition.z >= 221 && randomposition.z <= 226)) && !objectAndAreas.ContainsKey(randomposition))
            {
                randomposition = new Vector3(Random.Range(633, 678), 15, Random.Range(205, 230));
            }
            CreateFallingRocks(randomposition);
        }
    }

    private void CreateHealthCrystal()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomposition = new Vector3(Random.Range(633, 678), 0, Random.Range(205, 230));

            while (((randomposition.x >= 654 && randomposition.x <= 665.5)
                || (randomposition.z >= 221 && randomposition.z <= 226)) && !objectAndAreas.ContainsKey(randomposition))
            {
                randomposition = new Vector3(Random.Range(633, 678), 0, Random.Range(205, 230));
            }
            Instantiate(healthGem, randomposition, Quaternion.identity);
        }
    }

    private void CreateFallingRocks(Vector3 position)
    {
        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.Add(Instantiate(fallingRock, position, Quaternion.identity));
        gameObjects[0].AddComponent<Rigidbody>();
        position.y = 0.01f;
        gameObjects.Add(Instantiate(fallingArea, position, Quaternion.identity));
        objectAndAreas.Add(position, gameObjects);
    }
    private void CheckDestroyAllRocks()
    {
        foreach (KeyValuePair<Vector3, List<GameObject>> entry in objectAndAreas)
        {
            GameObject gameObject = entry.Value[0];
            if(gameObject.transform.position.y < -10)
            {
                DestroyAllRocks();
                break;
            }
        }
    }
    public void DestroyAllRocks()
    {
        foreach (KeyValuePair<Vector3, List<GameObject>> entry in objectAndAreas)
        {
            foreach (var attackObject in entry.Value)
            {
                Destroy(attackObject);
            }
        }
        objectAndAreas.Clear();
    }

    public void DodgeAttack(KeyCode keyPressed)
    {
        switch (rand)
        {
            case 0:
                counter = (keyPressed == KeyCode.Q);
                break;
            case 1:
                counter = (keyPressed == KeyCode.W);
                break;
            case 2:
                counter = (keyPressed == KeyCode.A);
                break;
            case 3:
                counter = (keyPressed == KeyCode.S);
                break;
        }
    }

    public void ReduceHealth()
    {
        if(health > 0)
            health--;
    }

    public bool BossOperativeAndAttack()
    {
        return this.bossOperative && this.heavyAttackIncoming;
    }
}
