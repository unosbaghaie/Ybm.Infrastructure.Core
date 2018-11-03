using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Ybm.Infrastructure.Core.DNTPersianUtils;

namespace Ybm.Infrastructure.Core.Test.DNTPersianUtilsTest
{
    public class HumanReadableIntegerTests
    {
        [Fact]
        public void Test_NumberToText_Works()
        {
            var actual = 1234567.NumberToText(Language.Persian);
            Assert.Equal(expected: "یک میلیون و دویست و سی و چهار هزار و پانصد و شصت و هفت", actual: actual);
        }


    }
}
