using SneezePharm;
using SneezePharm.PastaVenda;

void CarregarTodosArquivos()
{
    string diretorio = @"C:\SneezePharma\Files\";
    if (!Directory.Exists(diretorio))
    {
        Directory.CreateDirectory(diretorio);
    }
}

