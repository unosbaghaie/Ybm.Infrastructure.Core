using System;
using System.Linq;
using Xunit;
using Ybm.Infrastructure.Core.NCronTabCore;

namespace Ybm.Infrastructure.Core.Test
{
    public class CronPatternGeneratorTest
    {
        #region Secondly
        [Theory]
        [InlineData("*/10 * * * * *")]
        public void Secondly_Every_10_Seconds(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 7, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.True(res.Any());
        }
        #endregion


        #region Minutely
        [Theory]
        [InlineData("* */10 * * * *")]
        public void Minutely_Every_10_Minutes(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 10, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.True(res.Any());
        }
        #endregion


        #region Hourly
        [Theory]
        [InlineData("* * */5 * * *")]
        public void Hourly_Every_5_Hours(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 10, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.True(res.Any());
        }
        #endregion


        #region Daily
        [Theory]
        [InlineData("* 0 9 */5 * * *")]
        public void Daily_Every_5_Days_At_9(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 10, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.True(res.Any());
        }

        [Theory]
        [InlineData("* 0 9 */1 * * *")]
        public void Daily_EveryDay_At_9(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 10, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.True(res.Any());
        }
        #endregion


        #region Weekly
        [Theory]
        [InlineData("* 10 9 * * */0,5")]
        public void Weekly_Every_Saturday_And_Thursday_At_9(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 10, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.True(res.Any());
        }

        [Theory]
        [InlineData("* 10 9 * * */1")]
        public void Weekly_Every_Saturday_At_9(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 10, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.True(res.Any());
        }

        #endregion


        #region Monthly
        [Theory]
        [InlineData("* 10 9 15 */1,3,5,7,9,11 *")]
        public void Montly_SpecificDay_SpecificMonths(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 10, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.True(res.Any());
        }

        [Theory]
        [InlineData("* 0 9 L * *")]
        public void Montly_LastDay(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 12, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.Equal("1397/3/31 9:00:00 AM", res[0]);
            Assert.Equal("1397/4/31 9:00:00 AM", res[1]);
            Assert.Equal("1397/5/31 9:00:00 AM", res[2]);
            Assert.Equal("1397/6/31 9:00:00 AM", res[3]);
            Assert.Equal("1397/7/30 9:00:00 AM", res[4]);
            Assert.Equal("1397/8/30 9:00:00 AM", res[5]);
        }

        [Theory]
        [InlineData("* 0 9 L */1,3,5,7,9,11 *")]
        public void Montly_LastDay_SpecificMonths(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 12, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);
        }

        [Theory]
        [InlineData("* 0 9 1 * *")]
        public void Montly_FirstDay_At_9(string cronPattern)
        {
            var startDate = new DateTime(2018, 1, 1, 10, 50, 25);
            var endDate = new DateTime(2019, 12, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.Equal("1396/11/1 9:00:00 AM", res[0]);
            Assert.Equal("1397/9/1 9:00:00 AM", res[10]);
            Assert.Equal("1398/7/1 9:00:00 AM", res[20]);
        }

        [Theory]
        [InlineData("* 0 9 10 * *")]
        public void Montly_Tenth_At_9(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 12, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.Equal("1397/4/10 9:00:00 AM", res[0]);
            Assert.Equal("1397/5/10 9:00:00 AM", res[1]);
            Assert.Equal("1397/6/10 9:00:00 AM", res[2]);
            Assert.Equal("1397/7/10 9:00:00 AM", res[3]);
            Assert.Equal("1397/8/10 9:00:00 AM", res[4]);
        }

        [Theory]
        [InlineData("* 10 9 15 */1,3,5,7,9,11 *")]
        public void Montly_SpecificDay_in_SpecificMonths(string cronPattern)
        {
            var startDate = new DateTime(2018, 6, 1, 10, 50, 25);
            var endDate = new DateTime(2018, 12, 1, 10, 50, 25);

            var res = new CronTabScheduler().Occurances(cronPattern, startDate, endDate, true);

            Assert.Equal("1397/3/15 9:10:00 AM", res[0]);
            Assert.Equal("1397/5/15 9:10:00 AM", res[1]);
            Assert.Equal("1397/7/15 9:10:00 AM", res[2]);
        }
        #endregion
    }
}
