using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Meeting.Entities.Constants;

namespace Meeting.Services.Extensions
{
    public class Translit
    {
        // объявляем и заполняем словарь с заменами
        // при желании можно исправить словать или дополнить
        Dictionary<string, string> dictionaryChar = new Dictionary<string, string>()
            {
                {"а","a"},
                {"б","b"},
                {"в","v"},
                {"г","g"},
                {"д","d"},
                {"е","e"},
                {"ё","yo"},
                {"ж","zh"},
                {"з","z"},
                {"и","i"},
                {"й","y"},
                {"к","k"},
                {"л","l"},
                {"м","m"},
                {"н","n"},
                {"о","o"},
                {"п","p"},
                {"р","r"},
                {"с","s"},
                {"т","t"},
                {"у","u"},
                {"ф","f"},
                {"х","h"},
                {"ц","ts"},
                {"ч","ch"},
                {"ш","sh"},
                {"щ","sch"},
                {"ъ","'"},
                {"ы","yi"},
                {"ь",""},
                {"э","e"},
                {"ю","yu"},
                {"я","ya"}
            };
        Dictionary<string, string> dictionaryCharDayShift = new Dictionary<string, string>()
        {
             {"У","u"},
             {"Д","d"},
             {"В","v"},
             {"УД","u_d"},
             {"ДВ","d_v"},
             {"УДВ","u_d_v"},
             {"НСТ","nost"},
        };
        Dictionary<string, string> dictionaryCharComplex = new Dictionary<string, string>()
        {
             {"ТАГ","TAG "},
             {"БС","BS"},
             {"Б","B"},
             {"СТИЛОБАТ","STIL"},
             {"Р","RA"},
        };
        /// <summary>
        /// метод делает транслит на латиницу
        /// </summary>
        /// <param name="source"> это входная строка для транслитерации </param>
        /// <returns>получаем строку после транслитерации</returns>
        public string TranslitName(string source,string type)
        {
            var result = "";
            switch (type)
            {
                case TypeTranslateName.dayShift_TC:
                    dictionaryCharDayShift.TryGetValue(source.ToString(), out result);
                    if (String.IsNullOrEmpty(result)) Translate(source);
                    break;
                case TypeTranslateName.complex:
                    dictionaryCharComplex.TryGetValue(source.ToString(), out result);
                    if (String.IsNullOrEmpty(result)) Translate(source);
                    break;
                case TypeTranslateName.course:
                    result = Translate(source);
                    break;
                default:
                    break;
            };
          
            
            // проход по строке для поиска символов подлежащих замене которые находятся в словаре dictionaryChar
            return result;
        }

        private string Translate(string source)
        {
            string result = "";
            foreach (var ch in source)
            {
                var ss = "";
                // берём каждый символ строки и проверяем его на нахождение его в словаре для замены,
                // если в словаре есть ключ с таким значением то получаем true 
                // и добавляем значение из словаря соответствующее ключу
                if (dictionaryChar.TryGetValue(ch.ToString(), out ss))
                {
                    result += ss;
                }
                // иначе добавляем тот же символ
                else result += ch;
            }
            return result;
        }
    }
}
