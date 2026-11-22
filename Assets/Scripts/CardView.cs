using UnityEngine;
using UnityEngine.UI;
public class CardView : MonoBehaviour {
 public Image artwork, factionEmblem, signatureSlot, universalSlot, fireOverlay; public Text title, faction, attack, defense, effect, signatureLabel, universalLabel; public Button button;
 public void Show(ChampionData data, EquipmentData signature, EquipmentData universal) {
  var form=data.GetForm(signature!=null?(universal!=null?2:1):0);
  title.text=form.displayName; faction.text=data.faction; artwork.sprite=form.artwork; artwork.color=form.artwork?Color.white:new Color(.22f,.25f,.3f); factionEmblem.sprite=data.factionEmblem; factionEmblem.enabled=data.factionEmblem!=null;
  attack.text=(form.attack+(signature?signature.attackBonus:0)+(universal?universal.attackBonus:0)).ToString(); defense.text=(form.defense+(signature?signature.defenseBonus:0)+(universal?universal.defenseBonus:0)).ToString(); effect.text=form.effect;
  SetSlot(signatureSlot,signatureLabel,signature,"S"); SetSlot(universalSlot,universalLabel,universal,"U"); fireOverlay.gameObject.SetActive(universal!=null&&universal.fireVisual);
 }
 void SetSlot(Image image,Text label,EquipmentData item,string empty) { image.sprite=item?item.slotIcon:null; image.color=image.sprite?Color.white:new Color(.10f,.09f,.07f); label.text=image.sprite?"":item?item.displayName:empty; }
 void Update(){if(fireOverlay&&fireOverlay.gameObject.activeSelf){var c=fireOverlay.color;c.a=.20f+.12f*Mathf.Sin(Time.time*5);fireOverlay.color=c;}}
}
