using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    public float growTime = 0.2f;
    public Vector2 initialScale = new Vector2(0.1f, 0.1f);
    
    public FruitTypeStruct fruitType;

    public float chiefChance = 0.1f;

    [System.Serializable]
    public class FruitTypeStruct
    {
        public Sprite sprite;
        public Vector2 finalScale;
        public int score = 1;
        public FruitNames FruitName;

    }

    [SerializeField]
    public enum FruitNames
    {
        PIG,
        OTTER,
        CAT,
        BOCCHI,
        RAT,
        SHARK,
        CHIEF

    }
    [SerializeField]
    public List<FruitTypeStruct> fruitTypes;
    public FruitTypeStruct Chief;

    // Start is called before the first frame update
    void Start()
    {
        //Randomly Spawn Levels 0-2, or chief
        if(UnityEngine.Random.Range(0f,1) < chiefChance) //Spawns Chief
        {
            SetFruitType(Chief);
        }
        else
        {
            int randomType = UnityEngine.Random.Range(0, 2);
            SetFruitType(fruitTypes[randomType]);
        }

    }

    void SetFruitType(FruitTypeStruct type)
    {
        if(fruitType.FruitName == FruitNames.BOCCHI)
        {
            //Remove Bocchi Componennt
            Destroy(GetComponent<BocchiPath>());
            gameObject.GetComponent<Rigidbody2D>().mass = 1;
        }
        else if(fruitType.FruitName == FruitNames.CHIEF)
        {

        }else if (fruitType.FruitName == FruitNames.RAT)
        {
            Destroy(GetComponent<RatJump>());
        }
        else if (fruitType.FruitName == FruitNames.SHARK)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;

        }


        fruitType = type;
        GetComponent<SpriteRenderer>().sprite = fruitType.sprite;
        StopAllCoroutines();
        StartCoroutine(GrowAnim());

        //Add the new Component

        if (fruitType.FruitName == FruitNames.BOCCHI)
        {
            //Add Bocchi Componennt
            gameObject.AddComponent<BocchiPath>();
            gameObject.GetComponent<Rigidbody2D>().mass = 10;
        }
        else if (fruitType.FruitName == FruitNames.CHIEF)
        {
            gameObject.AddComponent<ExplosionComponent>();
        }
        else if (fruitType.FruitName == FruitNames.RAT)
        {
            gameObject.AddComponent<RatJump>();
        }
        else if (fruitType.FruitName == FruitNames.SHARK)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10)
        {
            if(fruitType.FruitName != Chief.FruitName)
            {
                GameManager.instance.AddPenalty();
            }

            Destroy(gameObject); //Despawn and add penalty
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(!collision.gameObject.GetComponent<Fruit>())
        {
            return;
        }
        if ((fruitType.FruitName == Chief.FruitName )|| fruitType.FruitName != collision.gameObject.GetComponent<Fruit>().fruitType.FruitName)
        {
            return; //Chief cannot merge, not the same fruit cant merge
        }

        if(gameObject.transform.position.y >= collision.gameObject.transform.position.y)
        {
            Destroy(collision.gameObject);
            AdvanceNextTier();
        }
       
    }

    private void AdvanceNextTier()
    {
        
        

        for(int i = 0; i < fruitTypes.Count; i++ )
        {
            if(fruitTypes[i].FruitName == fruitType.FruitName)
            {
                if( i + 1 == fruitTypes.Count) 
                {
                 AddScore(2 * fruitType.score);
                 Destroy(gameObject); // 2 Sharks, disappear
                }
                else
                {

                    AddScore(fruitType.score);
                    SetFruitType(fruitTypes[i + 1]);
                    return;
                }
            }
        }
        
    }


    public void AddScore(int amt)
    {
        GameManager.instance.AddScore(amt);
    }
    public IEnumerator GrowAnim()
    {
        transform.localScale = initialScale;
        float timePassed = 0f;
        while(timePassed < growTime)
        {
            transform.localScale = Vector3.Lerp(initialScale, fruitType.finalScale, (timePassed / growTime));
            yield return 0;
            timePassed += Time.deltaTime;
        }

    
    }
}
