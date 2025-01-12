namespace Zebble
{
    using Foundation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SDK = global::Facebook.CoreKit;

    public partial class Facebook
    {
        public static void FacebookAutoLogAppEventsEnabled(bool enable) => SDK.Settings.SharedSettings.IsAutoLogAppEventsEnabled = enable;

        public static void EnableUpdatesOnAccessTokenChange(bool enable) => SDK.Profile.IsUpdatedWithAccessTokenChange = enable;

        public static void SetAdvertiserIDCollectionEnabled(bool enable) => SDK.Settings.SharedSettings.IsAdvertiserIdCollectionEnabled = enable;

        public static void ActivateAppEvents() => SDK.AppEvents.Shared.ActivateApp();

        public static void CallEvent(EventNames eventName, Dictionary<ParameterNames, object> @params, double? valueToSum = null)
        {
            var keys = @params.Select(x => new NSString(Enum.GetName(typeof(SDK.AppEventParameterName), x.Key.ToParameterName()))).ToArray();
            var values = @params.Select(x => NSObject.FromObject(x.Value)).ToArray();

            var param = new NSDictionary<NSString, NSObject>(keys, values);

            if (valueToSum.HasValue)
                SDK.AppEvents.Shared.LogEvent(eventName.ToEventName().ToString(), valueToSum.Value, param);
            else
                SDK.AppEvents.Shared.LogEvent(eventName.ToEventName().ToString(), param);
        }

        public static void CallEvent(string eventName, Dictionary<string, object> @params, double? valueToSum = null)
        {
            var keys = @params.Select(x => new NSString(x.Key)).ToArray();
            var values = @params.Select(x => NSObject.FromObject(x.Value)).ToArray();

            var param = new NSDictionary<NSString, NSObject>(keys, values);
            if (valueToSum.HasValue)
                SDK.AppEvents.Shared.LogEvent(eventName, valueToSum.Value, param);
            else
                SDK.AppEvents.Shared.LogEvent(eventName, param);
        }

        public static void CallLogPurchase(double purchaseAmount, string currency, Dictionary<string, object> @params = null, AccessToken accessToken = null)
        {
            NSDictionary<NSString, NSObject> param = null;
            if (@params != null)
            {
                var keys = @params.Select(x => new NSString(x.Key)).ToArray();
                var values = @params.Select(x => NSObject.FromObject(x.Value)).ToArray();
                param = new NSDictionary<NSString, NSObject>(keys, values);

                if (accessToken == null)
                    SDK.AppEvents.Shared.LogPurchase(purchaseAmount, currency, param);
                else
                    SDK.AppEvents.Shared.LogPurchase(purchaseAmount, currency, param, accessToken.ToSDKAccessToken());
            }
            else
                SDK.AppEvents.Shared.LogPurchase(purchaseAmount, currency);
        }

        static SDK.AppEventName ToEventName(this EventNames eventName)
        {
            switch (eventName)
            {
                case EventNames.AchieveLevel:
                    return SDK.AppEventName.AchievedLevel;
                case EventNames.InAppAdClick:
                    return SDK.AppEventName.AdClick;
                case EventNames.AddPaymentInfo:
                    return SDK.AppEventName.AddedPaymentInfo;
                case EventNames.AddToCart:
                    return SDK.AppEventName.AddedToCart;
                case EventNames.AddToWishlist:
                    return SDK.AppEventName.AddedToWishlist;
                case EventNames.CompleteRegistration:
                    return SDK.AppEventName.CompletedRegistration;
                case EventNames.CompleteTutorial:
                    return SDK.AppEventName.CompletedTutorial;
                case EventNames.Contact:
                    return SDK.AppEventName.Contact;
                case EventNames.CustomizeProduct:
                    return SDK.AppEventName.CustomizeProduct;
                case EventNames.Donate:
                    return SDK.AppEventName.Donate;
                case EventNames.FindLocation:
                    return SDK.AppEventName.FindLocation;
                case EventNames.InitiateCheckout:
                    return SDK.AppEventName.InitiatedCheckout;
                case EventNames.Rate:
                    return SDK.AppEventName.Rated;
                case EventNames.Schedule:
                    return SDK.AppEventName.Schedule;
                case EventNames.SpentCredits:
                    return SDK.AppEventName.SpentCredits;
                case EventNames.StartTrial:
                    return SDK.AppEventName.StartTrial;
                case EventNames.SubmitApplication:
                    return SDK.AppEventName.SubmitApplication;
                case EventNames.Subscription:
                    return SDK.AppEventName.Subscribe;
                case EventNames.UnlockAchievement:
                    return SDK.AppEventName.UnlockedAchievement;
                case EventNames.ViewContent:
                    return SDK.AppEventName.ViewedContent;
                case EventNames.Search:
                    return SDK.AppEventName.Searched;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventName));
            }
        }

        static SDK.AppEventParameterName ToParameterName(this ParameterNames paramName)
        {
            switch (paramName)
            {
                case ParameterNames.Level:
                    return SDK.AppEventParameterName.Level;
                case ParameterNames.AdType:
                    return SDK.AppEventParameterName.AdType;
                case ParameterNames.Success:
                    return SDK.AppEventParameterName.Success;
                case ParameterNames.ContentType:
                    return SDK.AppEventParameterName.ContentType;
                case ParameterNames.Currency:
                    return SDK.AppEventParameterName.Currency;
                case ParameterNames.ContentId:
                    return SDK.AppEventParameterName.ContentId;
                case ParameterNames.Content:
                    return SDK.AppEventParameterName.Content;
                case ParameterNames.RegistrationMethod:
                    return SDK.AppEventParameterName.RegistrationMethod;
                case ParameterNames.NumItems:
                    return SDK.AppEventParameterName.NumItems;
                case ParameterNames.PaymentInfoAvailable:
                    return SDK.AppEventParameterName.PaymentInfoAvailable;
                case ParameterNames.MaxRatingValue:
                    return SDK.AppEventParameterName.MaxRatingValue;
                case ParameterNames.SearchString:
                    return SDK.AppEventParameterName.SearchString;
                case ParameterNames.OrderId:
                    return SDK.AppEventParameterName.OrderId;
                case ParameterNames.Description:
                    return SDK.AppEventParameterName.Description;
                default:
                    throw new ArgumentOutOfRangeException(nameof(paramName));
            }
        }
    }
}