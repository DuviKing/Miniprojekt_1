using UnityEngine;
//SpawnDamageText(combat position, damage amount as string)
public class damgeIndicatorManeger : MonoBehaviour
{
    public GameObject damageTextPrefab; // Assign your prefab in the inspector

    public void SpawnDamageText(Vector3 position, string text)
    {
        GameObject damageText = Instantiate(damageTextPrefab, position, Quaternion.identity);
        damageText.GetComponentInChildren<TextMesh>().text = text;
        StartCoroutine(MoveAndDestroy(damageText));
    }

    private System.Collections.IEnumerator MoveAndDestroy(GameObject obj)
    {
        float elapsed = 0f;
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = startPos + Vector3.up * 2f; // Move up by 2 units

        while (elapsed < 3f)
        {
            obj.transform.position = Vector3.Lerp(startPos, endPos, elapsed / 3f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }
}