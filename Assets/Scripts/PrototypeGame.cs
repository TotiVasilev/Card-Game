using UnityEngine;
using UnityEngine.UI;
public class PrototypeGame : MonoBehaviour {
 public ChampionData champion; public EquipmentData fireRing, excalibur; public CardView championPrefab; public Button equipmentPrefab; public RectTransform hand, board; public Text status; public Button endTurn, restart; public bool signatureFirst;
 CardView active, handChampion; EquipmentData signature, universal; int turn, actions; bool drawn;
 void Start(){endTurn.onClick.AddListener(EndTurn);restart.onClick.AddListener(ResetGame);ResetGame();}
 void ResetGame(){foreach(Transform t in hand)Destroy(t.gameObject);foreach(Transform t in board)Destroy(t.gameObject);active=null;signature=null;universal=null;turn=1;actions=3;drawn=false;handChampion=Instantiate(championPrefab,hand);handChampion.Show(champion,null,null);handChampion.button.onClick.AddListener(Deploy);Draw(signatureFirst?excalibur:fireRing);Report("Play Arthur, then equip an item.");}
 void Deploy(){if(active||actions==0)return;active=handChampion;active.transform.SetParent(board,false);active.button.onClick.RemoveAllListeners();active.button.interactable=false;actions--;Report("Arthur deployed.");}
 void Draw(EquipmentData item){var b=Instantiate(equipmentPrefab,hand);var go=b.gameObject;go.name=item.displayName;var im=go.GetComponent<Image>();im.sprite=item.cardArtwork;b.onClick.AddListener(()=>Equip(item,go));}
 void Equip(EquipmentData item,GameObject card){if(!active){Report("Deploy Arthur first.");return;}if(actions==0){Report("No actions remaining.");return;}if(item.signature){if(signature||item.requiredChampionId!=champion.championId){Report("Signature slot unavailable.");return;}signature=item;}else{if(universal){Report("Universal slot occupied.");return;}universal=item;}actions--;Destroy(card);active.Show(champion,signature,universal);Report(item.displayName+" equipped.");}
 void EndTurn(){turn++;actions=3;if(!drawn){Draw(signatureFirst?fireRing:excalibur);drawn=true;}Report("New turn. Equip your next item.");}
 void Report(string message){status.text="Turn "+turn+"  |  Actions: "+actions+"/3\n"+message;}
}
