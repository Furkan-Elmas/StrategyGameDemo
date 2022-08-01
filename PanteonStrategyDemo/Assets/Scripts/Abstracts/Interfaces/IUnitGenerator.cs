using PanteonStrategyDemo.Abstracts.Enums;

namespace PanteonStrategyDemo.Abstracts.Interfaces
{
    public interface IUnitGenerator
    {
        void GenerateUnit(UnitType unitType,int unitCost);
    }
}