using UnityEngine;
using System.Collections;

public class ApfelkernController : MonoBehaviour {

    private Vector3 targetposition;
    private int speed = 3;
    float step;

    // Use this for initialization
    void Start () {
        StartCoroutine(Projektilzerstoerung ());
        step = speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(ApfelEnemyController.target[0].transform.position.x,2f,ApfelEnemyController.target[0].transform.position.y));
        targetposition = new Vector3(ApfelEnemyController.target[0].transform.position.x,2f,ApfelEnemyController.target[0].transform.position.y) - transform.position;
    }

    // Update is called once per frame
    void Update () 
    {
        transform.position += targetposition * step;

    }
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag) 
        {
            case "Haus":
                print("Apfelkern House hit");
                ApfelEnemyController.target[0].GetComponent<Health>().health -= 20;
                Destroy (gameObject);
                break;

            default:
                print("irgendwas hit Apfelkern" + other.gameObject);
                break;
        }
    }

    IEnumerator Projektilzerstoerung()
    {
            yield return new WaitForSeconds (3);
            Destroy (gameObject);

    }
}