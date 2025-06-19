using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Units/Create Unit", order = 1)]
public class UnitSO : ScriptableObject
{
    public string unitName;
    public int health;
    public float speed;
    public GameObject prefab;
    public Sprite icon;
    public int cost;
    public string description;
}