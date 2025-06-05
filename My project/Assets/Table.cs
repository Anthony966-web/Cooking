using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public int MaxChairCount;
    public float Range = 2f;
    public List<GameObject> Chairs = new List<GameObject>();

    void Start()
    {
        print(Chairs.Count);
    }


    void Update()
    {
        UpdateChairList();
    }

    void UpdateChairList()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, Range);

        HashSet<GameObject> chairsInRange = new HashSet<GameObject>();

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Chair"))
            {
                chairsInRange.Add(hit.gameObject);
            }
        }

        // Add new chairs in range
        foreach (GameObject chair in chairsInRange)
        {
            if (!Chairs.Contains(chair) && Chairs.Count < MaxChairCount)
            {
                Chairs.Add(chair);
            }
        }

        // Remove chairs that are no longer in range
        Chairs.RemoveAll(chair => chair == null || !chairsInRange.Contains(chair));
    }

    // Optional: visualize range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
