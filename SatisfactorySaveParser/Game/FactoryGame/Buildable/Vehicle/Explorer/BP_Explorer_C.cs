using System;

namespace SatisfactorySaveParser.Game.FactoryGame.Buildable.Vehicle.Explorer
{
    [TypePath("/Game/FactoryGame/Buildable/Vehicle/Explorer/BP_Explorer.BP_Explorer_C")]
    [Serializable]
    public class BP_Explorer_C : Vehicle
    {
        public BP_Explorer_C() { }

        public BP_Explorer_C(string typePath, string rootObject, string instanceName) : base(typePath, rootObject, instanceName) { }
    }
}
