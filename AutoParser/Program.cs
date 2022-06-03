using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoParser
{
    internal class Program
    {
        static void Format(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i].Replace(@"\r", "");
                list[i] = list[i].Replace("&#x2757;", "");
                list[i] = list[i].Replace(@"\n", "");
                list[i] = list[i].Replace("&#xfe0f;", "");
                list[i] = list[i].Replace("&#x1f539;", "");
                list[i] = list[i].Replace("&#x2705;", "");
                list[i] = list[i].Replace("&#x2796;", "");
                list[i] = list[i].Replace("&#x2033;", "");
                list[i] = list[i].Replace("&#x1f699;", "");
                list[i] = list[i].Replace("}", "");
                list[i] = list[i].Replace("\"", "");
                list[i] = list[i].Replace("name", "Название");
                list[i] = list[i].Replace("description", "Описание");
                list[i] = list[i].Replace("numberOfDoors", "Количество дверей");
                list[i] = list[i].Replace("productionDate", "Год выпуска");
                list[i] = list[i].Replace("vehicleTransmission", "Трансмиссия");
                list[i] = list[i].Replace("datePublished", "Дата публикации");
                list[i] = list[i].Replace("vehicleConfiguration", "Конфигурация ТС");
                list[i] = list[i].Replace("bodyType", "Кузов");
                list[i] = list[i].Replace("modelDate", "Дата выпуска модели");
                list[i] = list[i].Replace("vehicleEngine", "Двигатель");
                list[i] = list[i].Replace("EngineSpecification", "Спецификация:");
                list[i] = list[i].Replace("engineDisplacement", "Объем двигателя");
                list[i] = list[i].Replace("enginePower", "Мощность двигателя");
                list[i] = list[i].Replace("fuelType", "Топливо");
                list[i] = list[i].Replace("{@type:", "");
                list[i] = list[i].Replace("brand", "Марка");
                list[i] = list[i].Replace("color", "Цвет");
                if(list[i].IndexOf("contentUrl") > 0)
                    list[i] = list[i].Replace(list[i].Substring(list[i].IndexOf("contentUrl"), list[i].IndexOf(".jpg") - list[i].IndexOf("contentUrl")+5), "");
                if(list[i].IndexOf("image") > 0)
                    list[i] = list[i].Replace(list[i].Substring(list[i].IndexOf("image"), list[i].IndexOf("ct,") - list[i].IndexOf("contentUrl")+5), "");
                
            }
        }
        static async Task Main(string[] args)
        {
            Console.Write("Mark name: ");
            string Vehicle = Console.ReadLine();
            Console.Write("Model name: ");
            List<string> vehiclelist = new List<string>();
            string CarModel = Console.ReadLine();
            string URL = $"https://auto.drom.ru/{Vehicle}/{CarModel}";
            HttpClient http = new HttpClient();
            try
            {
                HttpResponseMessage response = await http.GetAsync(URL);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                int index = 0;
                int pos = -1;
                while ((pos = responseBody.IndexOf("\"name\"", index)) != -1) {
                    if (responseBody.IndexOf("offers", pos) - pos - 2 > 0)
                    {
                        vehiclelist.Add(responseBody.Substring(pos, responseBody.IndexOf("offers", pos) - pos - 2));
                    }
                    else
                        break;
                    index = pos + 1;
                }
                Format(vehiclelist);
                foreach(string vehicle in vehiclelist)
                {
                    Console.WriteLine(vehicle);
                    Console.WriteLine("\n\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
