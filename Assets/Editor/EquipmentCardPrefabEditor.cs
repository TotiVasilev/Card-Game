using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

// One-time editor migration: turns the existing EquipmentCard.prefab into a
// normal layered prefab. Children are saved to the prefab asset, not spawned in play mode.
[InitializeOnLoad]
public static class EquipmentCardPrefabEditor {
    const string Path = "Assets/Generated/EquipmentCard.prefab";

    static EquipmentCardPrefabEditor() {
        EditorApplication.delayCall += UpgradeIfNeeded;
    }

    static RectTransform Rect(string name, Transform parent, float x, float y, float w, float h) {
        var existing = parent.Find(name);
        if (existing) return existing.GetComponent<RectTransform>();
        var go = new GameObject(name, typeof(RectTransform));
        var rect = go.GetComponent<RectTransform>();
        rect.SetParent(parent, false);
        rect.anchorMin = rect.anchorMax = new Vector2(.5f, .5f);
        rect.sizeDelta = new Vector2(w, h);
        rect.anchoredPosition = new Vector2(x, y);
        return rect;
    }

    static Image Image(string name, Transform parent, float x, float y, float w, float h, Sprite sprite, Color color) {
        var rect = Rect(name, parent, x, y, w, h);
        var image = rect.GetComponent<Image>();
        if (!image) image = rect.gameObject.AddComponent<Image>();
        image.sprite = sprite;
        image.color = color;
        image.raycastTarget = false;
        return image;
    }

    static Text Text(string name, Transform parent, float x, float y, float w, float h, int size, string value) {
        var rect = Rect(name, parent, x, y, w, h);
        var text = rect.GetComponent<Text>();
        if (!text) text = rect.gameObject.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = size;
        text.color = new Color(1f, .91f, .73f);
        text.alignment = TextAnchor.MiddleCenter;
        text.text = value;
        text.raycastTarget = false;
        return text;
    }

    static Sprite Art(string filename) {
        return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/" + filename + ".png");
    }

    static void UpgradeIfNeeded() {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(Path);
        if (!prefab) return;
        var view = prefab.GetComponent<EquipmentCardView>();
        if (view && view.artwork && view.border && view.title && view.effect && view.attack && view.defense)
            return;
        Upgrade();
    }

    [MenuItem("Cards/Upgrade Equipment Card Prefab")]
    public static void Upgrade() {
        if (!AssetDatabase.LoadAssetAtPath<GameObject>(Path)) {
            Debug.LogWarning("EquipmentCard.prefab was not found at " + Path);
            return;
        }
        var root = PrefabUtility.LoadPrefabContents(Path);
        try {
            var view = root.GetComponent<EquipmentCardView>();
            if (!view) view = root.AddComponent<EquipmentCardView>();
            var background = root.GetComponent<Image>();
            if (background) {
                background.sprite = null;
                background.color = new Color(.12f, .10f, .08f);
            }
            view.artwork = Image("Artwork", root.transform, 0, 55, 182, 190, Art("FireRing"), Color.white);
            view.effectPanel = Image("Effect panel", root.transform, 0, -76, 174, 68, null, new Color(.10f, .09f, .07f, .88f));
            view.border = Image("Border", root.transform, 0, 0, 190, 310, Art("Border"), Color.white);
            view.nameplate = Image("Nameplate", root.transform, -17, 112, 145, 27, null, new Color(.12f, .10f, .08f));
            view.emblem = Image("Faction emblem", root.transform, 72, 130, 32, 32, null, Color.clear);
            view.title = Text("Name", root.transform, -17, 112, 145, 27, 16, "Equipment");
            view.faction = Text("Faction", root.transform, 0, -43, 165, 20, 11, "");
            view.effect = Text("Effect", root.transform, 0, -79, 160, 54, 12, "");
            view.attackBadge = Image("Attack badge", root.transform, -58, -125, 48, 48, Art("Attack"), Color.white);
            view.defenseBadge = Image("Defense badge", root.transform, 58, -125, 48, 48, Art("Defense"), Color.white);
            view.attack = Text("Attack bonus", root.transform, -58, -125, 48, 40, 20, "+0");
            view.defense = Text("Defense bonus", root.transform, 58, -125, 48, 40, 20, "+0");
            PrefabUtility.SaveAsPrefabAsset(root, Path);
            Debug.Log("EquipmentCard.prefab now has editable artwork, border, labels, and stat layers.");
        } finally {
            PrefabUtility.UnloadPrefabContents(root);
        }
    }
}
