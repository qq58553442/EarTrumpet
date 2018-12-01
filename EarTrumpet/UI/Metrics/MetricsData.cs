using System;
using System.Collections.Generic;

namespace EarTrumpet.UI.Metrics
{
    class MetricsData
    {
        private static double s_currentSize = 1;

        public static Dictionary<string, double> Get()
        {
            var ret = new Dictionary<string, double>();

            ret.Add("DeviceIconSize", 24);
            ret.Add("DeviceIconTextSize", 24);
            ret.Add("AppIconSize", 24);
            ret.Add("AppIconMuteTextSize", 24);
            ret.Add("DeviceTitleCellHeight", 44);
            ret.Add("AppItemCellHeight", 44);
            ret.Add("DeviceItemCellHeight", 54);
            ret.Add("AppVolumeTextFontSize", 18);
            ret.Add("DeviceVolumeTextFontSize", 24);
            ret.Add("IconCellWidth", 65);
            ret.Add("VolumeCellWidth", 63);
            ret.Add("SliderThumbWidth", 8);
            ret.Add("SliderThumbHeight", 24);
            ret.Add("LargeWindowTextFontSize", 15);

            return ret;
        }
        public static Dictionary<string, double> Get(double sz)
        {
            var ret = new Dictionary<string, double>();
            foreach (var kv in Get())
            {
                ret[kv.Key] = kv.Value * sz;
            }
            return ret;
        }

        public static void ChangeFromMouseDelta(int delta)
        {
            s_currentSize += Math.Sign(delta) * 0.25;

            MetricsManager.Current.Apply(Get(s_currentSize));
        }
    }
}
