using System;
using System.Collections.Generic;
using System.Text;
//forked from https://github.com/VahidN/DNTPersianUtils.Core
namespace Ybm.Infrastructure.Core.DNTPersianUtils
{
    /// <summary>
    /// Time Constants
    /// </summary>
    public static class TimeConstants
    {
        /// <summary>
        /// Day = 24 * Hour
        /// </summary>
        public const int Day = 24 * Hour;

        /// <summary>
        /// Hour = 60 * Minute
        /// </summary>
        public const int Hour = 60 * Minute;

        /// <summary>
        /// Minute = 60 * Second
        /// </summary>
        public const int Minute = 60 * Second;

        /// <summary>
        /// Month = 30 * Day
        /// </summary>
        public const int Month = 30 * Day;

        /// <summary>
        /// Second = 1
        /// </summary>
        public const int Second = 1;
    }
}
