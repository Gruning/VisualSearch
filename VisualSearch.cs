namespace VisualSearchEjemplo
{
    public class VisualSearch
    {
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
    }
}
