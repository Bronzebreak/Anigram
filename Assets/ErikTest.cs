using UnityEngine;

public class ErikTest : MonoBehaviour
{
    public TextMesh text;

    public GameObject prefab;

    int i;

    void Update()
    {
        i++;
        text.text = i.ToString();

        Instantiate(prefab, transform.position + Random.insideUnitSphere * 10, Random.rotation);
    }
}
