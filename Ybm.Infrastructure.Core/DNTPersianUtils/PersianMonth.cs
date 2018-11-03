using System;
using System.Collections.Generic;
using System.Text;
//forked from https://github.com/VahidN/DNTPersianUtils.Core
namespace Ybm.Infrastructure.Core.DNTPersianUtils
{
    /// <summary>
    /// اجزای ماه شمسی
    /// </summary>
    public class PersianMonth
    {
        /// <summary>
        /// اولین روز ماه شمسی
        /// </summary>
        public DateTime StartDate { set; get; }

        /// <summary>
        /// آخرین روز ماه شمسی
        /// </summary>
        public DateTime EndDate { set; get; }
    }
}
