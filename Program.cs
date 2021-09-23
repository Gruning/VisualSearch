using Microsoft.Bing.VisualSearch;
using VisualSearchEjemplo;

FileStream stream = new FileStream("../../../car.jpg", FileMode.Open);

var cliente = new VisualSearchClient(
    new ApiKeyServiceClientCredentials(AppSettings.ApiKeySearch)
    );
Console.WriteLine("inicia llamada");
var resultados = await cliente.Images.VisualSearchMethodAsync(image: stream);
Console.WriteLine("llamada terminada");
if (resultados.Tags.Count > 0) {
    foreach (var tag in resultados.Tags) {
        Console.WriteLine(tag.Actions[0].DisplayName);
    }
}