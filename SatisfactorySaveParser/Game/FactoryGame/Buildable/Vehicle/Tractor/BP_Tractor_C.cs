using System;

namespace SatisfactorySaveParser.Game.FactoryGame.Buildable.Vehicle.Tractor
{
    [TypePath("/Game/FactoryGame/Buildable/Vehicle/Tractor/BP_Tractor.BP_Tractor_C")]
    [Serializable]
    public class BP_Tractor_C : Vehicle
    {
        public BP_Tractor_C() { }

        public BP_Tractor_C(string typePath, string rootObject, string instanceName) : base(typePath, rootObject, instanceName) { }
    }
}
