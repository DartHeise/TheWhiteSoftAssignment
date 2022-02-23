using System.Text.Json;
using System.Net;

namespace TheWhiteSoftAssignment
{
    internal class Program
    {
        /* ИНСТРУКЦИЯ ДЛЯ ЗАПУСКА
         * В переменную jsonReplacement передается путь к файлу replacement.json
         * В переменную responce передается указанный URL
         * Закомментирована возможность передать в responce путь к файлу data.json
         * Программа запускается и создает файл result.json в указанном пути
         */
        static void Main()
        {
            var jsonReplacement = File.ReadAllText("replacement.json");
            var replaceList = JsonSerializer.Deserialize<List<SourceReplacement>>(jsonReplacement);
            var replacementDictionary = new Dictionary<string, string>();

            replaceList.Sort(); // Сортируем список 
            foreach (SourceReplacement el in replaceList)
            {
                if (!replacementDictionary.ContainsKey(el.replacement)) // Если ключ отсутствует в словаре:
                {
                    replacementDictionary.Add(el.replacement, el.source); // добавляем ключ и его значение
                }
                else
                {
                    replacementDictionary[el.replacement] = el.source; // иначе заменяем значение ключа
                }
            }

            var webClient = new WebClient();
           // var responce = File.ReadAllText("data.json");
            var responce = webClient.DownloadString
            ("https://raw.githubusercontent.com/thewhitesoft/student-2022-assignment/main/data.json");
            var jsonData = JsonSerializer.Deserialize<string[]>(responce);
            var result = new List<string>();

            for (int i = 0; i < jsonData.Length; i++)
            {
                foreach (var values in replacementDictionary)
                {
                    jsonData[i] = jsonData[i].Replace(values.Key, values.Value); // Исправляем в строке измененные сообщения
                }
                if (!string.IsNullOrEmpty(jsonData[i]))
                    result.Add(jsonData[i]); // Добавляем исправленную строку в список
            }

            var jsonResult = JsonSerializer.Serialize
            (result.ToArray(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("result.json", jsonResult); // Записываем результат в result.json
        }
    }
}