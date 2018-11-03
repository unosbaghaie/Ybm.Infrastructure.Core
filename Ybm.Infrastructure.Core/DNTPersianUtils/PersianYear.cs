using System;
using System.Collections.Generic;
using System.Text;
//forked from https://github.com/VahidN/DNTPersianUtils.Core
namespace Ybm.Infrastructure.Core.DNTPersianUtils
{
    /// <summary>
    /// اجزای سال شمسی
    /// </summary>
    public class PersianYear
    {
        /// <summary>
        /// اولین روز سال شمسی
        /// </summary>
        public DateTime StartDate { set; get; }

        /// <summary>
        /// آخرین روز سال شمسی
        /// </summary>
        public DateTime EndDate { set; get; }
    }
}
