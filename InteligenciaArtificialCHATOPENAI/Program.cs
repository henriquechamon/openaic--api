using System.Drawing;
using System.Text;
using Newtonsoft.Json;

Início:
Console.Title = "Inteligência artificial - Aligg System";
Console.WriteLine("Olá! Seja muito bem-vindo(a) ao sistema de inteligência artificial Aligg.");
Console.WriteLine();
Console.WriteLine();
Console.WriteLine("O Que você deseja fazer?");
Console.WriteLine("[1] - Utilizar o sistema de texto");
Console.WriteLine("[2] - Utilizar o sistema de imagem");
Console.Write("Escolha:"); int escolha = int.Parse(Console.ReadLine());
if (escolha == 1) {

TextInteligence:
    Console.Clear();
    Console.Title = "Inteligência artificial - Text Inteligence";
    Console.Write("Digite sua pergunta: ");
    var pergunta = Console.ReadLine();
    if (pergunta.Length > 1)
    {
        HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Add("authorization", "Bearer <SUAKEY>");

        var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \"" + pergunta + "\",\"temperature\": 1,\"max_tokens\": 1024}",
            Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/completions", content);

        string responseString = await response.Content.ReadAsStringAsync();

        try
        {
            var dyData = JsonConvert.DeserializeObject<dynamic>(responseString);

            string guess = GuessCommand(dyData!.choices[0].text);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ResetColor();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"---> Houve um erro!");
        }

        static string GuessCommand(string raw)
        {
            Console.WriteLine("Resposta:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(raw);

            var lastIndex = raw.LastIndexOf('\n');

            string guess = raw.Substring(lastIndex + 1);

            Console.ResetColor();

            return guess;
        }
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Pressione ENTER para fechar...");
        Console.ReadLine();
        goto TextInteligence;
    }
    else
    {
        Console.WriteLine("Utilize pelo menos uma linha!");
        Thread.Sleep(2000);
        goto TextInteligence;
    }
}
if (escolha == 2)
{
Imagem:
    Console.Clear();
    Console.Title = "Inteligência artificial - Imagem";
    string apiKey = "<SUAKEY>";
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("O Que você deseja gerar: ");
    Console.ForegroundColor = ConsoleColor.White;
    string prompt = Console.ReadLine();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://api.openai.com/v1/images/generations"),
                    Content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        model = "image-alpha-001",
                        prompt = prompt
                    }), Encoding.UTF8, "application/json")
                };

                var response = client.SendAsync(request).Result;

        if (response.IsSuccessStatusCode)
        {
            string responseString = response.Content.ReadAsStringAsync().Result;
            dynamic responseJson = JsonConvert.DeserializeObject(responseString);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" " + responseJson.data[0].url);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Pressione ENTER para voltar ao início.");
            Console.ReadLine();
            goto Imagem;
        }
        else
        {
            Console.WriteLine("Eita! Error! Tente reiniciar o sistema!");
            Thread.Sleep(3000);
            goto Imagem;
        }
    }
}