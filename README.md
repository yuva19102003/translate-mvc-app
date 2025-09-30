
# TranslatorMVC

TranslatorMVC is a simple .NET MVC application that uses [Lingva.ml](https://lingva.ml) for language translation.

---

## Supported Languages

| Code | Language |
|------|----------|
| af   | Afrikaans |
| sq   | Albanian |
| am   | Amharic |
| ar   | Arabic |
| hy   | Armenian |
| az   | Azerbaijani |
| bn   | Bengali |
| bs   | Bosnian |
| bg   | Bulgarian |
| ca   | Catalan |
| zh   | Chinese (Simplified) |
| zh-TW | Chinese (Traditional) |
| hr   | Croatian |
| cs   | Czech |
| da   | Danish |
| nl   | Dutch |
| en   | English |
| eo   | Esperanto |
| et   | Estonian |
| fi   | Finnish |
| fr   | French |
| gl   | Galician |
| ka   | Georgian |
| de   | German |
| el   | Greek |
| gu   | Gujarati |
| ht   | Haitian Creole |
| ha   | Hausa |
| he   | Hebrew |
| hi   | Hindi |
| hu   | Hungarian |
| is   | Icelandic |
| id   | Indonesian |
| ga   | Irish |
| it   | Italian |
| ja   | Japanese |
| jv   | Javanese |
| kn   | Kannada |
| kk   | Kazakh |
| km   | Khmer |
| ko   | Korean |
| ku   | Kurdish |
| ky   | Kyrgyz |
| lo   | Lao |
| lv   | Latvian |
| lt   | Lithuanian |
| mk   | Macedonian |
| ml   | Malayalam |
| mr   | Marathi |
| mn   | Mongolian |
| ne   | Nepali |
| no   | Norwegian |
| ps   | Pashto |
| fa   | Persian |
| pl   | Polish |
| pt   | Portuguese |
| pa   | Punjabi |
| ro   | Romanian |
| ru   | Russian |
| sr   | Serbian |
| si   | Sinhala |
| sk   | Slovak |
| sl   | Slovenian |
| es   | Spanish |
| su   | Sundanese |
| sw   | Swahili |
| sv   | Swedish |
| ta   | Tamil |
| te   | Telugu |
| th   | Thai |
| tr   | Turkish |
| uk   | Ukrainian |
| ur   | Urdu |
| vi   | Vietnamese |
| cy   | Welsh |
| xh   | Xhosa |
| yi   | Yiddish |
| yo   | Yoruba |
| zu   | Zulu |

Example translation request using `curl`:

```bash
curl https://lingva.ml/api/v1/en/ta/hi%20i%20am%20yuvaraj | jq . > translate.json
````

---

## Publish Your Application

The command to publish your app to a folder:

```bash
dotnet publish -c Release -o ./publish
```

**Explanation:**

* `-c Release` → Publish in Release configuration (optimized for production).
* `-o ./publish` → Output the published files into a folder named `publish` in your project directory.

---

### Run from Publish Folder

1. Navigate to the publish folder:

```bash
cd /path/to/your/project/TranslatorMVC/publish
```

2. Run the app:

```bash
dotnet TranslatorMVC.dll
```

---

## Docker

Steps to build and run the application using Docker:

1. Build the Docker image:

```bash
docker build -t translatormvc:latest .
```

2. Run the container:

```bash
docker run -d -p 5270:5270 --name translatormvc translatormvc:latest
```

**Explanation:**

* `-p 5270:5270` maps your host port 5270 to the container port 5270.
* Access your app in the browser at: [http://localhost:5270](http://localhost:5270)

---