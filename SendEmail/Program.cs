using SendEmail;

var gmail = new Email("smtp.gmail.com", "", Env.Password);

gmail.SendEmail(
emailsTo: [""],
subject: "Teste",
body: "Segue anexo",
attachments: new List<string>
{
    @"C:\temp\excel\Dados.xlsx"
});