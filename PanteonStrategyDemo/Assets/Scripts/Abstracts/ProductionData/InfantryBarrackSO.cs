using UnityEngine;

namespace PanteonStrategyDemo.Abstracts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Infantry Barrack", menuName = "ProductionSO/Barracks/Infantry Barrack")]
    public class InfantryBarrackSO : BarrackDataSO
    {
        [SerializeField] GameObject _infantryBarrackPrefab;
        [SerializeField] string _name = "Infantry Barrack";
        [SerializeField] int _cost = 50;

        public override GameObject ProductionPrefab { get => _infantryBarrackPrefab; set => _infantryBarrackPrefab = value; }
        public override string Name { get => _name; set => _name = value; }
        public override int Cost { get => _cost; set => _cost = value; }
    }
}