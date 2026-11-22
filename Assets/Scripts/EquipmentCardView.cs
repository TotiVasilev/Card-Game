using UnityEngine;
using UnityEngine.UI;

// Display-only component: every visual layer is saved and editable in EquipmentCard.prefab.
// EquipmentData provides optional per-card overrides and card text.
[RequireComponent(typeof(Button))]
public class EquipmentCardView : MonoBehaviour {
    public EquipmentData data;
    public Image artwork, border, nameplate, emblem, effectPanel, attackBadge, defenseBadge;
    public Text title, faction, effect, attack, defense;

    // Visual children are authored and saved in EquipmentCard.prefab by the editor.
    // Never create or rearrange prefab layers at runtime.
    static void Set(Image image, Sprite sprite, Color fallback) {
        // Empty data fields leave prefab artwork and colors intact.
        if (sprite) {
            image.sprite = sprite;
            image.color = Color.white;
            image.enabled = true;
        } else if (!image.sprite && fallback.a > 0) {
            image.color = fallback;
            image.enabled = true;
        }
    }

    public void Show(EquipmentData item) {
        if (!item) return;
        data = item;
        if (!artwork || !border || !nameplate || !emblem || !effectPanel ||
            !attackBadge || !defenseBadge || !title || !faction || !effect || !attack || !defense) {
            Debug.LogError("EquipmentCard prefab is missing serialized visual references.", this);
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
