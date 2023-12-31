﻿using Windows.System;

namespace XCApp
{
    public class Constants 
    {

        public static string XCAPIUrl = "https://www.xeno-canto.org/api/2/recordings?query=";
        public static string NoneStr = "NONE";
        public static string Long_byncsa = "Creative Commons Attribution-NonCommercial-ShareAlike";
        public static string Short_byncsa = "by-nc-sa";
        public static string Long_byncnd = "Creative Commons Attribution-NonCommercial-NoDerivatives";
        public static string Short_byncnd = "by-nc-nd";
        public static string Long_bysa = "Creative Commons Attribution-ShareAlike";
        public static string Short_bysa = "by-sa";
        public static string UrlScheme = "https:";
        public static string UrlFFTS = "ffts/XC";
        public static string UrlFFTSSmallImage = "-small.png";
        public static string UrlFFTSLargeImage = "-large.png";
        public static string UrlFFTSFullImage = "-full.png";
        public static double IconButtonSize = 64;

        public static List<string> Years = new List<string> { };

        public static List<string> Months = new List<string> { };

        public static readonly String[] Licenses =
        {
            NoneStr,
            Long_byncsa,
            Long_byncnd,
            Long_bysa
        };

        public static readonly String[] SongTypes = 
        {
            NoneStr,
            "song",
            "call",
            "alarm call",
            "flight call",
            "drumming",
            "begging call",
            "male",
            "female",
            "juvenil"
        };

        //q TAG
        public static readonly String[] Qualities = { NoneStr, "A", "B", "C", "D", "E" , "0"};
        public static readonly String[] QualityOperators = { NoneStr, "=", "<", ">" };

        //area TAG
        public static readonly String[] Areas = { NoneStr, "Africa", "America", "Asia", "Australia", "Europe"};

        public static readonly String[] Countries = 
        {
            NoneStr,
            "Afghanistan",
            "Albania",
            "Algeria",
            "Andorra",
            "Angola",
            "Antarctica",
            "Antigua & Departments",
            "Argentina",
            "Armenia",
            "Australia",
            "Austria",
            "Azerbaijan",
            "Bahamas",
            "Bahrain",
            "Bangladesh",
            "Barbados",
            "Belarus",
            "Belgium",
            "Belize",
            "Benin",
            "Bhutan",
            "Bolivia",
            "Bosnia Herzegovina",
            "Botswana",
            "Brazil",
            "Brunei",
            "Bulgaria",
            "Burkina Faso",
            "Burundi",
            "Cambodia",
            "Cameroon",
            "Canada",
            "Cape Verde",
            "Central African Republic",
            "Chad",
            "Chile",
            "China",
            "Colombia",
            "Comoros",
            "Congo (Brazzaville)",
            "Congo (Democratic Republic)",
            "Costa Rica",
            "Croatia",
            "Cuba",
            "Cyprus",
            "Czech Republic",
            "Denmark",
            "Djibouti",
            "Dominica",
            "Dominican Republic",
            "East Timor",
            "Ecuador",
            "Egypt",
            "El Salvador",
            "Equatorial Guinea",
            "Eritrea",
            "Estonia",
            "Ethiopia",
            "Fiji",
            "Finland",
            "France",
            "French Guiana",
            "Gabon",
            "Gambia",
            "Georgia",
            "Germany",
            "Ghana",
            "Greece",
            "Grenada",
            "Guatemala",
            "Guinea",
            "Guinea-Bissau",
            "Guyana",
            "Haiti",
            "Honduras",
            "Hungary",
            "Iceland",
            "India",
            "Indonesia",
            "Iran",
            "Iraq",
            "Ireland",
            "Israel",
            "Italy",
            "Ivory Coast",
            "Jamaica",
            "Japan",
            "Jordan",
            "Kazakhstan",
            "Kenya",
            "Kiribati",
            "Kuwait",
            "Kyrgyzstan",
            "Laos",
            "Latvia",
            "Lebanon",
            "Lesotho",
            "Liberia",
            "Libya",
            "Liechtenstein",
            "Lithuania",
            "Luxembourg",
            "Macedonia",
            "Madagascar",
            "Malawi",
            "Malaysia",
            "Maldives",
            "Mali",
            "Malta",
            "Marshall Islands",
            "Mauritania",
            "Mauritius",
            "Mexico",
            "Micronesia",
            "Moldova",
            "Monaco",
            "Mongolia",
            "Montenegro",
            "Morocco",
            "Mozambique",
            "Myanmar",
            "Namibia",
            "Nauru",
            "Nepal",
            "Netherlands",
            "New Zealand",
            "Nicaragua",
            "Niger",
            "Nigeria",
            "North Korea",
            "Norway",
            "Oman",
            "Pakistan",
            "Palau",
            "Panama",
            "Papua New Guinea",
            "Paraguay",
            "Peru",
            "Philippines",
            "Poland",
            "Portugal",
            "Puerto Rico",
            "Qatar",
            "Romania",
            "Russian Federation",
            "Rwanda",
            "San Marino",
            "Sao Tome",
            "Saudi Arabia",
            "Senegal",
            "Serbia",
            "Seychelles",
            "Sierra Leone",
            "Singapore",
            "Slovakia",
            "Slovenia",
            "Solomon Islands",
            "Somalia",
            "South Africa",
            "South Korea",
            "South Sudan",
            "Spain",
            "Sri Lanka",
            "St Kitts & Nevis",
            "St Lucia",
            "St Vincent & Grenadines",
            "Sudan",
            "Suriname",
            "Swaziland",
            "Sweden",
            "Switzerland",
            "Syria",
            "Taiwan",
            "Tajikistan",
            "Tanzania",
            "Thailand",
            "Togo",
            "Tonga",
            "Trinidad & Tobago",
            "Tunisia",
            "Turkey",
            "Turkmenistan",
            "Tuvalu",
            "Uganda",
            "Ukraine",
            "United Arab Emirates",
            "United Kingdom",
            "United States",
            "Uruguay",
            "Uzbekistan",
            "Vanuatu",
            "Vatican City",
            "Venezuela",
            "Vietnam",
            "Western Samoa",
            "Yemen",
            "Zambia",
            "Zimbabwe"
        };

        public static void MonthsFill()
        {
            Months.Clear();
            Months.Add(Constants.NoneStr);
            for (int i = 0; i < 12; i++)
            {
                Months.Add (System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i]);
            }
        }

        public static void YearsFill()
        {
            Years.Clear();
            Years.Add (Constants.NoneStr);
            for (int i = 1; i < 150; i++)
            {
                Years.Add ((DateTime.Now.Year - i + 1).ToString());
            }
        }

    }
}
