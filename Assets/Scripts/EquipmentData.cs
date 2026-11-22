using UnityEngine;
[CreateAssetMenu(menuName="Cards/Equipment")]
public class EquipmentData : ScriptableObject {
 public string displayName; public bool signature; public string requiredChampionId; public Sprite cardArtwork, slotIcon; public int attackBonus, defenseBonus; public bool fireVisual; [TextArea] public string effect;
}
