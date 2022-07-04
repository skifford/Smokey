using System;
using Smokey.Features.ValuesCollecting;
using Smokey.Models;

namespace Smokey.Demo.Features.ValuesCollecting
{
    public class Page : SmokeyPageObject<Page>
    {
        [Collectable]
        public int Number { get; private set; }
        
        public ICollectableProperty<int> Count { get; set; }

        public Component Component { get; }

        public Page() : base(webDriver: null)
        {
            Component = new Component();
        }
        
        public Page SetNumber(int number)
        {
            Number = number;
            return this;
        }
        
        public Page SetElementName(string name)
        {
            Component.SetElementName(name);
            return this;
        }
        
        public Page SetComponentTitle(string title)
        {
            Component.SetTitle(title);
            return this;
        }

        public Page SetComponentDescription(string description)
        {
            Component.SetDescription(description);
            return this;
        }
        
        public Page SetCount(int count)
        {
            Count.Value = count;
            return this;
        }
        
        public Page UpdateCount()
        {
            Count.Invoke();
            return this;
        }

        public Page Run(Action action)
        {
            action.Invoke();
            return this;
        }
    }
}
