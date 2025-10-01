
# TranslatorMVC

![.NET](https://img.shields.io/badge/.NET-9.0-blue) ![Docker](https://img.shields.io/badge/Docker-Yes-blue) ![GitHub Actions](https://img.shields.io/badge/GitHub%20Actions-CI/CD-green)

## Workflow

<img src="screenshots/Screenshot 2025-10-01 201658.png">

---

## View

<img src="screenshots/Screenshot 2025-10-01 195126.png">

TranslatorMVC is a simple **.NET Core MVC application** that uses [Lingva.ml](https://lingva.ml) for language translation and is fully dockerized with CI/CD via GitHub Actions.

---

## Table of Contents

* [Features](#features)
* [Supported Languages](#supported-languages)
* [Prerequisites](#prerequisites)
* [Project Structure](#project-structure)
* [Setup & Run Locally](#setup--run-locally)
* [Docker](#docker)
* [GitHub Actions CI/CD](#github-actions-cicd)
* [Updating Docker Image in docker-compose](#updating-docker-image-in-docker-compose)
* [Publish Your Application](#publish-your-application)
* [Run from Publish Folder](#run-from-publish-folder)
* [License](#license)

---

## Features

* .NET Core 9.0 MVC application
* Uses Lingva.ml API for translation
* Dockerized for easy deployment
* CI/CD with GitHub Actions
* Automatic Docker image tagging (`v${{ github.run_number }}`)
* Auto-update of `docker-compose.yml` with latest image tag

---

## Supported Languages

| Code  | Language              |
| ----- | --------------------- |
| af    | Afrikaans             |
| sq    | Albanian              |
| am    | Amharic               |
| ar    | Arabic                |
| hy    | Armenian              |
| az    | Azerbaijani           |
| bn    | Bengali               |
| bs    | Bosnian               |
| bg    | Bulgarian             |
| ca    | Catalan               |
| zh    | Chinese (Simplified)  |
| zh-TW | Chinese (Traditional) |
| hr    | Croatian              |
| cs    | Czech                 |
| da    | Danish                |
| nl    | Dutch                 |
| en    | English               |
| eo    | Esperanto             |
| et    | Estonian              |
| fi    | Finnish               |
| fr    | French                |
| gl    | Galician              |
| ka    | Georgian              |
| de    | German                |
| el    | Greek                 |
| gu    | Gujarati              |
| ht    | Haitian Creole        |
| ha    | Hausa                 |
| he    | Hebrew                |
| hi    | Hindi                 |
| hu    | Hungarian             |
| is    | Icelandic             |
| id    | Indonesian            |
| ga    | Irish                 |
| it    | Italian               |
| ja    | Japanese              |
| jv    | Javanese              |
| kn    | Kannada               |
| kk    | Kazakh                |
| km    | Khmer                 |
| ko    | Korean                |
| ku    | Kurdish               |
| ky    | Kyrgyz                |
| lo    | Lao                   |
| lv    | Latvian               |
| lt    | Lithuanian            |
| mk    | Macedonian            |
| ml    | Malayalam             |
| mr    | Marathi               |
| mn    | Mongolian             |
| ne    | Nepali                |
| no    | Norwegian             |
| ps    | Pashto                |
| fa    | Persian               |
| pl    | Polish                |
| pt    | Portuguese            |
| pa    | Punjabi               |
| ro    | Romanian              |
| ru    | Russian               |
| sr    | Serbian               |
| si    | Sinhala               |
| sk    | Slovak                |
| sl    | Slovenian             |
| es    | Spanish               |
| su    | Sundanese             |
| sw    | Swahili               |
| sv    | Swedish               |
| ta    | Tamil                 |
| te    | Telugu                |
| th    | Thai                  |
| tr    | Turkish               |
| uk    | Ukrainian             |
| ur    | Urdu                  |
| vi    | Vietnamese            |
| cy    | Welsh                 |
| xh    | Xhosa                 |
| yi    | Yiddish               |
| yo    | Yoruba                |
| zu    | Zulu                  |

**Example translation request using `curl`:**

```bash
curl https://lingva.ml/api/v1/en/ta/hi%20i%20am%20yuvaraj | jq . > translate.json
```

---

## Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* [Docker](https://www.docker.com/get-started)
* [Docker Compose](https://docs.docker.com/compose/)
* GitHub repository with **write access token** (or use `GITHUB_TOKEN`)

---

## Project Structure

```
TranslatorMVC/
├── Controllers/
├── Models/
├── Views/
├── TranslatorMVC.csproj
├── TranslatorMVC.sln
├── docker-compose.yml
├── Dockerfile
└── .github/workflows/
```

---

## Setup & Run Locally

1. Clone the repo:

```bash
git clone https://github.com/yuva19102003/translate-mvc-app.git
cd translate-mvc-app
```
2. Unit testing

```bash
dotnet test
```

<img src="screenshots/Screenshot 2025-10-01 194916.png">


3. Run with Docker Compose:

```bash
docker-compose up --build -d
```

<img src="screenshots/Screenshot 2025-10-01 195145.png">


4. Access the app in your browser:

[http://localhost:5270](http://localhost:5270)

---

## Docker

**Build Docker image:**

```bash
docker build -t translatormvc:latest .
```

**Run container:**

```bash
docker run -d -p 5270:5270 --name translatormvc translatormvc:latest
```

**Stop & remove container:**

```bash
docker stop translatormvc
docker rm translatormvc
```

**View logs:**

```bash
docker logs -f translatormvc
```

---

## GitHub Actions CI/CD

Workflow includes:

<img src="screenshots/Screenshot 2025-10-01 194653.png">

<img src="screenshots/Screenshot 2025-10-01 194718.png">


1. Checkout repository
2. Set Git identity
3. Build Docker image (`v${{ github.run_number }}`)
4. Push image to Docker Hub
5. Update `docker-compose.yml` with new image tag
6. Commit & push changes

**Example snippet:**

```yaml
- name: Update docker-compose.yml
  run: |
    sed -i "s|image: .*|image: yuva19102003/translator-mvc-app:$TAG|g" docker-compose.yml
    git add docker-compose.yml
    git commit -m "Update docker-compose image tag to $TAG" || echo "No changes to commit"
    git push https://x-access-token:${{ secrets.GITHUB_TOKEN }}@github.com/yuva19102003/translate-mvc-app.git HEAD:main
  env:
    TAG: v${{ github.run_number }}
```

---

## Publish Your Application

```bash
dotnet publish -c Release -o ./publish
```

* `-c Release` → optimized for production
* `-o ./publish` → outputs to `publish` folder

---

## Run from Publish Folder

1. Navigate to publish folder:

```bash
cd /path/to/project/TranslatorMVC/publish
```

2. Run the app:

```bash
dotnet TranslatorMVC.dll
```

---

## Updating Docker Image in docker-compose

Whenever a new Docker image is pushed:

<img src="screenshots/Screenshot 2025-10-01 194744.png">


```bash
docker-compose pull
docker-compose up -d
```

* `docker-compose.yml` is automatically updated by GitHub Actions with latest `v`-tagged image.

---

## License

MIT License © Yuvaraj K

---
