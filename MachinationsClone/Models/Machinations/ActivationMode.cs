using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Machinations
{
    public static class ActivationMode
    {
        public const string AUTO = "AUTO";
        public const string PASSIVE = "PASSIVE";
        public const string ON_START = "ON_START";
        
        public static string GetDefault()
        {
            return AUTO;
        }
        
        public static ActivationModeEnum GetDefaultEnum()
        {
            return GetEnum(GetDefault());
        }
        
        public static bool IsValid(string mode)
        {
            return mode == AUTO || mode == PASSIVE || mode == ON_START;
        }
        
        public static string GetFromEnum(ActivationModeEnum mode)
        {
            switch (mode)
            {
                case ActivationModeEnum.Auto:
                    return AUTO;
                case ActivationModeEnum.Passive:
                    return PASSIVE;
                case ActivationModeEnum.OnStart:
                    return ON_START;
                default:
                    return AUTO;
            }
        }
        
        public static ActivationModeEnum GetEnum(string mode)
        {
            switch (mode)
            {
                case AUTO:
                    return ActivationModeEnum.Auto;
                case PASSIVE:
                    return ActivationModeEnum.Passive;
                case ON_START:
                    return ActivationModeEnum.OnStart;
                default:
                    return ActivationModeEnum.Auto;
            }
        }
    }
}