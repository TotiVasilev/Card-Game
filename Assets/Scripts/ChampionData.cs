using UnityEngine;
[CreateAssetMenu(menuName="Cards/Champion")]
public class ChampionData : ScriptableObject {
 [System.Serializable] public class Form { public string displayName; public Sprite artwork; public int attack, defense; [TextArea] public string effect; }
 public string championId="arthur", faction="Arthurian Legends"; public Sprite factionEmblem;
 public Form baseForm=new Form(), signatureForm=new Form(), finalForm=new Form();
 public Form GetForm(int index) => index==2?finalForm:index==1?signatureForm:baseForm;
}
