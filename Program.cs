using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Newtonsoft.Json;
using System.Text;

FileStream stream = new FileStream("../../../imagen.jpg", FileMode.Open);



Console.WriteLine("inicia llamada al Computer ");
var clienteCV = new ComputerVisionClient(
    new Microsoft.Azure.CognitiveServices.Vision.ComputerVision.ApiKeyServiceClientCredentials(AppSettings.ApiKeyCV)
    );
clienteCV.Endpoint = AppSettings.EndpointCV;

var resultadosCV = await clienteCV.DescribeImageInStreamAsync(stream);

string route = "/translate?api-version=3.0&from=en&to=es";

if (resultadosCV.Captions.Count > 0) {
    foreach (var cap in resultadosCV.Captions) {
        //traduccion
        //endpoint https://api.cognitive.microsofttranslator.com/

        object[] body = new object[] { new { Text = cap.Text } };
        var requuestBody = JsonConvert.SerializeObject(body);

        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage()) {
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri("https://api.cognitive.microsofttranslator.com/" + route);
            request.Content = new StringContent(requuestBody, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", AppSettings.TranslateKey);
            request.Headers.Add("Ocp-Apim-Subscription-Region", "eastus");

            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);
        }

    }
}

Console.WriteLine("fin llamada al Computer Vision ");

public static class AppSettings
{
    //public static string ApiKey = "5449e07d53bc4f2e85a585d4bf72c8c2";

    public static string ApiKeySearch = "0b0d36e8da3a440db1fdd65c04f6dac7";
    // cv  6af1c2ecbaae455694fed243cd6e1385

    //cv teacher 2a68dc731e24451c9d1e1130d3db6536

    public static string ApiKeyCV = "6af1c2ecbaae455694fed243cd6e1385";

    public static string EndpointCV = "https://c-vision-2021.cognitiveservices.azure.com/";

    public static string TranslateKey = "0b0427f95f224ff88b9ca0a5402038e4";

}
}