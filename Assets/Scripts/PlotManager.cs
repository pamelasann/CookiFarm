using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plotManager : MonoBehaviour
{
    bool isPlanted = false;
    SpriteRenderer plant;
    BoxCollider2D plantCollider;
    public Sprite[] plantStages;
    int plantStage = 0;
    float timeBTWStages = 2f;
    float timer;

    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    void Update() // Update is called once per frame
    {
        if (isPlanted)
        {
            timer = timer - Time.deltaTime;
            if (timer < 0 && plantStage < plantStages.Length - 1)
            {
                timer = timeBTWStages;
                plantStage++;
                UpdatePlant();
            }
        }
    }
    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if (plantStage == plantStages.Length - 1)
            {
                Harvest();
            }
        }
        else
        {
            Plant();
        }
    }
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
    }
    void Plant()
    {
        isPlanted = true;
        plantStage = 0;
        UpdatePlant();
        timer = timeBTWStages;
        plant.gameObject.SetActive(true);
    }
    void UpdatePlant()
    {
        plant.sprite = plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.sprite.bounds.size.y / 2);
    }
}