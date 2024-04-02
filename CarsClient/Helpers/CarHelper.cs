using CarsClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace CarsClient.Helpers
{
	public static class CarHelper
	{
		public static List<string> GetCarStyleTags(int imageCount, string prefix)
		{
			var width = ((float)100 / imageCount)
				.ToString("0.00000000", System.Globalization.CultureInfo.GetCultureInfo("en-US"));

			var f1 = $"#slides article{{ width: {width}%; float: left; }}";
			var f2 = $"#slides .image{{ width:{imageCount * 100}%;line-height: 0;}}";

			var f3 = "";
			var f4 = "";
			var f5 = "";
			var f6 = "";

			for (int i = 1; i <= imageCount; i++)
			{
				f3 += $"#switch_{i}:checked ~ #controls label:nth-child({(i - 1 == 0 ? imageCount : i - 1)}), ";
				f4 += $"#switch_{i}:checked ~ #controls label:nth-child({(i + 1 == imageCount + 1 ? 1 : i + 1)}), ";
				f5 += $"#switch_{i}:checked ~ #slides .image{{margin-left: -{(i - 1) * 100}%; }}";
				f6 += $"#switch_{i}:checked ~ #active label:nth-child({i}), ";
			}
			var path = "{background: url(../" + prefix;																/*was 0*/
			f3 = f3.Substring(0, f3.Length - 2) + path + "images/icons/left_arrow.svg) no-repeat;float: left;margin: -50% 0 0 -55px; display: block;height: 51px;width: 51px;} ";
			f4 = f4.Substring(0, f4.Length - 2) + path + "images/icons/right_arrow.svg) no-repeat; float: right;margin: -50% -55px 0 0;display: block;width: 51px;height: 51px;background-position: center;background-origin: cover;} ";
			f6 = f6.Substring(0, f6.Length - 2) + "{opacity: 1;} ";

			var styles = new List<string>();
			styles.Add(f1 + f2);
            styles.Add(f3 + f4);
            styles.Add(f5); 
			styles.Add(f6);

			return styles;

        }

		public static void Logout(HttpContext context)
		{
			if (context != null)
			{
				var cookies = context.Response.Cookies;
				cookies.Delete("_ym_uid");
				cookies.Delete(".AspNetCore.cookieC2");
				cookies.Delete("_ym_d");
				cookies.Delete("idsrv.session");
				cookies.Delete(".AspNetCore.cookieC1");
				cookies.Delete(".AspNetCore.cookie");
				cookies.Delete("idsrv");
				cookies.Delete(".AspNetCore.Antiforgery.vt3n6_Bd4OM");
			}
		}

        public static string GetQueryForCarFilter(
            Guid[]? manufactures,
            Guid[]? types,
            Guid[]? powerReserves,
            Guid[]? batteryCapacity,
            Guid[]? driveModes,
            int? filter_price_mn,
            int? filter_price_max)
        {
            var queryString = "?";
            if (manufactures != null && manufactures.Any())
                queryString += "manufactures=" + string.Join("&manufactures=", manufactures) + "&";
            if (types != null && types.Any())
                queryString += "carTypeIds=" + string.Join("&carTypeIds=", types) + "&";
            if (powerReserves != null && powerReserves.Any())
                queryString += "powerReserveParamIds=" + string.Join("&powerReserveParamIds=", powerReserves) + "&";
            if (batteryCapacity != null && batteryCapacity.Any())
                queryString += "batteryCapacity=" + string.Join("&batteryCapacity=", batteryCapacity) + "&";
            if (driveModes != null && driveModes.Any())
                queryString += "driveModeIds=" + string.Join("&driveModeIds=", driveModes) + "&";
            if (filter_price_mn != null)
                queryString += "priceFrom=" + filter_price_mn + "&";
            if (filter_price_max != null)
                queryString += "priceTo=" + filter_price_max + "&";

            if (!string.IsNullOrEmpty(queryString))
                queryString = queryString.TrimEnd('&');
            if (queryString.Length == 1)
                queryString = "";

            return queryString;
        }
    }
}