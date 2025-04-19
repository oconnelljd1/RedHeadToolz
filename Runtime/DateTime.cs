using System.Collections.Generic;
using UnityEngine;
using System;

namespace RedHeadToolz.Utils
{
    public static class DateTimeExtensions
    {
        public static double GetTotalSeconds(this DateTime dateTime)
        {
            return dateTime.Subtract(DateTime.MinValue).TotalSeconds;
        }
    }
}