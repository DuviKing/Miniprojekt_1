using UnityEngine;
using TMPro;
//SpawnDamageText(combat position, damage amount as string)
public class DamageIndicatorManager : MonoBehaviour
{
    void Start()
    {
        
    }
    public GameObject damageTextPrefab; // Assign your prefab in the inspector

    public void SpawnDamageText(Vector3 worldPosition, string text)
    {
        var canvas = GameObject.Find("UI Canvas");
        var damageText = Instantiate(damageTextPrefab, canvas.transform);
        damageText.GetComponent<TextMeshProUGUI>().text = text;

        // Convert world position to screen position
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);

        // Convert screen position to canvas local position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            screenPoint,
            canvas.GetComponent<Canvas>().worldCamera,
            out Vector2 localPoint);

        var rectTransform = damageText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = localPoint;

        StartCoroutine(MoveAndDestroy(damageText));
    }

    private System.Collections.IEnumerator MoveAndDestroy(GameObject obj)
    {
        float duration = 2f;
        float fadeDuration = 1f;
        float elapsed = 0f;
        var rectTransform = obj.GetComponent<RectTransform>();
    Vector2 startPos = rectTransform.anchoredPosition;
    // Add a small random horizontal offset to the end position
    float randomX = Random.Range(-30f, 30f); // Adjust range as desired
    Vector2 endPos = startPos + new Vector2(randomX, 100f); // Move up by 100 units with random X
        var text = obj.GetComponent<TextMeshProUGUI>();
        Color originalColor = text.color;

        while (elapsed < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / duration);
            if (elapsed > duration - fadeDuration)
            {
                float fade = 1f - ((elapsed - (duration - fadeDuration)) / fadeDuration);
                text.color = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a * fade);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }
}