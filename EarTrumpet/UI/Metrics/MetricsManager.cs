using System;
using System.Collections.Generic;
using System.Windows;

namespace EarTrumpet.UI.Metrics
{
    public class MetricsManager
    {
        public static MetricsManager Current { get; private set; }

        public MetricsManager()
        {
            Current = this;
        }

        public void Apply(Dictionary<string, double> values)
        {
            var oldDictionary = Application.Current.Resources.MergedDictionaries[1];
            var newDictionary = new ResourceDictionary();

            foreach (var newValue in values)
            {
                var oldEntry = oldDictionary[newValue.Key];
                if (oldEntry == null)
                {
                    throw new InvalidOperationException($"{newValue.Key} is missing from the previous dictionary");
                }

                if (oldEntry is GridLength)
                {
                    newDictionary[newValue.Key] = new GridLength(newValue.Value);
                }
                else
                {
                    newDictionary[newValue.Key] = newValue.Value;
                }
            }

#if DEBUG
            foreach (var key in oldDictionary.Keys)
            {
                // Verify the new dictionary has the old entry. i.e. fix MetricsData.cs
                var newEntry = newDictionary[key];
                if (newEntry == null)
                {
                    throw new InvalidOperationException($"{key} is missing from the new dictionary");
                }
            }
#endif

            Application.Current.Resources.MergedDictionaries.RemoveAt(1);
            Application.Current.Resources.MergedDictionaries.Insert(1, newDictionary);
        }
    }
}
