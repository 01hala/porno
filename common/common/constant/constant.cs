using abelkhan;
using service;

namespace constant
{
    public class constant
    {
        public const int CoinPrice = 100;

        public const int MoneyPrice = 5;

        public const long UserTimeoutTmp = 8 * 60 * 60 * 1000;

        public const long FreeOrderWaitTime = 30 * 1000;

        public const long GenImageWaitTime = 30 * 1000;

        public const long RefreshHotspotTime = 10 * 60 * 1000;

        public const string PornoDBName = "porno";

        public const string UserDBCollection = "user";

        public const string CheckAccountDBCollection = "check_account";

        public const string CheckMailDBCollection = "check_mail";

        public const string CheckDBPrimaryKey = "token";

        public const string ImageDBCollection = "image";

        public const string ImageHotspot = "hotspot";

        public const string ImageHotspotPrompt = "prompt";

        public const string ImageHotspotAccess = "save_image_state";

        public const string ImageCreateTime = "create_time";

        public const long ImageHotspotTimeoutTime = 24 * 60 * 60 * 1000;

        public const string ImageGuid = "img_guid";

        public const string ImageCreator = "account";

        public const int ImageCacheCount = 100;

        public const int ImagePageCount = 32;

        public const string HotspotDBCollection = "hotspot";

        public const string HotspotPrompt = "prompt";

        public const string HotspotPromptHotspot = "count";

        public const int HotspotPromptCount = 8;

        public const string GuestGuidCollection = "guest_guid";

        public const string ImageGuidCollection = "image_guid";

        public const string EssayGuidCollection = "essay_guid";

        public const string InsideGuid = "inside_guid";

        public const string GenImageOrderQueue = "gen_image_order_queue";

        public const string SDModelPath = "../model/";

        public const string SDOutputDir = "../image/";

        public const string ImageUrl = "http://127.0.0.1/image/";

        public const string WterMark = "pornoai.art";

        public const long DelTmpImageTimeTick = 7 * 24 * 60 * 60 * 1000;

        public const long DefaultRedisCacheTimeout = 7 * 24 * 60 * 60 * 1000;
    }
}
