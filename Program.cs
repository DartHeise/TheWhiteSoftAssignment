using System.Text.Json;

namespace TheWhiteSoftAssignment
{
    internal class Program
    {
        /* ИНСТРУКЦИЯ ДЛЯ ЗАПУСКА
         * В переменную jsonReplacement передается путь к файлу replacement.json
         * В переменную responce передается указанный URI
         * Дополнительно закомментирована возможность передать в responce путь к файлу data.json
         * При запуске программы создается файл result.json в указанном пути
         */
        static void Main()
        {
            var jsonReplacement = File.ReadAllText("replacement.json");
            var replacementList = JsonSerializer.Deserialize<List<SourceReplacement>>(jsonReplacement);
            var replacementDictionary = new Dictionary<string, string>();
            if (replacementList != null)
            {
                replacementList.Sort(); // Сортируем список 
                foreach (var element in replacementList)
                {
                    if (!replacementDictionary.ContainsKey(element.Replacement)) // Если ключ отсутствует в словаре:
                    {
                        replacementDictionary.Add(element.Replacement, element.Source); // добавляем ключ и его значение
                    }
                    else
                    {
                        replacementDictionary[element.Replacement] = element.Source; // иначе заменяем значение ключа
                    }
                }
            }

            //var responce = File.ReadAllText("data.json");
            var responce = GetHttpMessage("https://raw.githubusercontent.com/thewhitesoft/student-2022-assignment/main/data.json");
            var jsonData = JsonSerializer.Deserialize<string[]>(responce.Result);
            var result = new List<string>();
            if (jsonData != null)
            {
                for (int i = 0; i < jsonData.Length; i++)
                {
                    foreach (var element in replacementDictionary)
                    {
                        if (element.Key != element.Value)
                            jsonData[i] = jsonData[i].Replace(element.Key, element.Value); // Исправляем в строке измененные сообщения
                    }
                    if (!string.IsNullOrEmpty(jsonData[i]))
                        result.Add(jsonData[i]); // Добавляем исправленную строку в список
                }
            }

            var jsonResult = JsonSerializer.Serialize(result.ToArray(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("result.json", jsonResult); // Записываем результат в result.json
        }
        static async Task<string> GetHttpMessage(string? requestUri)
        {
            return await new HttpClient().GetStringAsync(requestUri);
        }
    }
}