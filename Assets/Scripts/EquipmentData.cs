using UnityEngine;
[CreateAssetMenu(menuName="Cards/Equipment")]
public class EquipmentData : ScriptableObject {
 public string displayName; public bool signature; public string requiredChampionId; public Sprite cardArtwork, slotIcon;
 [Header("Equipment Card Visuals")]
 public Sprite artworkLayer, borderLayer, nameplateLayer, factionEmblem, effectPanelLayer, attackBadgeLayer, defenseBadgeLayer;
 public string faction;
 [TextArea] public string flavorText; public int attackBonus, defenseBonus; public bool fireVisual; [TextArea] public string effect;
}
