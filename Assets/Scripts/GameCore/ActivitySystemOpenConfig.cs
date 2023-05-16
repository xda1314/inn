using Ivy.Activity;
using System.Collections.Generic;

public class ActivitySystemOpenConfig : Ivy.Activity.ActivitySystemConfig
{

    public override List<ActivityConfigKey> Configs
    {
        get
        {
            List<ActivityConfigKey> list = new List<ActivityConfigKey>();

            // 添加活动配置

            //// 7日签到
            //ActivityConfigKey signIn = new ActivityConfigKey()
            //{
            //    activityType = ActivityType.SignIn,
            //    remoteConfigKey = "activity_sign_in",
            //    cloudDataCollection = CloudDataCollection_Default,
            //    cloudDataKey = "SignIn",
            //};
            //list.Add(signIn);

            // 交叉推广
            ActivityConfigKey crossPromotion = new ActivityConfigKey()
            {
                activityType = ActivityType.CrossPromotion,
                remoteConfigKey = "activity_cross_promotion",
                cloudDataCollection = CloudDataCollection_Default,
                cloudDataKey = "CrossPromotion",
            };
            list.Add(crossPromotion);

            // Add other activity here!


            return list;
        }
    }
}
