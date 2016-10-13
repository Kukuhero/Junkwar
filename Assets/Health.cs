using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public int health;
    public GameObject Loot;
    private int maxhealth;
    private Vector3 screenPos;
    public Texture2D healthTexture;
    private int Position = 30;

	// Use this for initialization
	void Start () 
    {
        maxhealth = health;
	}
	
	// Update is called once per frame
	void Update () 
    {
        print(health);
        if (health <= 0) 
        {
            health = 0;
            Destroy (gameObject);
            //Destroy(gameObject);
            if (Random.Range(0f, 1f) <= 1f) 
            {
                if (Loot != null)
                {
                    Instantiate(Loot, new Vector3(gameObject.transform.position.x, 1f, gameObject.transform.position.z), gameObject.transform.rotation);
                }
            }

        }
	}

    void OnGUI()
    {
        if (health != maxhealth) 
        {
            screenPos = Camera.main.WorldToScreenPoint (gameObject.transform.position);
            GUI.DrawTexture (new Rect (screenPos.x - 40, Screen.height - screenPos.y - Position, health / 2, 5), healthTexture);
            GUI.color = Color.black;
            GUI.Label (new Rect (screenPos.x - 35, Screen.height - screenPos.y - Position, 50, 30), "" + health + "/" + maxhealth);
        }
}
}
