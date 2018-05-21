using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using GraduateWorkTest;
using Newtonsoft.Json.Linq;

namespace WebAPIClient
{
    class Program
    {
        private static string url = "http://api.ispras.ru/texterra/v1/nlp?targetType=named-entity&apikey=e00be91e113526bc1e691f6d70c2a7f4576104ae";
        private static RestClient client = new RestClient(url);
        private static RestRequest request = new RestRequest(Method.POST);


        /*
        static void Main(string[] args)
        {
            //ToDo вынести парсинг в отдельный класс

            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "[{\"text\" : \"Later, it was said the man came from the north, from Ropers Gate. He came on foot, leading his laden horse by the bridle. It was late afternoon and the ropers’, saddlers’ and tanners’ stalls were already closed, the street empty. It was hot but the man had a black coat thrown over his shoulders. He drew attention to himself. He stopped in front of the Old Narakort Inn, stood there for a moment, listened to the hubbub of voices. As usual, at this hour, it was full of people. The stranger did not enter the Old Narakort. He pulled his horse further down the street to another tavern, a smaller one, called The Fox. Not enjoying the best of reputations, it was almost empty.\" }]", ParameterType.RequestBody);
            //request.AddParameter("application/json", "[ {\"text\" : \"Our kids should grow up in an America where opportunity is real.\" }," +
            //                                         "{\"text\" : \"Согласно официальному прогнозу Минэкономразвития, ВВП России упадет на 3%.\"} ]", ParameterType.RequestBody);

            // full JSON content
            IRestResponse response = client.Execute(request);

            JArray sentenceArr = JArray.Parse(response.Content);
            Console.WriteLine(sentenceArr[0]);
            //Console.WriteLine(arr[1]);
            Console.ReadKey();

            /*

            // Array with each of sentences
            JArray sentenceArr = JArray.Parse(response.Content);
            //Console.WriteLine(arr[0]);
            //Console.WriteLine(arr[1]);
            //Console.ReadKey();

            // храним элементы JSON, как лист объектов типа EachSentenceInfo
            var eachSentenceInfoObj = JsonConvert.DeserializeObject <List<EachSentenceInfo>>(response.Content);
            
            // проходим по каждому предложению
            for (int i = 0; i <= sentenceArr.Count; i++) 
            {
                //Console.WriteLine("Element of arr: " + sentenceArr[i]);
                // заходим в содержимое annotations
                foreach (var ret in eachSentenceInfoObj[i].annotations)
                {
                    // заходим в содержимое StartEndValue
                    foreach (var StartEndValueArr in ret.Value)
                    {

                        //Console.WriteLine("langArr: " + StartEndValueArr);
                        // храним элементы 
                        var sevObj = JsonConvert.DeserializeObject<StartEndValue>(StartEndValueArr.ToString());
                        //Console.WriteLine("start: " + iad.start);
                        //Console.WriteLine("end: " + iad.end);

                        // 
                        foreach (var ValueObj in sevObj.value)
                        {
                            Console.WriteLine();
                        }


                        //Console.WriteLine("language: " + iad.value);
                        //Console.ReadKey();

                    }
                }
            }


            /////// Language detection
            /*
            for (int i = 0; i <= arr.Count; i++)
            {
                Console.WriteLine("Element of arr: " + arr[i]);
                foreach (var ret in info[i].annotations)
                {
                    foreach (var langArr in ret.Value)
                    {

                        Console.WriteLine("langArr: " + langArr);
                        var iad = JsonConvert.DeserializeObject<Language>(langArr.ToString());

                        Console.WriteLine("start: " + iad.start);
                        Console.WriteLine("end: " + iad.end);
                        Console.WriteLine("language: " + iad.value);
                        Console.ReadKey();

                    }
                }
            }
            */
            //}
        

    }
}
