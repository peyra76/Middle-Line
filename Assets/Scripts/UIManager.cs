using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Interaction UI")]
    public RectTransform interactionPanel;
    public TextMeshProUGUI interactionText; 
    private CanvasGroup interactionCG;

    [Header("Item Pickup UI")]
    public RectTransform itemNotificationPanel;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    private CanvasGroup notificationCG;

    [Header("Animation Settings")]
    public float animSpeed = 5f;
    public Vector3 startOffset = new Vector3(0, -100, 0);

    private Vector3 interactionOriginalPos;
    private Vector3 notificationOriginalPos;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        interactionCG = interactionPanel.GetComponent<CanvasGroup>();
        notificationCG = itemNotificationPanel.GetComponent<CanvasGroup>();

        interactionOriginalPos = interactionPanel.localPosition;
        notificationOriginalPos = itemNotificationPanel.localPosition;

        interactionCG.alpha = 0;
        notificationCG.alpha = 0;
    }

    public void ToggleInteractionUI(bool state, string itemName = "")
    {
        if (state) interactionText.text = $"Press E to pick up {itemName}";

        StopCoroutine("AnimateInteraction");
        StartCoroutine(AnimateInteraction(state));
    }

    private IEnumerator AnimateInteraction(bool appearing)
    {
        yield return StartCoroutine(AnimatePanel(interactionPanel, interactionCG, appearing, interactionOriginalPos));
    }

    public void ShowItemNotification(string name, string description)
    {
        itemNameText.text = name;
        itemDescriptionText.text = description;
        StopCoroutine("NotificationSequence");
        StartCoroutine(NotificationSequence());
    }

    private IEnumerator NotificationSequence()
    {
        yield return StartCoroutine(AnimatePanel(itemNotificationPanel, notificationCG, true, notificationOriginalPos));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(AnimatePanel(itemNotificationPanel, notificationCG, false, notificationOriginalPos));
    }

    private IEnumerator AnimatePanel(RectTransform panel, CanvasGroup cg, bool appearing, Vector3 targetPos)
    {
        float timer = 0;
        Vector3 startPos = appearing ? targetPos + startOffset : targetPos;
        Vector3 endPos = appearing ? targetPos : targetPos + startOffset;

        float startAlpha = appearing ? 0 : 1;
        float endAlpha = appearing ? 1 : 0;

        panel.localPosition = startPos;

        while (timer < 1)
        {
            timer += Time.deltaTime * animSpeed;

            panel.localPosition = Vector3.Lerp(startPos, endPos, timer);
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, timer);

            float scale = Mathf.Lerp(0.8f, 1.0f, appearing ? timer : 1 - timer);
            panel.localScale = new Vector3(scale, scale, scale);

            yield return null;
        }

        panel.localPosition = endPos;
        cg.alpha = endAlpha;
    }
}