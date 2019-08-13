using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VttToSrtTranslateLanguage.Models
{
    class SubTime
    {
        private const string Arrow = " --> ";

        public int FromHour { get; private set; }
        public int FromMinute { get; private set; }
        public int FromSecond { get; private set; }
        public int FromMilliSecond { get; private set; }

        public int ToHour { get; private set; }
        public int ToMinute { get; private set; }
        public int ToSecond { get; private set; }
        public int ToMilliSecond { get; private set; }



        public static (SubTime time, bool isOk) TryParse(string text)
        {
            // text = "00:53:05,580 --> 00:53:08,170";



            return (null, false);
        }

        private static SubTime ParseArrowOrNull(string text)
        {
            if (text.Contains(Arrow))
            {
                // short "53:05,580 --> 53:08,170"
                if (Regex.IsMatch(text.Replace(Arrow, ""), @"^([0-5][0-9]\:[0-5][0-9][.,][[0-9]{3}){2}"))
                {
                    var time = new SubTime
                    {
                        FromMinute = int.Parse(text.Substring(0, 2)),
                        FromSecond = int.Parse(text.Substring(3, 2)),
                        FromMilliSecond = int.Parse(text.Substring(6, 3)),

                        ToMinute = int.Parse(text.Substring(14, 2)),
                        ToSecond = int.Parse(text.Substring(17, 2)),
                        ToMilliSecond = int.Parse(text.Substring(20, 3))
                    };

                    return time;
                }

                // long "00:53:05,580 --> 00:53:08,170"
                if (Regex.IsMatch(text.Replace(" --> ", ""), @"^(([0-1][0-9]|2[0-3])\:[0-5][0-9]\:[0-5][0-9][.,][[0-9]{3}){2}"))
                {
                    var time = new SubTime
                    {
                        FromHour = int.Parse(text.Substring(0, 2)),
                        FromMinute = int.Parse(text.Substring(3, 2)),
                        FromSecond = int.Parse(text.Substring(6, 2)),
                        FromMilliSecond = int.Parse(text.Substring(9, 3)),

                        ToHour = int.Parse(text.Substring(17, 2)),
                        ToMinute = int.Parse(text.Substring(20, 2)),
                        ToSecond = int.Parse(text.Substring(23, 2)),
                        ToMilliSecond = int.Parse(text.Substring(26, 3))
                    };

                    return time;
                }
            }

            return null;
        }

        public string ToSrtFormat()
        {
            return ToArrowFormat(',');
        }

        public string ToVttFormat()
        {
            return ToArrowFormat('.');
        }

        private string ToArrowFormat(char preMilliSecond)
        {
            var sb = new StringBuilder();

            sb.Append(FromHour.ToString("D2"))
                .Append(":")
                .Append(FromMinute.ToString("D2"))
                .Append(":")
                .Append(FromSecond.ToString("D2"))
                .Append(preMilliSecond)
                .Append(FromMilliSecond.ToString("D3"))
                .Append(Arrow)
                .Append(ToHour.ToString("D2"))
                .Append(":")
                .Append(ToMinute.ToString("D2"))
                .Append(":")
                .Append(ToSecond.ToString("D2"))
                .Append(preMilliSecond)
                .Append(ToMilliSecond.ToString("D3"));

            return sb.ToString();
        }

    }
}
