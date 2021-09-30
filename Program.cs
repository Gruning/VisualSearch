using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Newtonsoft.Json;
using System.Text;
using VisualSearchEjemplo;

FileStream stream = new FileStream("../../../imagen.jpg", FileMode.Open);

//var cliente = new VisualSearchClient(
//    new Microsoft.Bing.VisualSearch.ApiKeyServiceClientCredentials(AppSettings.ApiKeySearch)
//    );
//Console.WriteLine("inicia llamada al visual search");
//var resultados = await cliente.Images.VisualSearchMethodAsync(image: stream);
//Console.WriteLine("llamada terminada Visual Seach");
//if (resultados.Tags.Count > 0) {
//    foreach (var tag in resultados.Tags) {
//        Console.WriteLine(tag.Actions[0].DisplayName);
//    }
//}

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

Console.WriteLine("fin llamada al Computer ");