using PanteonStrategyDemo.Abstracts.ProductionTypes;

namespace PanteonStrategyDemo.Abstracts.Interfaces
{
    public interface IUnitGenerator
    {
        void GenerateUnit(UnitType unitType,int unitCost);
    }
}