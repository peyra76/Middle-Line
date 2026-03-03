using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public GameObject visualPrefab;
    public enum BonusType
    {
        AxBass,
        Coffee,
        DullKnife,
        Heart,
        Milk,
        Protein
    }

    public BonusType bonusType;
}