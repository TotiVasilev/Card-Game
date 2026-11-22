using UnityEngine;
using UnityEngine.UI;

// Builds an editable, layered equipment card inside the existing EquipmentCard button.
// Each layer reads an independent sprite or text field from EquipmentData.
[RequireComponent(typeof(Button))]
public class EquipmentCardView : MonoBehaviour {
    public EquipmentData data;
    public Image artwork, border, nameplate, emblem, effectPanel, attackBadge, defenseBadge;
    public Text title, faction, effect, attack, defense;

    // Visual children are authored and saved in EquipmentCard.prefab by the editor.
    // Never create or rearrange prefab layers at runtime.
    static void Set(Image image, Sprite sprite, Color fallback) {
        image.sprite = sprite;
        image.color = sprite ? Color.white : fallback;
        image.enabled = sprite != null || fallback.a > 0;
    }

    public void Show(EquipmentData item) {
        if (!item) return;
        data = item;
        if (!artwork || !border || !title || !effect || !attack || !defense) {
            Debug.LogError("EquipmentCard prefab is missing editable layers. Use Cards > Upgrade Equipment Card Prefab in the Unity editor.", this);
            return;
        }
        // The original flattened art is retained as a fallback for older equipment assets.
        var background = GetComponent<Image>();
        if (background) {
            background.sprite = null;
            background.color = new Color(.12f, .10f, .08f);
        }
        Set(artwork, item.artworkLayer ? item.artworkLayer : item.cardArtwork, new Color(.22f, .25f, .30f));
        Set(border, item.borderLayer, Color.clear);
        Set(nameplate, item.nameplateLayer, new Color(.12f, .10f, .08f));
        Set(emblem, item.factionEmblem, Color.clear);
        Set(effectPanel, item.effectPanelLayer, new Color(.10f, .09f, .07f, .88f));
        Set(attackBadge, item.attackBadgeLayer, new Color(.35f, .17f, .09f));
        Set(defenseBadge, item.defenseBadgeLayer, new Color(.15f, .25f, .33f));
        title.text = item.displayName;
        faction.text = item.faction;
        effect.text = string.IsNullOrEmpty(item.effect) ? item.flavorText : item.effect;
        attack.text = item.attackBonus > 0 ? "+" + item.attackBonus : item.attackBonus.ToString();
        defense.text = item.defenseBonus > 0 ? "+" + item.defenseBonus : item.defenseBonus.ToString();
    }
}
