using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.ComponentModel;
using System.Threading.Tasks;


namespace XCApp
{
    public class XCAPIClass
    {
        public static string QueryRequest;
        public static string JsonData;

        public static async Task<string> ServerRequest(string url)
        {
            using (var w = new WebClient())
            {
                // attempt to download JSON data as a string
                try
                {
                    //+++Implement Timeout
                    Task<string> JsonDataTask = w.DownloadStringTaskAsync(url);
                    JsonData=await JsonDataTask;
                    return HttpStatusCode.OK.ToString();
                }
                catch (WebException e)
                {
                    JsonData = string.Empty;
                    Console.WriteLine("\n\nException Message :" + e.Message);
                    //+++if (e.Status == WebExceptionStatus.ProtocolError)
                    //+++{
                    //+++Console.WriteLine("\n\nStatus Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                    //+++Console.WriteLine("\n\nStatus Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                    //+++}
                    return e.Message.ToString();
                }

            }
        }

        public static T Deserialize_json_data<T>(string json_data) where T : new()
        {

            //Replace content of Class T instead of appending
            var settings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };

            // if string with JSON data is not empty, deserialize it to class and return its instance 
            return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data, settings) : new T();

        }

        public class XCAPIResponseError
        {
            // "error": {
            //     "code":"missing_parameter",
            //     "message":"No query specified"
            // }
            [JsonProperty("code")]
            public string Code { get; set; }
            [JsonProperty("message")]
            public string Message { get; set; }
        }

        public class XCAPIRecordingsMain
        {
            //For a successful query, a HTTP 200 response code will be returned, with a payload in the following format:
            [JsonProperty("numRecordings")]
            public static string NumRecordings { get; set; }//"1"
            [JsonProperty("numSpecies")]
            public static string NumSpecies { get; set; }//"1"
            [JsonProperty("page")]
            public static string Page { get; set; } //1
            [JsonProperty("numPages")]
            public static string NumPages { get; set; } //1
                                                 //public string //"recordings"{ get; set; }:[
                                                 //    ...,
                                                 //    Array of Recording objects(see below),
                                                 //    ...
                                                 //    ]
            [JsonProperty("recordings")]
            public static List<XCAPIRecordings> Recordings { get; set; }
        }

        public class XCAPIRecordings
        {
            //Recording Object
            [JsonProperty("id")]
            public string Id { get; set; } //"134880"
            [JsonProperty("gen")]
            public string Gen { get; set; } //"Pheucticus"
            [JsonProperty("sp")]
            public string Sp { get; set; } //"ludovicianus"
            [JsonProperty("ssp")]
            public string Ssp { get; set; } //""
            [JsonProperty("en")]
            public string En { get; set; } //"Rose-breasted Grosbeak"
            [JsonProperty("rec")]
            public string Rec { get; set; } //"Jonathon Jongsma"
            [JsonProperty("cnt")]
            public string Cnt { get; set; } //"United States"
            [JsonProperty("loc")]
            public string Loc { get; set; } //"Grey Cloud Dunes SNA, Washington, Minnesota"
            [JsonProperty("lat")]
            public string Lat { get; set; } //"44.793"
            [JsonProperty("lng")]
            public string Lng { get; set; } //"-92.962"
            [JsonProperty("type")]
            public string Type { get; set; } //"song",
            [JsonProperty("file")]
            public string File { get; set; } //"http:\/\/www.xeno-canto.org\/134880\/download"
            [JsonProperty("lic")]
            public string Lic { get; set; } //"http:\/\/creativecommons.org\/licenses\/by-sa\/3.0\/"
            [JsonProperty("url")]
            public string Url { get; set; } //":"http:\/\/www.xeno-canto.org\/134880"
            [JsonProperty("q")]
            public string Q { get; set; } //"A"
            [JsonProperty("time")]
            public string Time { get; set; } //"9:16"
            [JsonProperty("date")]
            public string Date { get; set; } //"2013-05-25"

            public string XCId => string.Format("XC{0}", Id);
            public string FullSName => string.Format("{0} {1}", Gen, Sp);
            public string Citation => string.Format("{0}, XC{1}. Accessible at www.xeno-canto.org/{1}.", Rec, Id);
        

        }

        public class XCAPISearch : INotifyPropertyChanged
        {
            private string _Name;
            public string Name
            {
                get { return _Name; }
                set
                {
                    if (_Name != value)
                    {
                        _Name = value;
                        OnPropertyChanged("Name");
                    }
                }
            }

            private string _Gen;
            public string Gen
            {
                get { return _Gen; }
                set
                {
                    if (_Gen != value)
                    {
                        _Gen = value;
                        OnPropertyChanged("Gen");
                    }
                }
            }

            //+++Missing Ssp there is no tag in documetation
            //private string _Ssp;
            //public string Ssp
            //{
            //    get { return _Ssp; }
            //    set
            //    {
            //        if (_Ssp != value)
            //        {
            //            _Ssp = value;
            //            OnPropertyChanged("Ssp");
            //        }
            //    }
            //}

            private string _Rec;
            public string Rec
            {
                get { return _Rec; }
                set
                {
                    if (_Rec != value)
                    {
                        _Rec = value;
                        OnPropertyChanged("Rec");
                    }
                }
            }

            public string Cnt;
            private int _CntIndex;
            public int CntIndex
            {
                get { return _CntIndex; }
                set
                {
                    if (_CntIndex != value)
                    {
                        _CntIndex = value;
                        Cnt = Constants.Countries[CntIndex];
                        OnPropertyChanged("CntIndex");
                    }
                }
            }

            private string _Loc;
            public string Loc
            {
                get { return _Loc; }
                set
                {
                    if (_Loc != value)
                    {
                        _Loc = value;
                        OnPropertyChanged("Loc");
                    }
                }
            }

            private string _Rmk;
            public string Rmk
            {
                get { return _Rmk; }
                set
                {
                    if (_Rmk != value)
                    {
                        _Rmk = value;
                        OnPropertyChanged("Rmk");
                    }
                }
            }

            public string Type;
            private int _TypeIndex;
            public int TypeIndex
            {
                get { return _TypeIndex; }
                set
                {
                    if (_TypeIndex != value)
                    {
                        _TypeIndex = value;
                        Type = Constants.SongTypes[TypeIndex];
                        OnPropertyChanged("TypeIndex");
                    }
                }
            }

            private string _Nr;
            public string Nr
            {
                get { return _Nr; }
                set
                {
                    if (_Nr != value)
                    {
                        _Nr = value;
                        OnPropertyChanged("Nr");
                    }
                }
            }

            private string _Also;
            public string Also
            {
                get { return _Also; }
                set
                {
                    if (_Also != value)
                    {
                        _Also = value;
                        OnPropertyChanged("Also");
                    }
                }
            }

            public string Lic;
            private int _LicIndex;
            public int LicIndex
            {
                get { return _LicIndex; }
                set
                {
                    if (_LicIndex != value)
                    {
                        _LicIndex = value;
                        Lic = Constants.Licenses[LicIndex];
                        OnPropertyChanged("LicIndex");
                    }
                }
            }

            public string Q;
            private int _QIndex;
            public int QIndex
            {
                get { return _QIndex; }
                set
                {
                    if (_QIndex != value)
                    {
                        _QIndex = value;
                        Q = Constants.Qualities[QIndex];
                        OnPropertyChanged("QIndex");
                    }
                }
            }

            public string QO;
            private int _QOIndex;
            public int QOIndex
            {
                get { return _QOIndex; }
                set
                {
                    if (_QOIndex != value)
                    {
                        _QOIndex = value;
                        QO = Constants.QualityOperators[QOIndex];
                        OnPropertyChanged("QOIndex");
                    }
                }
            }

            public string Area;
            private int _AreaIndex;
            public int AreaIndex
            {
                get { return _AreaIndex; }
                set
                {
                    if (_AreaIndex != value)
                    {
                        _AreaIndex = value;
                        Area = Constants.Areas[AreaIndex];
                        OnPropertyChanged("AreaIndex");
                    }
                }
            }

            private DateTime _DateSearch;
            public DateTime DateSearch
            {
                get { return _DateSearch; }
                set
                {
                    if (_DateSearch != value)
                    {
                        _DateSearch = value;
                        OnPropertyChanged("DateSearch");
                    }
                }
            }

            private bool _DateSince;
            public bool DateSince
            {
                get { return _DateSince; }
                set
                {
                    if (_DateSince != value)
                    {
                        _DateSince = value;
                        OnPropertyChanged("DateSince");
                    }
                }
            }

            public string Year;
            private int _YearIndex;
            public int YearIndex
            {
                get { return _YearIndex; }
                set
                {
                    if (_YearIndex != value)
                    {
                        _YearIndex = value;
                        Year = Constants.Years[YearIndex];
                        OnPropertyChanged("YearIndex");
                    }
                }
            }

            public string Month;
            private int _MonthIndex;
            public int MonthIndex
            {
                get { return _MonthIndex; }
                set
                {
                    if (_MonthIndex != value)
                    {
                        _MonthIndex = value;
                        Month = Constants.Months[MonthIndex];
                        OnPropertyChanged("MonthIndex");
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                var changed = PropertyChanged;
                if (changed != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

                }
            }

        }

        public static string BuildTheQuery(XCAPISearch XCQuery)
        {
            string queryRequest="";

            //Build the query
            //+++ Missing Ssp there is no tag in documetation

            if (!String.IsNullOrEmpty(XCQuery.Name))
            {
                queryRequest = XCQuery.Name + " ";
            }

            if (!String.IsNullOrEmpty(XCQuery.Gen))
            {
                queryRequest += "gen:\"" + XCQuery.Gen + "\" ";
            }

            if (!String.IsNullOrEmpty(XCQuery.Rec))
            {
                queryRequest += "rec:\"" + XCQuery.Rec + "\" ";
            }

            if (!String.IsNullOrEmpty(XCQuery.Cnt) && XCQuery.Cnt != Constants.NoneStr)
            {
                if (XCQuery.Cnt.ToUpper() != Constants.NoneStr)
                    queryRequest += "cnt:\"" + XCQuery.Cnt + "\" ";
            }

            if (!String.IsNullOrEmpty(XCQuery.Loc))
            {
                queryRequest += "loc:\"" + XCQuery.Loc + "\" ";
            }

            if (!String.IsNullOrEmpty(XCQuery.Rmk))
            {
                queryRequest += "rmk:\"" + XCQuery.Rmk + "\" ";
            }

            if (!String.IsNullOrEmpty(XCQuery.Also))
            {
                queryRequest += "also:\"" + XCQuery.Also + "\" ";
            }

            if (!String.IsNullOrEmpty(XCQuery.Type) && XCQuery.Type != Constants.NoneStr)
            {
                if (XCQuery.Type.ToUpper() != Constants.NoneStr)
                    queryRequest += "type:\"" + XCQuery.Type + "\" ";
            }

            if (!String.IsNullOrEmpty(XCQuery.Nr))
            {
                queryRequest += "nr:\"" + XCQuery.Nr + "\" ";
            }

            if (!String.IsNullOrEmpty(XCQuery.Lic) && XCQuery.Lic != Constants.NoneStr)
            {
                if (XCQuery.Lic==Constants.Long_byncnd)
                {
                    queryRequest += "lic:\"" + Constants.Short_byncnd + "\" ";
                }
                if (XCQuery.Lic == Constants.Long_byncsa)
                {
                    queryRequest += "lic:\"" + Constants.Short_byncsa + "\" ";
                }
                if (XCQuery.Lic == Constants.Long_bysa)
                {
                    queryRequest += "lic:\"" + Constants.Short_bysa + "\" ";
                }
            }

            if (String.IsNullOrEmpty(XCQuery.QO) || XCQuery.QO == "=" || XCQuery.QO == Constants.NoneStr)
            { 
                if (!String.IsNullOrEmpty(XCQuery.Q))
                {
                    if (XCQuery.Q.ToUpper() != Constants.NoneStr)
                        queryRequest += "q:" + XCQuery.Q + " ";
                }
            }
            else
            {
                if (XCQuery.QO==">")
                    queryRequest += "q>:" + XCQuery.Q + " ";
                else if (XCQuery.QO == "<")
                    queryRequest += "q<:" + XCQuery.Q + " ";
            }


            if (!String.IsNullOrEmpty(XCQuery.Area) && XCQuery.Area != Constants.NoneStr)
            {
                if (XCQuery.Area.ToUpper() != Constants.NoneStr)
                    queryRequest += "area:\"" + XCQuery.Area + "\" ";
            }

            if (XCQuery.DateSince)
            {
                queryRequest += "since:\"" +
                    string.Format("{0:yyyy-MM-dd}", XCQuery.DateSearch)
                        + "\" ";
            }
            else
            {
                if (!String.IsNullOrEmpty(XCQuery.Year) && XCQuery.Year != Constants.NoneStr)
                {
                    queryRequest += "year:" + XCQuery.Year
                        + " ";
                }

                if (XCQuery.MonthIndex > 0)
                {
                    queryRequest += "month:" + XCQuery.MonthIndex
                        + " ";
                }

            }

            //Complete query and clean it
            if (!string.IsNullOrEmpty(queryRequest)) 
            {
                //Add start string of query, lower case and trim spaces
                queryRequest = (Constants.XCAPIUrl + queryRequest).TrimEnd().ToLower();
                //Clean double spaces
                while (queryRequest.Contains("  "))
                    queryRequest = queryRequest.Replace("  ", " ");
            }

            //Test queries
            //queryRequest = Constants.XCAPIUrl + "nr:404086"; // with ssp
            //queryRequest = Constants.XCAPIUrl + "nr:134880";
            //queryRequest = Constants.XCAPIUrl + "passer iagoensis";
            //queryRequest = Constants.XCAPIUrl + "passer domesticus";
            //queryRequest = Constants.XCAPIUrl + "passer domesticos";
            //https://www.xeno-canto.org/134880
            //https://www.xeno-canto.org/api/2/recordings?query=134880

            return queryRequest;
        }

        public static string SecondsToString(double seconds, Boolean ShowMilliseconds = false)
        {
            string r = "";
            TimeSpan t = TimeSpan.FromSeconds(seconds);

            if (t.Hours != 0) r = t.Hours.ToString("00") + ":";
            r = r + t.Minutes.ToString("00") + ":";
            r = r + t.Seconds.ToString("00");
            if (ShowMilliseconds) r = r + "." + (t.Milliseconds / 100).ToString("0");

            return r;
        }

        public class XCAPIGetUris
        {
            //to fill this Class must give Url and Id from API Response
            //XCAPIRecordings.Url
            //XCAPIRecordings.Id
            public string AudioUri { get; set; }
            public string FFTSSmallImageUri { get; set; }
            public string FFTSLargeImageUri { get; set; }
            public string Error { get; set; }

            public string GetUris(string url, string id)
            {
                //returns the error code
                try
                {
                    //Create a WebRequest to get the file
                    HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(url);
                    //Create a response for this request
                    HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

                    if (fileResp != null)
                    {
                        AudioUri = Constants.UrlScheme +"//"+ fileResp.ResponseUri.Host + fileResp.ResponseUri.AbsolutePath;
                        string aux = Constants.UrlScheme + "//" + fileResp.ResponseUri.Host +
                            fileResp.ResponseUri.Segments[0] +
                            fileResp.ResponseUri.Segments[1] +
                            fileResp.ResponseUri.Segments[2] +
                            fileResp.ResponseUri.Segments[3] +
                            Constants.UrlFFTS + id;
                        FFTSSmallImageUri = aux + Constants.UrlFFTSSmallImage;
                        FFTSLargeImageUri = aux + Constants.UrlFFTSLargeImage;
                        Error = HttpStatusCode.OK.ToString();
                        return HttpStatusCode.OK.ToString();
                    }
                    else
                    {
                        throw new System.ArgumentException("No answer from host...!");
                    }

                }
                catch (WebException e)
                {
                    AudioUri = "Error playing file...!";
                    FFTSSmallImageUri = "Error getting the Spectrogram...!";
                    FFTSLargeImageUri = "Error getting the Spectrogram...!";
                    Error = e.Message.ToString(); 
                    return e.Message.ToString();
                }

            }
        //    //constructor(s)

        //    public Example()
        //    {
        //        //code
        //    }

        //    //method(s)

        //    public string ExampleMethod()
        //    {
        //        //code
        //    }
        }

    }
}
