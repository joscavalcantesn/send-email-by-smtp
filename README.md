# 📧 Enviando e-mails com C#

Exemplo prático de envio de e-mails em **C#** utilizando **SMTP**.  
Este repositório serve como um ponto de partida para integrar envio de e-mails em aplicações .NET.

---

## ⚙️ Funcionalidades
- Envio de e-mails para múltiplos destinatários  
- Suporte a **HTML no corpo do e-mail**  
- Inclusão de **anexos**  

---

## 📋 Pré-requisitos
- .NET 8
- Conta e servidor SMTP válido (ex: Gmail, Outlook, Mailtrap)  

---

## 🔧 Configuração
Configure as credenciais no seu código (ou em variáveis de ambiente):

```csharp
var email = new Email("smtp.gmail.com", "seu-email@gmail.com", "sua-senha");
