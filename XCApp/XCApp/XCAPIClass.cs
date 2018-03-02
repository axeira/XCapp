using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace XCApp
{
    public class XCAPIClass
    {

        public static string ServerRequest(string url, out string result)
        {

            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                
                
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                    result = json_data;
                    return HttpStatusCode.OK.ToString();
                }
                catch (WebException e)
                {
                    result = string.Empty;
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

        //And you can deserialize your json easily :
        //JsonConvert.DeserializeObject<List<RecordingsJson>>(json);

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
                        Cnt = ConstantsClass.Countries[CntIndex];
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
                        Type = ConstantsClass.Type[TypeIndex];
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

            private string _Lic;
            public string Lic
            {
                get { return _Lic; }
                set
                {
                    if (_Lic != value)
                    {
                        _Lic = value;
                        OnPropertyChanged("Lic");
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
                        Q = ConstantsClass.Quality[QIndex];
                        OnPropertyChanged("QIndex");
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
                        Area = ConstantsClass.Area[AreaIndex];
                        OnPropertyChanged("AreaIndex");
                    }
                }
            }

            #region INotifyPropertyChanged Members
            public event PropertyChangedEventHandler PropertyChanged;
            #endregion

            protected virtual void OnPropertyChanged(string propertyName)
            {
                var changed = PropertyChanged;
                if (changed != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

                }
            }

        }

    }   
}
