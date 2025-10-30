using UnityEngine;


public class DetectionRange : MonoBehaviour
{
    public EnemyAI enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.isdead)
        {
            Collider2D collider2D = GetComponent<Collider2D>();
            collider2D.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
            if (other.CompareTag("Player"))
            {
                enemy.playerInRange = true;
            }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
            if (other.CompareTag("Player"))
            {
                enemy.playerInRange = false;
            }
        
    }
}
