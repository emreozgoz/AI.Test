# PDF Metin Çıkartma ve Özetleme AI Kullanarak

Bu proje, PDF dosyalarından metin çıkartmayı ve ardından AI (yerel bir Ollama chat istemcisi üzerinden) kullanarak içeriği özetlemeyi gösterir. Amaç, bir PDF dizinindeki dosyaları işleyip, metinlerini çıkartmak ve AI modelinden belirli bir JSON formatında özetler almak.

## Gereksinimler

- .NET 6 veya daha yeni bir sürüm
- PDF çıkartma için iText 7
- Yerel sunucuda çalışan Ollama Chat Client

## Kurulum

### 1. Gerekli Paketlerin Kurulması

PDF işleme ve AI iletişimi için aşağıdaki NuGet paketlerini kurmanız gerekiyor:

```bash
dotnet add package iText7
dotnet add package Microsoft.Extensions.Hosting
dotnet add package Microsoft.Extensions.AI

### Medium Yazısı
https://medium.com/@emreozgoz/net-ile-ak%C4%B1ll%C4%B1-pdf-%C3%B6zetleme-literat%C3%BCr-tarama-s%C3%BCrecini-yapay-zeka-ile-kolayla%C5%9Ft%C4%B1r%C4%B1n-60ca0b207461
