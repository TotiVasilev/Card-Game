using UnityEngine;
using UnityEngine.UI;

// Builds an editable, layered equipment card inside the existing EquipmentCard button.
// Each layer reads an independent sprite or text field from EquipmentData.
[RequireComponent(typeof(Button))]
public class EquipmentCardView : MonoBehaviour {
    public EquipmentData data;
    public Image artwork, border, nameplate, emblem, effectPanel, attackBadge, defenseBadge;
    public Text title, faction, effect, attack, defense;

    static RectTransform Place(string name, Transform parent, float x, float y, float w, float h) {
        var go = new GameObject(name, typeof(RectTransform));
        var rect = go.GetComponent<RectTransform>();
        rect.SetParent(parent, false);
        rect.anchorMin = rect.anchorMax = new Vector2(.5f, .5f);
        rect.sizeDelta = new Vector2(w, h);
        rect.anchoredPosition = new Vector2(x, y);
        return rect;
    }

    static Image Layer(string name, Transform parent, float x, float y, float w, float h) {
        var image = Place(name, parent, x, y, w, h).gameObject.AddComponent<Image>();
        image.raycastTarget = false;
        return image;
    }

    static Text Label(string name, Transform parent, float x, float y, float w, float h, int size) {
        var text = Place(name, parent, x, y, w, h).gameObject.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = size;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = new Color(1f, .91f, .73f);
        text.raycastTarget = false;
        return text;
    }

    void EnsureLayers() {
        if (artwork) return;
        artwork = Layer("Artwork", transform, 0, 55, 182, 190);
        effectPanel = Layer("Effect panel", transform, 0, -76, 174, 68);
        border = Layer("Border", transform, 0, 0, 190, 310);
        nameplate = Layer("Nameplate", transform, -17, 112, 145, 27);
        emblem = Layer("Faction emblem", transform, 72, 130, 32, 32);
        title = Label("Name", transform, -17, 112, 145, 27, 16);
        faction = Label("Faction", transform, 0, -43, 165, 20, 11);
        effect = Label("Effect", transform, 0, -79, 160, 54, 12);
        attackBadge = Layer("Attack badge", transform, -58, -125, 48, 48);
        defenseBadge = Layer("Defense badge", transform, 58, -125, 48, 48);
        attack = Label("Attack bonus", transform, -58, -125, 48, 40, 20);
        defense = Label("Defense bonus", transform, 58, -125, 48, 40, 20);
    }

    static void Set(Image image, Sprite sprite, Color fallback) {
        image.sprite = sprite;
        image.color = sprite ? Color.white : fallback;
        image.enabled = sprite != null || fallback.a > 0;
    }

    public void Show(EquipmentData item) {
        if (!item) return;
        data = item;
        EnsureLayers();
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
