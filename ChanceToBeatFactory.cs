using LiveSplit.ChanceToBeat;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(ChanceToBeatFactory))]

namespace LiveSplit.ChanceToBeat
{
    public class ChanceToBeatFactory : IComponentFactory
    {
        public string ComponentName => "Chance To Beat";

        public string Description => "Displays the probability of beating a specified time.";

        public ComponentCategory Category => ComponentCategory.Information;

        public IComponent Create(LiveSplitState state) => new ChanceToBeat(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => GetType().Assembly.GetName().Version;
    }
}
