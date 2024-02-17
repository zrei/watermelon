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
    public struct FruitTypeStruct
    {
        public Sprite sprite;
        public Vector2 finalScale;
        public int score;
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
        }
        else if(fruitType.FruitName == FruitNames.CHIEF)
        {

        }else if (fruitType.FruitName == FruitNames.RAT)
        {

        }


        fruitType = type;
        GetComponent<SpriteRenderer>().sprite = fruitType.sprite;
        StopAllCoroutines();
        StartCoroutine(GrowAnim());

        //Add the new Component

        if (fruitType.FruitName == FruitNames.BOCCHI)
        {
            //Remove Bocchi Componennt
        }
        else if (fruitType.FruitName == FruitNames.CHIEF)
        {

        }
        else if (fruitType.FruitName == FruitNames.RAT)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10)
        {
            Destroy(gameObject); //Despawn and add penalty
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(!collision.gameObject.GetComponent<Fruit>())
        {
            return;
        }
        print("collision"); 
        if ((fruitType.FruitName == Chief.FruitName )|| fruitType.FruitName != collision.gameObject.GetComponent<Fruit>().fruitType.FruitName)
        {
            return; //Chief cannot merge, not the same fruit cant merge
        }

        if(gameObject.transform.position.y < collision.gameObject.transform.position.y)
        {
            Destroy(collision.gameObject);
        }
        AdvanceNextTier();
    }

    private void AdvanceNextTier()
    {
        //TODO: Add Score

        for(int i = 0; i < fruitTypes.Count; i++ )
        {
            if(fruitTypes[i].FruitName == fruitType.FruitName)
            {
                if( i + 1 == fruitTypes.Count) 
                {

                    Destroy(gameObject); // 2 Sharks, disappear
                }
                else
                {
                    SetFruitType(fruitTypes[i + 1]);
                    return;
                }
            }
        }
        
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
