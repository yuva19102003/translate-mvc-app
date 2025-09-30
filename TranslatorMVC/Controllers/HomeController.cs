using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TranslatorMVC.Models;
using System.Collections.Generic;

namespace TranslatorMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _lingvaBaseUrl;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _lingvaBaseUrl = configuration["LingvaApi:BaseUrl"];
        }

        // Language codes and names
        public static readonly Dictionary<string, string> Languages = new Dictionary<string, string>
        {
            {"af","Afrikaans 🇿🇦"}, {"sq","Albanian 🇦🇱"}, {"am","Amharic 🇪🇹"}, {"ar","Arabic 🇸🇦"}, {"hy","Armenian 🇦🇲"},
            {"az","Azerbaijani 🇦🇿"}, {"bn","Bengali 🇧🇩"}, {"bs","Bosnian 🇧🇦"}, {"bg","Bulgarian 🇧🇬"}, {"ca","Catalan 🇪🇸"},
            {"zh","Chinese (Simplified) 🇨🇳"}, {"zh-TW","Chinese (Traditional) 🇹🇼"}, {"hr","Croatian 🇭🇷"}, {"cs","Czech 🇨🇿"},
            {"da","Danish 🇩🇰"}, {"nl","Dutch 🇳🇱"}, {"en","English 🇬🇧"}, {"eo","Esperanto 🌐"}, {"et","Estonian 🇪🇪"}, {"fi","Finnish 🇫🇮"},
            {"fr","French 🇫🇷"}, {"gl","Galician 🇪🇸"}, {"ka","Georgian 🇬🇪"}, {"de","German 🇩🇪"}, {"el","Greek 🇬🇷"}, {"gu","Gujarati 🇮🇳"},
            {"ht","Haitian Creole 🇭🇹"}, {"ha","Hausa 🇳🇬"}, {"he","Hebrew 🇮🇱"}, {"hi","Hindi 🇮🇳"}, {"hu","Hungarian 🇭🇺"}, {"is","Icelandic 🇮🇸"},
            {"id","Indonesian 🇮🇩"}, {"ga","Irish 🇮🇪"}, {"it","Italian 🇮🇹"}, {"ja","Japanese 🇯🇵"}, {"jv","Javanese 🇮🇩"}, {"kn","Kannada 🇮🇳"},
            {"kk","Kazakh 🇰🇿"}, {"km","Khmer 🇰🇭"}, {"ko","Korean 🇰🇷"}, {"ku","Kurdish 🇹🇷"}, {"ky","Kyrgyz 🇰🇬"}, {"lo","Lao 🇱🇦"},
            {"lv","Latvian 🇱🇻"}, {"lt","Lithuanian 🇱🇹"}, {"mk","Macedonian 🇲🇰"}, {"ml","Malayalam 🇮🇳"}, {"mr","Marathi 🇮🇳"}, {"mn","Mongolian 🇲🇳"},
            {"ne","Nepali 🇳🇵"}, {"no","Norwegian 🇳🇴"}, {"ps","Pashto 🇦🇫"}, {"fa","Persian 🇮🇷"}, {"pl","Polish 🇵🇱"}, {"pt","Portuguese 🇵🇹"},
            {"pa","Punjabi 🇮🇳"}, {"ro","Romanian 🇷🇴"}, {"ru","Russian 🇷🇺"}, {"sr","Serbian 🇷🇸"}, {"si","Sinhala 🇱🇰"}, {"sk","Slovak 🇸🇰"},
            {"sl","Slovenian 🇸🇮"}, {"es","Spanish 🇪🇸"}, {"su","Sundanese 🇮🇩"}, {"sw","Swahili 🇰🇪"}, {"sv","Swedish 🇸🇪"}, {"ta","Tamil 🇮🇳"},
            {"te","Telugu 🇮🇳"}, {"th","Thai 🇹🇭"}, {"tr","Turkish 🇹🇷"}, {"uk","Ukrainian 🇺🇦"}, {"ur","Urdu 🇵🇰"}, {"vi","Vietnamese 🇻🇳"},
            {"cy","Welsh 🏴"}, {"xh","Xhosa 🇿🇦"}, {"yi","Yiddish 🌐"}, {"yo","Yoruba 🇳🇬"}, {"zu","Zulu 🇿🇦"} // add more if needed
        };

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Languages = Languages;
            return View(new TranslationRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Index(TranslationRequest request)
        {
            ViewBag.Languages = Languages;

            if (string.IsNullOrEmpty(request.Text) ||
                string.IsNullOrEmpty(request.SourceLang) ||
                string.IsNullOrEmpty(request.TargetLang))
            {
                ModelState.AddModelError("", "Please enter text and select both languages.");
                return View(request);
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                string url = $"{_lingvaBaseUrl}/{request.SourceLang}/{request.TargetLang}/{Uri.EscapeDataString(request.Text)}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(json);
                    request.TranslatedText = doc.RootElement.GetProperty("translation").GetString();
                }
                else
                {
                    request.TranslatedText = "Error: Unable to fetch translation";
                }
            }
            catch
            {
                request.TranslatedText = "Error: API request failed";
            }

            return View(request);
        }
    }
}
