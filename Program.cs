using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
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

if (resultadosCV.Captions.Count > 0) {
    foreach (var cap in resultadosCV.Captions) {
        Console.WriteLine(cap.Text);
    }
}

Console.WriteLine("fin llamada al Computer ");