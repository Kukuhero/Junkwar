using UnityEngine;
using System.Collections;

public class ApfelkernController : MonoBehaviour {

    private Vector3 offset;
    private int speed = 3;

    // Use this for initialization
    void Start () {
        print(transform.parent.FindChild("Armature").transform.GetComponent<ApfelEnemyController>().target[0]);
        StartCoroutine(Projektilzerstoerung ());
        transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(transform.parent.FindChild("Armature").transform.GetComponent<ApfelEnemyController>().target[0].transform.position.x,2f,
            transform.FindChild("Armature").transform.GetComponent<ApfelEnemyController>().target[0].transform.position.y));
        offset = transform.parent.FindChild("Armature").transform.GetComponent<ApfelEnemyController>().target[0].transform.position - transform.position;
    }

    // Update is called once per frame
    void Update () 
    {
        transform.position += offset * speed * Time.deltaTime;

    }
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag) 
        {
            case "Haus":
                print("Apfelkern House hit");
                //transform.parent.GetComponent<ApfelEnemyController>().target[0].GetComponent<Health>().health -= 20;
                Destroy (gameObject);
                break;

            default:
                print("irgendwas hit Apfelkern" + other.gameObject);
                //Destroy(gameObject);
                break;
        }
    }

    IEnumerator Projektilzerstoerung()
    {
            yield return new WaitForSeconds (3);
            //Destroy (gameObject);

    }
}