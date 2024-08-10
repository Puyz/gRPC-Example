# gRPC
- Google’ın geliştirdiği Remote Procedure Call, yani başka bir servis ya da uzak sunucudaki bir metodu sanki kendi servisimizin metoduymuş gibi kullanabilmemizi sağlayan, client-server ilişkisindeki iletişimi kolay ve hızlıca sunan bir frameworktür.

![e2d7a531f783426fa5537acdbaeb42f1](https://github.com/user-attachments/assets/d0af62c3-459c-4dbf-93b6-914a8b11fb52)

## Protocol Buffer (ProtoBuf)
- gRPC için kullanılan **verinin binary olarak serialization yapan haberleşme protokolüdür.**

- Protocol Buffer, Google tarafından bulunmuş ve çoğunlukla gRPC ile kullanılan JSON modeli gibi bir **binary serileştirme formatıdır** fakat bazı durumlarda JSONdan 2 kat daha hafif olabilir. Dilden bağımsız olması ve http/2. ile taşınma hızını katlaması gRPC açısından çok avantajlıdır. 
- JSON’ın aksine oluşturulan **objelerden kesin bir düzen ve data type bekler.** Ve bunları binary formatlayarak http/2. aracılığı ile diğer mikro servise gönderir.

## gRPC Konseptleri ve Client/Server Arasındaki İletişim Tipleri
**Unary RPC: (1-1)** Client'ın server'a tek bir istek gönderdiği ve normal bir işlev çağrısı gibi tek bir yanıt geri aldığı RPC türüdür.

**Server streaming RPC: (1-n)** Client, server'a tek bir istek gönderir ve bir dizi mesajı geri okumak için bir akış (stream) alır.

**Client streaming RPC: (n-1)** Client'ın server'a stream mesaj gönderdiği ve server'ın tek bir response döndürdüğü RPC türüdür.

**Bi-directional streaming RPC: (n-n)** Client'ın server'a stream mesaj gönderdiği ve server'ın stream response döndürdüğü RPC türüdür. İki akış bağımsız olarak çalışır. Her akıştaki mesajların sırası korunur.

## gRPC Yaşam Döngüsü

![gRPC-Nedir-Ne-Amacla-ve-Nasil-Kullanilir](https://github.com/user-attachments/assets/205eed6b-04f8-4c42-8a19-83f5e9d1570f)
