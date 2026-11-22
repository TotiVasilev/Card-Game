using UnityEngine;

[CreateAssetMenu(fileName = "NewSupport", menuName = "Cards/Support")]
public class SupportData : ScriptableObject {
    [Header("Card Identity")]
    public string displayName = "New Support";
    public string faction;
    [Header("Card Artwork")]
    public Sprite artwork;
    public Sprite border;
    public Sprite nameplate;
    public Sprite factionEmblem;
    public Sprite effectPanel;
    [Header("Displayed Text (no gameplay logic yet)")]
    [TextArea(3, 8)] public string effectDescription;
    [TextArea(2, 5)] public string flavorText;
}
