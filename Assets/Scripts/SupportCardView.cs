using UnityEngine;
using UnityEngine.UI;

// Display-only: all visual children are serialized objects in SupportCard.prefab.
public class SupportCardView : MonoBehaviour {
    public SupportData data;
    public Image artwork, border, nameplate, factionEmblem, effectPanel;
    public Text title, faction, effectDescription;

    public void Show(SupportData card) {
        if (!card) return;
        data = card;
        if (!artwork || !border || !nameplate || !factionEmblem || !effectPanel ||
            !title || !faction || !effectDescription) {
            Debug.LogError("SupportCard prefab is missing UI references.", this);
            return;
        }
        Apply(artwork, card.artwork);
        Apply(border, card.border);
        Apply(nameplate, card.nameplate);
        Apply(factionEmblem, card.factionEmblem);
        Apply(effectPanel, card.effectPanel);
        title.text = card.displayName;
        faction.text = card.faction;
        effectDescription.text = card.effectDescription;
    }

    static void Apply(Image image, Sprite sprite) {
        // An empty override means the prefab's own visual design remains unchanged.
        if (sprite) {
            image.sprite = sprite;
            image.color = Color.white;
            image.enabled = true;
        }
    }
}
