using MachinationsClone.Models.Entities.Graph;

namespace MachinationsClone.Models.Machinations
{
    public static class PullMode
    {
        public const string PULL_ALL = "PULL_ALL";
        public const string PULL_ANY = "PULL_ANY";
        public const string PUSH_ALL = "PUSH_ALL";
        public const string PUSH_ANY = "PUSH_ANY";
        
        public static string GetDefault()
        {
            return PULL_ANY;
        }
        
        public static PullModeEnum GetDefaultEnum()
        {
            return GetEnum(GetDefault());
        }
        
        public static bool IsValid(string mode)
        {
            return mode == PULL_ALL || mode == PULL_ANY || mode == PUSH_ALL || mode == PUSH_ANY;
        }
        
        public static string GetFromEnum(PullModeEnum mode)
        {
            switch (mode)
            {
                case PullModeEnum.PullAll:
                    return PULL_ALL;
                case PullModeEnum.PullAny:
                    return PULL_ANY;
                case PullModeEnum.PushAll:
                    return PUSH_ALL;
                case PullModeEnum.PushAny:
                    return PUSH_ANY;
                default:
                    return PULL_ANY;
            }
        }
        
        public static PullModeEnum GetEnum(string mode)
        {
            switch (mode)
            {
                case PULL_ALL:
                    return PullModeEnum.PullAll;
                case PULL_ANY:
                    return PullModeEnum.PullAny;
                case PUSH_ALL:
                    return PullModeEnum.PushAll;
                case PUSH_ANY:
                    return PullModeEnum.PushAny;
                default:
                    return PullModeEnum.PullAny;
            }
        }
    }
}