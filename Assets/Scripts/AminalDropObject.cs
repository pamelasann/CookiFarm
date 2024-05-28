using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AminalDropObject : MonoBehaviour
{
    public CollectableType type; //creando animal que da objeto
    bool isPlanted = false;
    SpriteRenderer animalObj;
    BoxCollider2D animalCollider;
    int animalStage = 0;
    float timer;
    public AnimalObject selectedAnimal; //plantObject crheck if script

    public Collectable collectableInstance;


    void Start()
    {
        animalObj = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animalCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    void Update() // Update is called once per frame
    {
        if (isPlanted)
        {
            timer = timer - Time.deltaTime;
            if (timer < 0 && animalStage < selectedAnimal.animalStages.Length - 1)
            {
                timer = selectedAnimal.timeBTWStages;
                animalStage++;
                UpdatePlant();
            }
        }
    }
    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if (animalStage == selectedAnimal.animalStages.Length - 1)
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
        animalObj.gameObject.SetActive(false);
        SingletonManager.Instance.inventory.Add(collectableInstance);
    }
    void Plant()
    {
        isPlanted = true;
        animalStage = 0;
        UpdatePlant();
        timer = selectedAnimal.timeBTWStages;
        animalObj.gameObject.SetActive(true);
    }
    void UpdatePlant()
    {
        animalObj.sprite = selectedAnimal.animalStages[animalStage];
        animalCollider.size = animalObj.sprite.bounds.size;
        animalCollider.offset = new Vector2(0, animalObj.sprite.bounds.size.y / 2);
    }
}